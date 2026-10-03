using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class CombinedClassBenchmarks
{
    private readonly CapturingClassProbe _probe = new();

    [Benchmark(Baseline = true)]
    public object DelegateBuilder() => _probe.WithCombinedDelegate();

    [Benchmark]
    public object DirectBuilder() => _probe.WithCombinedDirectBuilder();
}
