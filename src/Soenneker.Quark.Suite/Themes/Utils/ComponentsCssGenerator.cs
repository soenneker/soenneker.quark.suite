using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>Generates CSS for all component options in a Theme.</summary>
public static class ComponentsCssGenerator
{
    /// <summary>
    /// Generates CSS rules for all component options in the theme.
    /// </summary>
    /// <param name="theme">Theme for the generate operation.</param>
    /// <returns>The text produced by generate.</returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string Generate(Theme theme)
    {
        if (theme is null)
            return string.Empty;

        var css = new PooledStringBuilder(stackalloc char[128]);
        try
        {
            foreach (var componentOptions in theme.GetAllComponentOptions())
                ComponentCssGenerator.Append(ref css, componentOptions);
            return css.ToString();
        }
        finally
        {
            css.Dispose();
        }
    }
}
