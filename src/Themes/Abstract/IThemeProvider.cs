using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Interface for providing theme functionality, including theme management and CSS generation.
/// </summary>
public interface IThemeProvider
{
    /// <summary>
    /// Gets or sets the name of the currently active theme.
    /// </summary>
    string? CurrentTheme { get; set; }

    /// <summary>
    /// Gets or sets the dictionary of available themes, keyed by theme name.
    /// </summary>
    Dictionary<string, Theme>? Themes { get; set; }

    /// <summary>
    /// Generates Bootstrap CSS from the current theme configuration.
    /// </summary>
    /// <returns>The generated Bootstrap CSS string, or null if generation fails.</returns>
    string? GenerateBootstrapCss();

    /// <summary>
    /// Generates component-specific CSS from the current theme configuration.
    /// </summary>
    /// <returns>The generated component CSS string, or null if generation fails.</returns>
    string? GenerateComponentsCss();
}
