using System;
using System.Globalization;

namespace Soenneker.Quark;

internal sealed class ComparisonChartTickCache
{
    private long _minimum;
    private long _maximum;
    private bool _logarithmic;
    private bool _initialized;

    public ComparisonChartTick[] Ticks { get; private set; } = [];

    public void Update(ComparisonChartMetric metric)
    {
        int intervals = metric.ShowMarker ? 2 : 4;
        int count = metric.Ticks?.Count ?? intervals + 1;
        long minimumBits = BitConverter.DoubleToInt64Bits(metric.Minimum);
        long maximumBits = BitConverter.DoubleToInt64Bits(metric.Maximum);
        bool scaleChanged = !_initialized || _minimum != minimumBits || _maximum != maximumBits || _logarithmic != metric.Logarithmic;
        if (Ticks.Length != count)
        {
            Ticks = new ComparisonChartTick[count];
            scaleChanged = true;
        }

        double minimumLog = metric.Logarithmic ? Math.Log(metric.Minimum) : 0;
        double logRange = metric.Logarithmic ? Math.Log(metric.Maximum) - minimumLog : 0;
        double range = metric.Maximum - metric.Minimum;
        for (int index = 0; index < count; index++)
        {
            double value = metric.Ticks is { } custom ? custom[index] : metric.Logarithmic
                ? Math.Exp(minimumLog + logRange * index / intervals)
                : metric.Minimum + range * index / intervals;
            if (!scaleChanged && BitConverter.DoubleToInt64Bits(Ticks[index].Value) == BitConverter.DoubleToInt64Bits(value))
                continue;

            double clamped = Math.Clamp(value, metric.Minimum, metric.Maximum);
            double fraction = metric.Logarithmic ? (Math.Log(clamped) - minimumLog) / logRange : (clamped - metric.Minimum) / range;
            Ticks[index] = new ComparisonChartTick(value,
                "--tick:" + (fraction * 100).ToString("0.####", CultureInfo.InvariantCulture) + "%",
                value == metric.Minimum, value == metric.Minimum ? "start" : value == metric.Maximum ? "end" : null);
        }

        _minimum = minimumBits;
        _maximum = maximumBits;
        _logarithmic = metric.Logarithmic;
        _initialized = true;
    }
}
