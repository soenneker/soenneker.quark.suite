using BenchmarkDotNet.Attributes;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.DependencyInjection;

namespace Soenneker.Quark.Suite.Benchmarks;

/// <summary>
/// Compares the immutable cascading-parameter fast path with the conservative mutable-context path.
/// </summary>
[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 8)]
public class CascadingRenderingBenchmarks
{
    private BunitContext _immutableContext = null!;
    private BunitContext _mutableContext = null!;
    private IRenderedComponent<CascadingBenchmarkHost> _immutableHost = null!;
    private IRenderedComponent<CascadingBenchmarkHost> _mutableHost = null!;
    private int _immutableTick;
    private int _mutableTick;

    [Params(100, 500)]
    public int ComponentCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _immutableContext = CreateContext();
        _mutableContext = CreateContext();
        _immutableHost = RenderHost(_immutableContext, mutableCascade: false);
        _mutableHost = RenderHost(_mutableContext, mutableCascade: true);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _immutableContext.Dispose();
        _mutableContext.Dispose();
    }

    [Benchmark(Baseline = true, Description = "Immutable cascade: unchanged children")]
    public void ImmutableCascade() => Render(_immutableHost, ++_immutableTick);

    [Benchmark(Description = "Mutable cascade: detailed render key")]
    public void MutableCascade() => Render(_mutableHost, ++_mutableTick);

    private IRenderedComponent<CascadingBenchmarkHost> RenderHost(BunitContext context, bool mutableCascade) =>
        context.Render<CascadingBenchmarkHost>(parameters => parameters
            .Add(component => component.ComponentCount, ComponentCount)
            .Add(component => component.MutableCascade, mutableCascade));

    private void Render(IRenderedComponent<CascadingBenchmarkHost> host, int tick) =>
        host.Render(parameters => parameters
            .Add(component => component.ComponentCount, ComponentCount)
            .Add(component => component.MutableCascade, host.Instance.MutableCascade)
            .Add(component => component.Tick, tick));

    private static BunitContext CreateContext()
    {
        var context = new BunitContext();
        context.Services.AddLogging();
        context.Services.AddDefaultQuarkOptionsAsScoped();
        return context;
    }
}

public sealed class CascadingBenchmarkHost : ComponentBase
{
    private BenchmarkPayload[] _payloads = [];
    private readonly BenchmarkCascadeContext _cascadeContext = new();

    [Parameter]
    public int ComponentCount { get; set; }

    [Parameter]
    public bool MutableCascade { get; set; }

    [Parameter]
    public int Tick { get; set; }

    protected override void OnParametersSet()
    {
        if (_payloads.Length == ComponentCount)
            return;

        _payloads = new BenchmarkPayload[ComponentCount];
        for (var index = 0; index < ComponentCount; index++)
            _payloads[index] = new BenchmarkPayload(index);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (MutableCascade)
        {
            builder.OpenComponent<CascadingValue<BenchmarkCascadeContext>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<BenchmarkCascadeContext>.Value), _cascadeContext);
        }
        else
        {
            builder.OpenComponent<CascadingValue<int>>(0);
            builder.AddAttribute(1, nameof(CascadingValue<int>.Value), 42);
        }

        builder.AddAttribute(2, "ChildContent", (RenderFragment)RenderLeaves);
        builder.CloseComponent();
        builder.AddContent(3, Tick);
    }

    private void RenderLeaves(RenderTreeBuilder builder)
    {
        var componentType = MutableCascade ? typeof(MutableCascadeBenchmarkLeaf) : typeof(ImmutableCascadeBenchmarkLeaf);
        for (var index = 0; index < _payloads.Length; index++)
        {
            builder.OpenComponent(0, componentType);
            builder.SetKey(index);
            builder.AddAttribute(1, nameof(CascadeBenchmarkLeaf.Payload), _payloads[index]);
            builder.CloseComponent();
        }
    }
}

public abstract class CascadeBenchmarkLeaf : Element
{
    [Parameter]
    public BenchmarkPayload Payload { get; set; } = null!;

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "span");
        builder.AddContent(1, Payload.Value);
        builder.CloseElement();
    }
}

public sealed class ImmutableCascadeBenchmarkLeaf : CascadeBenchmarkLeaf
{
    [CascadingParameter]
    public int CascadeValue { get; set; }

    protected override void ComputeRenderKeyCore(ref HashCode hashCode)
    {
        base.ComputeRenderKeyCore(ref hashCode);
        hashCode.Add(CascadeValue);
        hashCode.Add(Payload);
    }
}

public sealed class MutableCascadeBenchmarkLeaf : CascadeBenchmarkLeaf
{
    [CascadingParameter]
    public BenchmarkCascadeContext CascadeContext { get; set; } = null!;

    protected override void ComputeRenderKeyCore(ref HashCode hashCode)
    {
        base.ComputeRenderKeyCore(ref hashCode);
        hashCode.Add(CascadeContext.Version);
        hashCode.Add(Payload);
    }
}

public sealed class BenchmarkCascadeContext
{
    public int Version { get; set; }
}
