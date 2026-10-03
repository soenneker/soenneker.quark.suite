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
        if (!double.IsFinite(value) || Math.Abs(value) >= 1_000_000_000)
            return value.TryFormat(buffer, out length, "0.###", CultureInfo.InvariantCulture);

        var scaled = (long)Math.Round(Math.Abs(Math.Round(value, 3, MidpointRounding.AwayFromZero)) * 1000);
        int start = value < 0 ? 1 : 0;
        if (start != 0)
            buffer[0] = '-';
        (scaled / 1000).TryFormat(buffer[start..], out length, provider: CultureInfo.InvariantCulture);
        length += start;
        int fraction = (int)(scaled % 1000);
        if (fraction != 0)
        {
            buffer[length++] = '.';
            buffer[length++] = (char)('0' + fraction / 100);
            if (fraction % 100 != 0)
            {
                buffer[length++] = (char)('0' + fraction / 10 % 10);
                if (fraction % 10 != 0)
                    buffer[length++] = (char)('0' + fraction % 10);
            }
        }
        return true;
    }
}
