using System;
using System.Globalization;
using System.Text;

namespace Soenneker.Quark;

internal static class ChartNumberFormatting
{
    internal const string TotalOutOfRange = "The chart total exceeds the supported numeric range.";

    internal static double Fraction(double value, double low, double high)
    {
        if (high == low) return .5;
        double range = high - low;
        return double.IsFinite(range) ? (value - low) / range
            : (value * .5 - low * .5) / (high * .5 - low * .5);
    }

    internal static string Format(double value)
    {
        Span<char> buffer = stackalloc char[32];
        return TryFormat(value, buffer, out int length)
            ? new string(buffer[..length])
            : value.ToString("0.###", CultureInfo.InvariantCulture);
    }

    internal static StringBuilder AppendChartNumber(this StringBuilder builder, double value)
    {
        Span<char> buffer = stackalloc char[32];
        return TryFormat(value, buffer, out int length)
            ? builder.Append(buffer[..length])
            : builder.Append(value.ToString("0.###", CultureInfo.InvariantCulture));
    }

    private static bool TryFormat(double value, Span<char> buffer, out int length)
    {
        return value.TryFormat(buffer, out length, "0.###", CultureInfo.InvariantCulture);
    }
}
