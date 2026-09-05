using BenchmarkDotNet.Attributes;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Benchmarks;

/// <summary>
/// Compares Quark's global render-key suppression with normal Quark rendering and a plain Blazor component.
/// Each operation rerenders a parent containing many leaf components, which is the scenario where global
/// suppression is intended to recover its render-key computation cost.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 8)]
public class GlobalRenderingBenchmarks
{
    private BunitContext _plainContext = null!;
    private BunitContext _bufferedContext = null!;
    private BunitContext _suppressedContext = null!;
    private IRenderedComponent<RenderBenchmarkHost> _plain = null!;
    private IRenderedComponent<RenderBenchmarkHost> _buffered = null!;
    private IRenderedComponent<RenderBenchmarkHost> _suppressed = null!;
    private int _plainTick;
    private int _bufferedTick;
    private int _suppressedTick;

    [Params(100, 500)]
    public int ComponentCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _plainContext = CreateContext();
        _bufferedContext = CreateContext();
        _suppressedContext = CreateContext();
        _plain = RenderHost(_plainContext, RenderBenchmarkMode.PlainBlazor);
        _buffered = RenderHost(_bufferedContext, RenderBenchmarkMode.QuarkBuffered);
        _suppressed = RenderHost(_suppressedContext, RenderBenchmarkMode.QuarkSuppressed);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _plainContext.Dispose();
        _bufferedContext.Dispose();
        _suppressedContext.Dispose();
    }

    [Benchmark(Baseline = true, Description = "Plain Blazor: unchanged children")]
    public void PlainBlazorUnchanged() => Render(_plain, ++_plainTick, valuesChange: false);

    [Benchmark(Description = "Quark buffered: unchanged children")]
    public void QuarkBufferedUnchanged() => Render(_buffered, ++_bufferedTick, valuesChange: false);

    [Benchmark(Description = "Quark suppressed: unchanged children")]
    public void QuarkSuppressedUnchanged() => Render(_suppressed, ++_suppressedTick, valuesChange: false);

    [Benchmark(Description = "Plain Blazor: changing children")]
    public void PlainBlazorChanging() => Render(_plain, ++_plainTick, valuesChange: true);

    [Benchmark(Description = "Quark buffered: changing children")]
    public void QuarkBufferedChanging() => Render(_buffered, ++_bufferedTick, valuesChange: true);

    [Benchmark(Description = "Quark suppressed: changing children")]
    public void QuarkSuppressedChanging() => Render(_suppressed, ++_suppressedTick, valuesChange: true);

    private IRenderedComponent<RenderBenchmarkHost> RenderHost(BunitContext context, RenderBenchmarkMode mode) =>
        context.Render<RenderBenchmarkHost>(parameters => parameters
            .Add(component => component.ComponentCount, ComponentCount)
            .Add(component => component.Mode, mode));

    private void Render(IRenderedComponent<RenderBenchmarkHost> component, int tick, bool valuesChange) =>
        component.Render(parameters => parameters
            .Add(host => host.ComponentCount, ComponentCount)
            .Add(host => host.Mode, component.Instance.Mode)
            .Add(host => host.Tick, tick)
            .Add(host => host.ValuesChange, valuesChange));

    private static BunitContext CreateContext()
    {
        var context = new BunitContext();
        context.Services.AddLogging();
        context.Services.AddDefaultQuarkOptionsAsScoped();
        return context;
    }
}

public enum RenderBenchmarkMode
{
    PlainBlazor,
    QuarkBuffered,
    QuarkSuppressed
}

public sealed class RenderBenchmarkHost : ComponentBase
{
    private BenchmarkPayload[] _payloads = [];

    [Parameter]
    public int ComponentCount { get; set; }

    [Parameter]
    public RenderBenchmarkMode Mode { get; set; }

    [Parameter]
    public int Tick { get; set; }

    [Parameter]
    public bool ValuesChange { get; set; }

    protected override void OnParametersSet()
    {
        if (_payloads.Length == ComponentCount)
            return;

        _payloads = new BenchmarkPayload[ComponentCount];
        for (var index = 0; index < _payloads.Length; index++)
            _payloads[index] = new BenchmarkPayload(index);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        var componentType = Mode switch
        {
            RenderBenchmarkMode.PlainBlazor => typeof(PlainBlazorLeaf),
            RenderBenchmarkMode.QuarkBuffered => typeof(QuarkBufferedLeaf),
            _ => typeof(QuarkSuppressedLeaf)
        };

        for (var index = 0; index < ComponentCount; index++)
        {
            builder.OpenComponent(0, componentType);
            builder.SetKey(index);
            builder.AddAttribute(1, nameof(PlainBlazorLeaf.Value), ValuesChange ? index + Tick : index);
            builder.AddAttribute(2, nameof(PlainBlazorLeaf.Payload), _payloads[index]);
            builder.CloseComponent();
        }
    }
}

public sealed class PlainBlazorLeaf : ComponentBase
{
    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public BenchmarkPayload Payload { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddAttribute(1, "class", "benchmark-leaf");
        builder.AddContent(2, Value + Payload.Value);
        builder.CloseElement();
    }
}

public abstract class QuarkBenchmarkLeaf : Element
{
    [Parameter]
    public int Value { get; set; }

    [Parameter]
    public BenchmarkPayload Payload { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddMultipleAttributes(1, BuildAttributes());
        builder.AddContent(2, Value + Payload.Value);
        builder.CloseElement();
    }

    protected override void BuildOwnedClassAndStyle(ref Soenneker.Utils.PooledStringBuilders.PooledStringBuilder sty,
        ref Soenneker.Utils.PooledStringBuilders.PooledStringBuilder cls)
    {
        base.BuildOwnedClassAndStyle(ref sty, ref cls);
        AppendClass(ref cls, "benchmark-leaf");
    }
}

public sealed class QuarkBufferedLeaf : QuarkBenchmarkLeaf
{
    protected override bool AlwaysRender => true;
}

public sealed class QuarkSuppressedLeaf : QuarkBenchmarkLeaf
{
    protected override bool AlwaysRender => false;

    protected override void ComputeRenderKeyCore(ref HashCode hashCode)
    {
        base.ComputeRenderKeyCore(ref hashCode);
        hashCode.Add(Value);
        hashCode.Add(Payload);
    }
}

public sealed class BenchmarkPayload(int value)
{
    public int Value { get; set; } = value;
}
