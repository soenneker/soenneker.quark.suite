using BenchmarkDotNet.Attributes;
using Soenneker.Bradix;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Suite.Benchmarks;

// No renderer, DOM, parameter-builder expressions or JS mocks in these measurements.
[MemoryDiagnoser]
public class BradixAttributeBenchmarks
{
    private readonly BradixAttributeProbe _probe = new();

    [Benchmark(Baseline = true)]
    public object ExplicitArray() => _probe.BuildArray();

    [Benchmark]
    public object ParamsSpan() => _probe.BuildSpan();
}

public sealed class BradixAttributeProbe : BradixElement
{
    public object BuildArray() => BuildAttributes(new (string Key, object? Value)[]
    {
        ("role", "option"), ("aria-labelledby", "label"), ("aria-selected", "false"),
        ("aria-disabled", null), ("data-value", "value"), ("data-state", "unchecked"),
        ("data-highlighted", null), ("data-disabled", null), ("tabindex", "-1")
    });

    public object BuildSpan() => BuildAttributes(
        ("role", "option"), ("aria-labelledby", "label"), ("aria-selected", "false"),
        ("aria-disabled", null), ("data-value", "value"), ("data-state", "unchecked"),
        ("data-highlighted", null), ("data-disabled", null), ("tabindex", "-1"));
}

[MemoryDiagnoser]
public class QuarkAttributeBenchmarks
{
    private readonly QuarkAttributeProbe _probe = new();
    private bool _alternate;

    [Benchmark]
    public object UnchangedVisualOutput() => _probe.Rebuild();

    [Benchmark]
    public object ChangedVisualOutput()
    {
        _alternate = !_alternate;
        _probe.VisualClass = _alternate ? "benchmark-a" : "benchmark-b";
        return _probe.Rebuild();
    }
}

public sealed class QuarkAttributeProbe : RenderComponent
{
    public string VisualClass { get; set; } = "benchmark-a";
    public object Rebuild() => BuildAttributes();

    protected override void BuildOwnedClassAndStyle(ref PooledStringBuilder style, ref PooledStringBuilder classes)
    {
        AppendClass(ref classes, VisualClass);
        AppendStyleDecl(ref style, "opacity: 1");
    }
}

[MemoryDiagnoser]
public class StaticClassBenchmarks
{
    private readonly StaticClassProbe _probe = new();

    [Params(null, "consumer-class")]
    public string? ExistingClass { get; set; }

    [Benchmark(Baseline = true)]
    public object PooledBuilder() => _probe.Build(ExistingClass, false);

    [Benchmark]
    public object LiteralPrefix() => _probe.Build(ExistingClass, true);
}

public sealed class StaticClassProbe : RenderComponent
{
    private readonly Dictionary<string, object> _attributes = new(1);

    public object Build(string? existingClass, bool literal)
    {
        _attributes.Clear();
        if (existingClass is not null)
            _attributes["class"] = existingClass;

        if (literal)
            PrependClassAttribute(_attributes, "flex items-center gap-2 rounded-lg border border-input px-3 py-2");
        else
            BuildClassAttribute(_attributes, static (ref PooledStringBuilder cls) =>
                AppendClass(ref cls, "flex items-center gap-2 rounded-lg border border-input px-3 py-2"));

        return _attributes;
    }
}
