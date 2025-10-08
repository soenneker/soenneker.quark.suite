using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>Generates simple Bootstrap CSS custom property overrides.</summary>
public static class BootstrapCssGenerator
{
    /// <summary>Generates Bootstrap CSS variable overrides using CssVariable attributes.</summary>
    public static string GenerateRootCss(BootstrapCssVariables cssVariables)
    {
        if (cssVariables == null)
            return string.Empty;

        var selectorGroups = new Dictionary<string, List<string>>();

        // Process all properties in BootstrapCssVariables that contain CSS variables
        var cssVariablesType = cssVariables.GetType();
        var properties = cssVariablesType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string));

        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(cssVariables);
            if (propertyValue != null)
            {
                ProcessCssVariableObject(propertyValue, selectorGroups);
            }
        }

        if (selectorGroups.Count == 0)
            return string.Empty;

        var css = new StringBuilder();
        foreach (var selectorGroup in selectorGroups)
        {
            css.AppendLine($"{selectorGroup.Key} {{");
            foreach (var variable in selectorGroup.Value)
            {
                css.AppendLine(variable);
            }
            css.AppendLine("}");
        }

        return css.ToString().TrimEnd();
    }

        private static void ProcessCssVariableObject(object cssVariableObject, Dictionary<string, List<string>> selectorGroups)
        {
            var type = cssVariableObject.GetType();

            // Get the CssSelector attribute from the class
            var cssSelectorAttr = type.GetCustomAttribute<CssSelectorAttribute>();
            var selector = cssSelectorAttr?.GetSelector() ?? ":root";

            // Get all properties with CssVariable attributes
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(string))
                .Where(p => p.GetCustomAttribute<CssVariableAttribute>() != null);

            foreach (var property in properties)
            {
                var value = property.GetValue(cssVariableObject) as string;
                if (value.HasContent())
                {
                    var attr = property.GetCustomAttribute<CssVariableAttribute>();
                    if (attr != null)
                    {
                        var cssVariableName = attr.GetName();
                        
                        if (!selectorGroups.ContainsKey(selector))
                            selectorGroups[selector] = [];
                        
                        selectorGroups[selector].Add($"  {cssVariableName}: {value};");
                    }
                }
            }
        }
}
