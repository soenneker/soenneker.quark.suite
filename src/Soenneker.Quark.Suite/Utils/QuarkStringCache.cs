using System;

namespace Soenneker.Quark;

internal static class QuarkStringCache
{
    internal static string Concat(string first, string separator, string second, ref string? previous)
    {
        if (previous is null || previous.Length != first.Length + separator.Length + second.Length ||
            !previous.AsSpan(0, first.Length).SequenceEqual(first.AsSpan()) ||
            !previous.AsSpan(first.Length, separator.Length).SequenceEqual(separator.AsSpan()) ||
            !previous.AsSpan(first.Length + separator.Length).SequenceEqual(second.AsSpan()))
            previous = string.Concat(first, separator, second);

        return previous;
    }
}
