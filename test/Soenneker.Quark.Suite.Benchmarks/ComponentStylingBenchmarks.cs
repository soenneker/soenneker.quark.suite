using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class ComponentStylingBenchmarks
{
    private readonly ComponentStylingProbe _empty = new();
    private readonly ComponentStylingProbe _sparse = new(styled: true);
    private readonly ComponentStylingProbe _preset = new(usePreset: true);
    private readonly ComponentStylingProbe _dense = CreateDense(false);
    private readonly ComponentStylingProbe _densePreset = CreateDense(true);

    private readonly ComponentStylingProbe _frozenPreset = new(preset:
        QuarkPresetToken.Freeze("benchmark-frozen", static c => c.Width = Quark.Width.Token("[240px]")));

    [Benchmark] public object FrozenPreset() => _frozenPreset.Rebuild();
    [Benchmark] public object Empty() => _empty.Rebuild();
    [Benchmark] public object Sparse() => _sparse.Rebuild();
    [Benchmark] public object Preset() => _preset.Rebuild();
    [Benchmark] public object Dense() => _dense.Rebuild();
    [Benchmark] public object DenseFrozenPreset() => _densePreset.Rebuild();

    private static ComponentStylingProbe CreateDense(bool usePreset)
    {
        void Populate(object target)
        {
            foreach (var property in target.GetType().GetProperties())
            {
                if (Nullable.GetUnderlyingType(property.PropertyType) is not { IsGenericType: true } type ||
                    type.GetGenericTypeDefinition() != typeof(CssValue<>)) continue;
                var value = type.GetMethod("Raw", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)!
                    .Invoke(null, ["probe-" + property.Name]);
                property.SetValue(target, value);
            }
        }
        var probe = new ComponentStylingProbe(preset: usePreset ? QuarkPresetToken.Freeze("dense", context => Populate(context)) : null);
        if (!usePreset)
            Populate(probe);
        return probe;
    }
}
