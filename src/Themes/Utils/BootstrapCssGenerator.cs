using System.Collections.Generic;
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
        foreach (IBootstrapCssVariableGroup group in cssVariables.GetAllCssVariableGroups())
        {
            string selector = group.GetSelector();
            IEnumerable<(string CssPropertyName, string Value)> variables = group.GetCssVariables();

            foreach ((string cssPropertyName, string value) in variables)
            {
                if (!selectorGroups.ContainsKey(selector))
                    selectorGroups[selector] = [];

                selectorGroups[selector].Add($"  {cssPropertyName}: {value};");
            }
        }

        if (selectorGroups.Count == 0)
            return string.Empty;

        using var css = new PooledStringBuilder();

        foreach (KeyValuePair<string, List<string>> selectorGroup in selectorGroups)
        {
            css.Append($"{selectorGroup.Key} {{\n");

            foreach (string variable in selectorGroup.Value)
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