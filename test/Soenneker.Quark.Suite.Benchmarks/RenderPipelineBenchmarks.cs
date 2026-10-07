using BenchmarkDotNet.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class RenderPipelineBenchmarks
{
    private ServiceProvider _services = null!;
    private BatchCountingRenderer _renderer = null!;
    private readonly Dictionary<string, object?> _parameters = new();
    private Func<Task> _render = null!;
    private int _root;
    private int _tick;

    [Params(100, 500)] public int ComponentCount { get; set; }
    [ParamsAllValues] public RenderBenchmarkMode Mode { get; set; }
    [Params(false, true)] public bool ValuesChange { get; set; }
    public int UpdatedComponents => _renderer.UpdatedComponents;
    public int ReferenceFrames => _renderer.ReferenceFrames;

    [GlobalSetup]
    public void Setup()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDefaultQuarkOptionsAsScoped();
        _services = services.BuildServiceProvider();
        _renderer = new BatchCountingRenderer(_services, _services.GetRequiredService<ILoggerFactory>());
        _parameters[nameof(RenderBenchmarkHost.ComponentCount)] = ComponentCount;
        _parameters[nameof(RenderBenchmarkHost.Mode)] = Mode;
        _parameters[nameof(RenderBenchmarkHost.ValuesChange)] = ValuesChange;
        _root = _renderer.Attach(new RenderBenchmarkHost());
        _render = () => _renderer.Render(_root, ParameterView.FromDictionary(_parameters));
        Render().GetAwaiter().GetResult();
    }

    [Benchmark]
    public Task Render()
    {
        _parameters[nameof(RenderBenchmarkHost.Tick)] = ++_tick;
        _renderer.ResetCounters();
        return _renderer.Dispatcher.InvokeAsync(_render);
    }

    [GlobalCleanup]
    public void Cleanup()
    {
        _renderer.Dispose();
        _services.Dispose();
    }
}
