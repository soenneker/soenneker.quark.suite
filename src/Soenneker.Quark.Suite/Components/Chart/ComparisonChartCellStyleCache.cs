using System;
using System.Globalization;

namespace Soenneker.Quark;

internal struct ComparisonChartCellStyleCache
{
    private long _minimum;
    private long _maximum;
    private long _value;
    private long _upper;
    private bool _logarithmic;
    private string? _color;
    private string? _style;

    internal double UpperFraction { get; private set; }

    internal string GetStyle(ComparisonChartRow row, ComparisonChartMetric metric, double value, double? upper)
    {
        long minimumBits = BitConverter.DoubleToInt64Bits(metric.Minimum);
        long maximumBits = BitConverter.DoubleToInt64Bits(metric.Maximum);
        long valueBits = BitConverter.DoubleToInt64Bits(value);
        long upperBits = BitConverter.DoubleToInt64Bits(upper ?? value);
        if (_style is not null && _minimum == minimumBits && _maximum == maximumBits && _value == valueBits &&
            _upper == upperBits && _logarithmic == metric.Logarithmic && string.Equals(_color, row.Color, StringComparison.Ordinal))
            return _style;

        _minimum = minimumBits;
        _maximum = maximumBits;
        _value = valueBits;
        _upper = upperBits;
        _logarithmic = metric.Logarithmic;
        _color = row.Color;
        double minimum = metric.Logarithmic ? Math.Log(metric.Minimum) : metric.Minimum;
        double range = (metric.Logarithmic ? Math.Log(metric.Maximum) : metric.Maximum) - minimum;
        double clamped = Math.Clamp(value, metric.Minimum, metric.Maximum);
        double position = ((metric.Logarithmic ? Math.Log(clamped) : clamped) - minimum) / range;
        double clampedUpper = Math.Clamp(upper ?? value, metric.Minimum, metric.Maximum);
        UpperFraction = ((metric.Logarithmic ? Math.Log(clampedUpper) : clampedUpper) - minimum) / range;
        _style = string.Create(CultureInfo.InvariantCulture,
            $"--comparison-position:{position * 100:0.####}%;--comparison-upper:{UpperFraction * 100:0.####}%;{(row.Color is null ? string.Empty : "--comparison-color:")}{row.Color}{(row.Color is null ? string.Empty : ";")}");
        return _style;
    }
}
