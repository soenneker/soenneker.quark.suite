using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class CapturingClassBenchmarks
{
    private readonly CapturingClassProbe _probe = new();

    [Benchmark(Baseline = true)]
    public object DelegateBuilder() => _probe.WithDelegate();

    [Benchmark]
    public object DirectBuilder() => _probe.WithDirectBuilder();

    [Benchmark]
    public object StackBuilder() => _probe.WithStackBuilder();
}
