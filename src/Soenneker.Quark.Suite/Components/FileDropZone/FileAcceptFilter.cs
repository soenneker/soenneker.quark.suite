using System;

namespace Soenneker.Quark;

internal static class FileAcceptFilter
{
    internal static bool Matches(string? accept, string name, string? contentType)
    {
        if (string.IsNullOrWhiteSpace(accept)) return true;
        ReadOnlySpan<char> rules = accept.AsSpan();
        foreach (var range in rules.Split(','))
        {
            ReadOnlySpan<char> rule = rules[range].Trim();
            if (rule.IsEmpty) continue;
            if (rule[0] == '.' ? name.AsSpan().EndsWith(rule, StringComparison.OrdinalIgnoreCase) :
                rule.EndsWith("/*", StringComparison.Ordinal) ? contentType.AsSpan().StartsWith(rule[..^1], StringComparison.OrdinalIgnoreCase) :
                contentType.AsSpan().Equals(rule, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }
}
