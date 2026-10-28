using System;
using System.Globalization;
using System.Text;

namespace Soenneker.Quark;

internal static class ChartNumberFormatting
{
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
