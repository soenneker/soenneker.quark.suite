using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>Stores the latest slice values for realtime pie and donut charts.</summary>
/// <remarks>
/// Bind Series, Labels, and Version to Chart.Series, Chart.Labels, and Chart.DataVersion.
/// Update on the Blazor dispatcher and request a parent render after each update.
/// This class is not thread-safe. Disable ChartOptions.Animate for frequent updates.
/// </remarks>
public sealed class RealtimeRadialChartData
{
    private readonly double?[] _values;

    /// <summary>Creates a chart snapshot with fixed slice labels and initially zero values.</summary>
    public RealtimeRadialChartData(string seriesName, params string[] labels)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(seriesName);
        ArgumentNullException.ThrowIfNull(labels);
        if (labels.Length == 0)
            throw new ArgumentException("At least one slice is required.", nameof(labels));
        foreach (var label in labels)
            ArgumentException.ThrowIfNullOrWhiteSpace(label);

        Labels = Array.AsReadOnly((string[])labels.Clone());
        _values = new double?[labels.Length];
        Array.Fill(_values, (double?)0);
        Series = Array.AsReadOnly(new[] { new ChartSeries(seriesName, Array.AsReadOnly(_values)) });
    }

    /// <summary>Gets the stable collection containing the slice series.</summary>
    public IReadOnlyList<ChartSeries> Series { get; }

    /// <summary>Gets the slice labels in constructor order.</summary>
    public IReadOnlyList<string> Labels { get; }

    /// <summary>Gets the revision to bind to Chart.DataVersion.</summary>
    public long Version { get; private set; }

    /// <summary>Replaces all slice values and advances the revision once. Values must be finite and non-negative.</summary>
    /// <remarks>Supply one value per label. Validation completes before any values change. Zero hides a slice.</remarks>
    public void Update(params double[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        Update(values.AsSpan());
    }

    /// <summary>Replaces all slice values without allocating an argument array.</summary>
    /// <remarks>Supply one finite, non-negative value per label. Validation completes before any values change.</remarks>
    public void Update(params ReadOnlySpan<double> values)
    {
        if (values.Length != _values.Length)
            throw new ArgumentException("Supply one value per slice.", nameof(values));
        foreach (var value in values)
        {
            if (!double.IsFinite(value) || value < 0)
                throw new ArgumentException("Slice values must be finite and non-negative.", nameof(values));
        }

        for (var index = 0; index < values.Length; index++)
            _values[index] = values[index];
        Version++;
    }

    /// <summary>Resets all slices to zero while preserving series and label identity.</summary>
    public void Clear()
    {
        Array.Fill(_values, (double?)0);
        Version++;
    }
}
