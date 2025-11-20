using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

///<inheritdoc cref="IThemeProvider"/>
public sealed class ThemeProvider : IThemeProvider
{
    /// <summary>
    /// Gets or sets the name of the currently active theme.
    /// </summary>
    public string? CurrentTheme { get; set; } = "Default";

    /// <summary>
    /// Gets or sets the dictionary of available themes, keyed by theme name.
    /// </summary>
    public Dictionary<string, Theme>? Themes { get; set; }

    /// <summary>
    /// Adds a theme to the theme collection.
    /// </summary>
    /// <param name="theme">The theme to add.</param>
    public void AddTheme(Theme theme)
    {
        Themes ??= new Dictionary<string, Theme>();
        Themes[theme.Name] = theme;
    }

    /// <summary>
    /// Generates Bootstrap CSS from the current theme configuration.
    /// </summary>
    /// <returns>The generated Bootstrap CSS string, or null if generation fails.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? GenerateBootstrapCss()
    {
        var currentTheme = GetCurrentTheme();

        if (currentTheme?.BootstrapCssVariables == null)
            return null;

        return BootstrapCssGenerator.Generate(currentTheme.BootstrapCssVariables);
    }

    /// <summary>
    /// Generates component-specific CSS from the current theme configuration.
    /// </summary>
    /// <returns>The generated component CSS string, or null if generation fails.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string? GenerateComponentsCss()
    {
        var currentTheme = GetCurrentTheme();

        if (currentTheme == null)
            return null;

        return ComponentsCssGenerator.Generate(currentTheme);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Theme? GetCurrentTheme()
    {
        if (CurrentTheme.IsNullOrEmpty() || Themes == null)
            return null;

        return Themes.GetValueOrDefault(CurrentTheme);
    }
}
