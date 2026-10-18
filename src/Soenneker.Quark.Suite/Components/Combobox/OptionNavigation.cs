using System;
using System.Collections.Generic;
namespace Soenneker.Quark;

internal static class OptionNavigation
{
    internal static int IndexOf(IReadOnlyList<string>? values, string value, StringComparison comparison)
    {
        if (values is not null)
            for (var i = 0; i < values.Count; i++)
                if (string.Equals(values[i], value, comparison)) return i;
        return -1;
    }

    internal static bool Matches(string label, string? value, string? query) =>
        string.IsNullOrEmpty(query) || label.Contains(query, StringComparison.OrdinalIgnoreCase) ||
        (value?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false);

    internal static int Move(int current, int count, string key)
    {
        if (count == 0) return -1;
        return key switch
        {
            "Home" => 0,
            "End" => count - 1,
            "ArrowUp" when current < 0 => count - 1,
            "ArrowDown" when current < 0 => 0,
            "ArrowUp" => (current - 1 + count) % count,
            "ArrowDown" => (current + 1) % count,
            _ => current
        };
    }
}
