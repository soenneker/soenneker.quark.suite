using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark.Themes.Utils;

/// <summary>Generates CSS for all component options in a Theme.</summary>
public static class ComponentsCssGenerator
{
    /// <summary>Generates CSS rules for all component options in the theme.</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Generate(Theme theme)
    {
        if (theme is null)
            return string.Empty;

        using var css = new PooledStringBuilder();

        var themeType = typeof(Theme);
        var properties = themeType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var property in properties)
        {
            // Skip BootstrapCssVariables and Name property
            if (property.Name == nameof(Theme.BootstrapCssVariables) || property.Name == nameof(Theme.Name))
                continue;

            // Check if property type is ComponentOptions or derived from it
            var propType = property.PropertyType;
            var underlying = Nullable.GetUnderlyingType(propType) ?? propType;

            if (!typeof(ComponentOptions).IsAssignableFrom(underlying))
                continue;

            // Get the component options value
            var componentOptions = property.GetValue(theme) as ComponentOptions;

            if (componentOptions is null)
                continue;

            // Generate CSS for this component and append if not empty
            var componentCss = ComponentCssGenerator.Generate(componentOptions);
            if (componentCss.IsNullOrEmpty())
                continue;

            // Append the CSS with a newline separator
            if (css.Length > 0)
                css.Append('\n');

            css.Append(componentCss);
        }

        return css.ToString();
    }
}

