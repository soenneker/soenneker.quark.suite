using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

internal static class ThemeSelector
{
    internal static string Resolve(string parent, string relative, bool descendant = false)
    {
        var result = new List<string>();
        foreach (var child in Split(relative))
        foreach (var scope in Split(parent))
            result.Add(child.Contains('&') ? child.Replace("&", scope, StringComparison.Ordinal)
                : !descendant && child[0] is ':' or '.' or '[' or '#' ? scope + child : scope + " " + child);
        return string.Join(", ", result);
    }

    private static IEnumerable<string> Split(string selectors)
    {
        var start = 0;
        var depth = 0;
        var quote = '\0';
        for (var i = 0; i < selectors.Length; i++)
        {
            var c = selectors[i];
            if (c == '\\') { i++; continue; }
            if (quote != '\0') { if (c == quote) quote = '\0'; continue; }
            if (c is '\'' or '"') quote = c;
            else if (c is '(' or '[') depth++;
            else if (c is ')' or ']') depth--;
            else if (c == ',' && depth == 0)
            {
                var item = selectors[start..i].Trim();
                if (item.Length > 0) yield return item;
                start = i + 1;
            }
        }
        var last = selectors[start..].Trim();
        if (last.Length > 0) yield return last;
    }
}
