using System;
using System.Collections;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Combines CSS class names without conflict resolution.
/// </summary>
public static class ClassNames
{
    private static readonly char[] WhitespaceSeparators = [' ', '\t', '\n', '\r'];

    /// <summary>
    /// Combines multiple class names while preserving the original token order.
    /// </summary>
    /// <param name="inputs">Strings, nulls, booleans (ignored), or IEnumerables. Conditional: (condition &amp;&amp; "class") works.</param>
    /// <returns>Combined class string with original token order preserved.</returns>
    public static string Combine(params object?[] inputs)
    {
        if (inputs == null || inputs.Length == 0) return string.Empty;

        var classes = new List<string>();
        var seen = new HashSet<string>(StringComparer.Ordinal);
        foreach (var input in inputs)
            ProcessInput(input, classes, seen);

        if (classes.Count == 0) 
            return string.Empty;
        
        return string.Join(' ', classes);
    }

    /// <summary>
    /// Returns the class string if condition is true, otherwise null. Use inside Combine() for conditional classes.
    /// </summary>
    public static string? When(bool condition, string className) =>
        condition ? className : null;

    private static void ProcessInput(object? input, List<string> classes, HashSet<string> seen)
    {
        if (input == null) return;
        if (input is string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                var parts = str.Split(WhitespaceSeparators, StringSplitOptions.RemoveEmptyEntries);
                foreach (var part in parts)
                {
                    if (seen.Add(part))
                        classes.Add(part);
                }
            }
            return;
        }
        if (input is bool) return;
        if (input is IEnumerable enumerable and not string)
        {
            foreach (var item in enumerable)
                ProcessInput(item, classes, seen);
            return;
        }
        var strValue = input.ToString();
        if (!string.IsNullOrWhiteSpace(strValue) && seen.Add(strValue!))
            classes.Add(strValue!);
    }
}
