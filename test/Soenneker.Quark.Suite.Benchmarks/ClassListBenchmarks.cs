using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class ClassListBenchmarks
{
    private readonly ClassListProbe _probe = new();
    [Benchmark] public object MultipleClasses() => _probe.Build();
}
