using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

// Measures component objects only, before renderer, DI, attributes, or DOM allocation.
#pragma warning disable BL0005 // Construction is the subject of these benchmarks.
[MemoryDiagnoser]
public class ComponentConstructionBenchmarks
{
    [Benchmark(Baseline = true)]
    public Div PlainDiv() => new();

    [Benchmark]
    public Button PlainButton() => new();

    [Benchmark]
    public Div AccessibleDiv() => new() { Role = "region", AriaLabel = "Messages", Lang = "en", Dir = "ltr" };

    [Benchmark]
    public Div EditingDiv() => new() { ContentEditable = "true", SpellCheck = true, EnterKeyHint = "send" };

    [Benchmark]
    public Div LiveRegion() => new() { AriaLive = "polite", AriaAtomic = true, AriaBusy = false };

    [Benchmark]
    public Div MicrodataDiv() => new() { ItemScope = true, ItemType = "https://schema.org/Thing", ItemProp = "name" };

    [Benchmark]
    public Div LogicalLayout() => new() { InlineSize = "inline-size-full", InsetStart = "start-0" };
}
#pragma warning restore BL0005
