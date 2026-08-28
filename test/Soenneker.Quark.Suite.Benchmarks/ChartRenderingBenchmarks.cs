using System.Linq;
using AngleSharp.Dom;
using BenchmarkDotNet.Attributes;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Benchmarks;

/// <summary>
/// Measures stable parameter delivery, changed geometry, and interaction-only chart renders independently.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 8)]
public class ChartRenderingBenchmarks
{
    private BunitContext _stableContext = null!;
    private BunitContext _changingContext = null!;
    private BunitContext _hoverContext = null!;
    private IRenderedComponent<Chart> _stableChart = null!;
    private IRenderedComponent<Chart> _changingChart = null!;
    private IRenderedComponent<Chart> _hoverChart = null!;
    private AngleSharp.Dom.IElement[] _hitAreas = [];
    private string[] _labels = [];
    private ChartSeries[] _seriesA = [];
    private ChartSeries[] _seriesB = [];
    private ChartOptions _options = null!;
    private bool _useAlternateSeries;
    private int _hoverIndex;

    [Params(100, 1000)]
    public int PointCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _labels = Enumerable.Range(1, PointCount).Select(static index => $"Point {index}").ToArray();
        _seriesA = [new ChartSeries("Requests", Enumerable.Range(1, PointCount).Select(static index => (double)index).ToArray())];
        _seriesB = [new ChartSeries("Requests", Enumerable.Range(1, PointCount).Select(static index => index * 1.01).ToArray())];
        _options = new ChartOptions { ShowPoints = false, Animate = false };

        _stableContext = CreateContext();
        _changingContext = CreateContext();
        _hoverContext = CreateContext();
        _stableChart = RenderChart(_stableContext, _seriesA);
        _changingChart = RenderChart(_changingContext, _seriesA);
        _hoverChart = RenderChart(_hoverContext, _seriesA);
        _hitAreas = _hoverChart.FindAll(".quark-chart-hit-area").ToArray();
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _stableContext.Dispose();
        _changingContext.Dispose();
        _hoverContext.Dispose();
    }

    [Benchmark(Baseline = true, Description = "Chart: stable data and options")]
    public void StableParameters() => RenderChart(_stableChart, _seriesA);

    [Benchmark(Description = "Chart: changed data reference")]
    public void ChangedData()
    {
        _useAlternateSeries = !_useAlternateSeries;
        RenderChart(_changingChart, _useAlternateSeries ? _seriesB : _seriesA);
    }

    [Benchmark(Description = "Chart: hover interaction only")]
    public void Hover()
    {
        _hoverIndex = (_hoverIndex + 1) % _hitAreas.Length;
        _hitAreas[_hoverIndex].TriggerEvent("onpointerenter", new PointerEventArgs());
    }

    private IRenderedComponent<Chart> RenderChart(BunitContext context, ChartSeries[] series) =>
        context.Render<Chart>(parameters => parameters
            .Add(component => component.Labels, _labels)
            .Add(component => component.Series, series)
            .Add(component => component.Options, _options));

    private void RenderChart(IRenderedComponent<Chart> chart, ChartSeries[] series) =>
        chart.Render(parameters => parameters
            .Add(component => component.Labels, _labels)
            .Add(component => component.Series, series)
            .Add(component => component.Options, _options));

    private static BunitContext CreateContext()
    {
        var context = new BunitContext();
        context.Services.AddLogging();
        context.Services.AddDefaultQuarkOptionsAsScoped();
        return context;
    }
}
