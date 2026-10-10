using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Soenneker.Asyncs.Locks;

namespace Soenneker.Quark;

public partial class ComparisonChart
{
    [Inject] private IComparisonChartInterop ComparisonChartInterop { get; set; } = null!;
    [Parameter] public IReadOnlyList<ComparisonChartRow> Rows { get; set; } = [];
    [Parameter] public IReadOnlyList<ComparisonChartMetric> Metrics { get; set; } = [];
    [Parameter] public bool MirrorFirstMetric { get; set; } = true;
    [Parameter] public bool Sortable { get; set; } = true;
    [Parameter] public bool Animate { get; set; } = true;
    [Parameter] public bool ShowLegend { get; set; } = true;
    [Parameter] public string EntryLabel { get; set; } = "Entry";
    [Parameter] public EventCallback<ComparisonChartRow> OnSelect { get; set; }
    [Parameter] public string EmptyText { get; set; } = "No data to display";

    private int _sortIndex = -1;
    private bool _ascending;
    private ComparisonChartRow? _selected;
    private string? _activeGroup;
    private int _sortVersion;
    private readonly string _tooltipPrefix = $"comparison-{Guid.NewGuid():N}";
    private readonly Dictionary<ComparisonChartRow, ElementReference> _rowElements = new();
    private readonly Dictionary<ComparisonChartRow, ElementReference> _observedRows = new();
    private readonly AsyncLock _revealLock = new();
    private bool _disposed;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        using (await _revealLock.Lock())
        {
            if (_disposed)
                return;

            var elements = _rowElements.Where(pair => Metrics.Count > 0 && Rows.Contains(pair.Key)).ToDictionary();
            foreach (var row in _rowElements.Keys.Except(elements.Keys).ToArray())
                _rowElements.Remove(row);
            foreach (var observed in _observedRows.ToArray())
            {
                if (!Animate || !elements.TryGetValue(observed.Key, out var current) || current.Id != observed.Value.Id)
                {
                    await ComparisonChartInterop.Destroy(observed.Value);
                    _observedRows.Remove(observed.Key);
                }
            }

            if (Animate)
            {
                foreach (var element in elements)
                {
                    if (!_observedRows.ContainsKey(element.Key))
                    {
                        await ComparisonChartInterop.Initialize(element.Value);
                        _observedRows.Add(element.Key, element.Value);
                    }
                }
            }
        }
    }

    public override async ValueTask DisposeAsync()
    {
        if (_disposed)
            return;
        _disposed = true;
        try
        {
            using (await _revealLock.Lock())
            {
                foreach (var element in _observedRows.Values)
                    await ComparisonChartInterop.Destroy(element);
                _observedRows.Clear();
            }
        }
        catch (Exception ex) when (ex is JSDisconnectedException or InvalidOperationException or TaskCanceledException)
        {
        }
        finally
        {
            await base.DisposeAsync();
        }
    }

    private string ColumnStyle => "--comparison-columns:" + string.Join(" ", Columns().Select(index =>
        index < 0 ? (MirrorFirstMetric ? "minmax(130px, .65fr)" : "minmax(105px, 220px)") : Metrics[index].ShowMarker ? "minmax(100px, .42fr)" : "minmax(0, 1fr)"));

    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        foreach (var metric in Metrics)
        {
            if (!double.IsFinite(metric.Minimum) || !double.IsFinite(metric.Maximum) || metric.Maximum <= metric.Minimum ||
                !double.IsFinite(metric.Maximum - metric.Minimum) || (metric.Logarithmic && metric.Minimum <= 0))
                throw new ArgumentException($"Metric '{metric.Name}' requires finite, increasing bounds and a positive minimum for logarithmic scales.", nameof(Metrics));
            if (metric.Ticks is not null && metric.Ticks.Any(tick => !double.IsFinite(tick) || tick < metric.Minimum || tick > metric.Maximum))
                throw new ArgumentException($"Metric '{metric.Name}' requires finite ticks within its bounds.", nameof(Metrics));
        }

        if (_sortIndex >= Metrics.Count || !Sortable)
            _sortIndex = -1;
        if (_selected is not null && !Rows.Contains(_selected))
            _selected = null;
    }

    private async Task Select(ComparisonChartRow row)
    {
        _selected = row;
        await OnSelect.InvokeAsync(row);
    }

    private static string Group(ComparisonChartRow row) => row.Highlighted ? "highlight" : row.Reference ? "reference" : "other";
    private IEnumerable<string> LegendGroups() => Rows.Select(Group).Distinct().OrderBy(group => group == "highlight" ? 0 : group == "reference" ? 1 : 2);
    private string LegendLabel(string group) => group switch
    {
        "highlight" => Rows.Count(row => row.Highlighted) == 1 ? Rows.First(row => row.Highlighted).Name : "Highlighted entries",
        "reference" => Rows.Count(row => row.Reference) == 1 ? Rows.First(row => row.Reference).Name + " (reference)" : "Reference entries",
        _ => "Other entries"
    };
    private void ToggleGroup(string group) => _activeGroup = _activeGroup == group ? null : group;

    private IEnumerable<int> Columns()
    {
        if (MirrorFirstMetric)
            yield return 0;
        yield return -1;
        for (var index = MirrorFirstMetric ? 1 : 0; index < Metrics.Count; index++)
            yield return index;
    }

    private void Sort(int index)
    {
        _ascending = _sortIndex == index ? !_ascending : Metrics[index].LowerIsBetter;
        _sortIndex = index;
        _sortVersion++;
    }

    private string SortLabel(int index) => _sortIndex != index ? "none" : _ascending ? "ascending" : "descending";

    private IEnumerable<ComparisonChartRow> OrderedRows()
    {
        if (_sortIndex < 0)
            return Rows;

        // Missing values always stay last, including when the sort direction is reversed.
        var available = Rows.OrderBy(row => Value(row, _sortIndex) is null);
        return _ascending ? available.ThenBy(row => Value(row, _sortIndex)) : available.ThenByDescending(row => Value(row, _sortIndex));
    }

    private static double? Value(ComparisonChartRow row, int index) =>
        index < row.Values.Count && row.Values[index] is { } value && double.IsFinite(value) ? value : null;

    private static string Format(ComparisonChartMetric metric, double value) =>
        metric.ValueFormatter?.Invoke(value) ?? value.ToString("0.##", CultureInfo.CurrentCulture);

    private static double? UpperValue(ComparisonChartRow row, int index) => row.UpperValues is { } upper &&
        index < upper.Count && upper[index] is { } value && double.IsFinite(value) && Value(row, index) is { } lower && value >= lower ? value : null;

    private static IEnumerable<double> Ticks(ComparisonChartMetric metric)
    {
        if (metric.Ticks is not null)
            return metric.Ticks;
        var intervals = metric.ShowMarker ? 2 : 4;
        return Enumerable.Range(0, intervals + 1).Select(index => metric.Logarithmic
            ? Math.Exp(Math.Log(metric.Minimum) + (Math.Log(metric.Maximum) - Math.Log(metric.Minimum)) * index / intervals)
            : metric.Minimum + (metric.Maximum - metric.Minimum) * index / intervals);
    }

    private static double Fraction(ComparisonChartMetric metric, double value)
    {
        var clamped = Math.Clamp(value, metric.Minimum, metric.Maximum);
        return metric.Logarithmic
            ? (Math.Log(clamped) - Math.Log(metric.Minimum)) / (Math.Log(metric.Maximum) - Math.Log(metric.Minimum))
            : (clamped - metric.Minimum) / (metric.Maximum - metric.Minimum);
    }

    private static string Percent(ComparisonChartMetric metric, double value) => (Fraction(metric, value) * 100).ToString("0.####", CultureInfo.InvariantCulture);

    private static string MarkStyle(ComparisonChartRow row, ComparisonChartMetric metric, double value, double? upper) =>
        $"--comparison-position:{Percent(metric, value)}%;--comparison-upper:{Percent(metric, upper ?? value)}%;" +
        (row.Color is null ? string.Empty : $"--comparison-color:{row.Color};");
}
