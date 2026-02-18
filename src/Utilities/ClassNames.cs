using System;
using System.Collections;
using System.Collections.Generic;

namespace Soenneker.Quark.Utilities;

/// <summary>
/// Combines CSS class names with Tailwind conflict resolution.
/// Equivalent to shadcn's Combine() (clsx + tailwind-merge). Use for component base + Class so user overrides win.
/// </summary>
public static class ClassNames
{
    private static readonly char[] WhitespaceSeparators = [' ', '\t', '\n', '\r'];

    /// <summary>
    /// Combines multiple class names and resolves Tailwind CSS conflicts. Later inputs win on conflict.
    /// </summary>
    /// <param name="inputs">Strings, nulls, booleans (ignored), or IEnumerables. Conditional: (condition &amp;&amp; "class") works.</param>
    /// <returns>Merged class string with Tailwind conflicts resolved.</returns>
    public static string Combine(params object?[] inputs)
    {
        if (inputs == null || inputs.Length == 0) return string.Empty;

        var classes = new List<string>();
        foreach (var input in inputs)
            ProcessInput(input, classes);

        if (classes.Count == 0) return string.Empty;
        return TailwindMerge.Merge(classes.ToArray());
    }

    /// <summary>
    /// Returns the class string if condition is true, otherwise null. Use inside Combine() for conditional classes.
    /// </summary>
    public static string? When(bool condition, string className) =>
        condition ? className : null;

    private static void ProcessInput(object? input, List<string> classes)
    {
        if (input == null) return;
        if (input is string str)
        {
            if (!string.IsNullOrWhiteSpace(str))
            {
                var parts = str.Split(WhitespaceSeparators, StringSplitOptions.RemoveEmptyEntries);
                classes.AddRange(parts);
            }
            return;
        }
        if (input is bool) return;
        if (input is IEnumerable enumerable and not string)
        {
            foreach (var item in enumerable)
                ProcessInput(item, classes);
            return;
        }
        var strValue = input.ToString();
        if (!string.IsNullOrWhiteSpace(strValue))
            classes.Add(strValue!);
    }
}
