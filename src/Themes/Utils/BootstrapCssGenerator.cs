using System.Collections.Generic;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>Generates simple Bootstrap CSS custom property overrides without reflection.</summary>
public static class BootstrapCssGenerator
{
    /// <summary>Generates Bootstrap CSS variable overrides using interface methods.</summary>
    public static string Generate(BootstrapCssVariables cssVariables)
    {
        if (cssVariables == null)
            return string.Empty;

        var selectorGroups = new Dictionary<string, List<string>>();

        // Process all CSS variable groups without reflection
        foreach (var group in cssVariables.GetAllCssVariableGroups())
        {
            var selector = group.GetSelector();
            var variables = group.GetCssVariables();

            foreach (var (cssPropertyName, value) in variables)
            {
                if (!selectorGroups.ContainsKey(selector))
                    selectorGroups[selector] = [];

                selectorGroups[selector].Add($"  {cssPropertyName}: {value};");
            }
        }

        if (selectorGroups.Count == 0)
            return string.Empty;

        using var css = new PooledStringBuilder();

        foreach (var selectorGroup in selectorGroups)
        {
            css.Append($"{selectorGroup.Key} {{\n");

            foreach (var variable in selectorGroup.Value)
            {
                css.Append(variable);
                css.Append('\n');
            }

            css.Append("}\n");
        }

        return css.ToString()
                  .TrimEnd();
    }
}