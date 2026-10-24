using System.Reflection;
using BenchmarkDotNet.Attributes;
using Soenneker.Bradix;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class ValueIdBenchmarks
{
    private static readonly ValueIdBuilder Build = typeof(BradixTabs).Assembly
        .GetType("Soenneker.Bradix.BradixStringCache")!
        .GetMethod("ChildValueId", BindingFlags.Static | BindingFlags.NonPublic)!
        .CreateDelegate<ValueIdBuilder>();
    private string? _cached;
    private string _value = string.Empty;

    [Params(10, 400)]
    public int Length { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        _value = new string('界', Length);
        Build("tabs-trigger", _value, ref _cached);
    }

    [Benchmark]
    public string UnchangedEscapedId() => Build("tabs-trigger", _value, ref _cached);
}
