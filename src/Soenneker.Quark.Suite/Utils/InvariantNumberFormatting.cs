using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

internal static class InvariantNumberFormatting
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void AppendInvariant(this ref PooledStringBuilder builder, double value)
    {
        Span<char> buffer = stackalloc char[32];
        if (value.TryFormat(buffer, out var length, "0.###", CultureInfo.InvariantCulture))
            builder.Append(buffer[..length]);
        else
            builder.Append(value.ToString("0.###", CultureInfo.InvariantCulture));
    }

    internal static void AppendInvariant(this ref PooledStringBuilder builder, int value)
    {
        Span<char> buffer = stackalloc char[11];
        value.TryFormat(buffer, out var length, provider: CultureInfo.InvariantCulture);
        builder.Append(buffer[..length]);
    }
}
