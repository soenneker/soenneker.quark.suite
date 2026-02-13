using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

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

        foreach (ComponentOptions componentOptions in theme.GetAllComponentOptions())
        {
            // Generate CSS for this component and append if not empty
            string componentCss = ComponentCssGenerator.Generate(componentOptions);

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