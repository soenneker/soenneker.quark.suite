using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class ComponentStylingBenchmarks
{
    private readonly ComponentStylingProbe _empty = new();
    private readonly ComponentStylingProbe _sparse = new(styled: true);
    private readonly ComponentStylingProbe _preset = new(usePreset: true);

    private readonly ComponentStylingProbe _frozenPreset = new()
    {
        Preset = QuarkPresetToken.Freeze("benchmark-frozen", static c => c.Width = Quark.Width.Token("[240px]"))
    };

    [Benchmark] public object FrozenPreset() => _frozenPreset.Rebuild();
    [Benchmark] public object Empty() => _empty.Rebuild();
    [Benchmark] public object Sparse() => _sparse.Rebuild();
    [Benchmark] public object Preset() => _preset.Rebuild();
}
