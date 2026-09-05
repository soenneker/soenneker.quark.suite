using BenchmarkDotNet.Attributes;

namespace Soenneker.Quark.Suite.Benchmarks;

[MemoryDiagnoser]
public class RealtimeAppendBenchmarks
{
    private readonly RealtimeChartData _data = new(1000, "API", "Worker");
    private readonly DateTimeOffset[] _timestamps = new DateTimeOffset[16];
    private readonly double?[] _batchValues = new double?[32];
    private long _tick;

    [Benchmark(Baseline = true)]
    public void ExplicitArray() => _data.Append(DateTimeOffset.FromUnixTimeMilliseconds(++_tick), new double?[] { 1, 2 });

    [Benchmark]
    public void ParamsSpan() => _data.Append(DateTimeOffset.FromUnixTimeMilliseconds(++_tick), 1, 2);

    [Benchmark(OperationsPerInvoke = 16)]
    public void Batch()
    {
        for (var index = 0; index < _timestamps.Length; index++)
            _timestamps[index] = DateTimeOffset.FromUnixTimeMilliseconds(++_tick);
        _data.AppendBatch(_timestamps, _batchValues);
    }
}
