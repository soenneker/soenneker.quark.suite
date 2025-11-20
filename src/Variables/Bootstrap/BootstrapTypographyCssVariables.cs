using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's typography CSS variables
/// </summary>
public sealed class BootstrapTypographyCssVariables : IBootstrapCssVariableGroup
{
    // Font Families
    /// <summary>
    /// Sans-serif font family. Default: system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", "Noto Sans", "Liberation Sans", Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"
    /// </summary>
    public string? FontSansSerif { get; set; }

    /// <summary>
    /// Monospace font family. Default: SFMono-Regular, Menlo, Monaco, Consolas, "Liberation Mono", "Courier New", monospace
    /// </summary>
    public string? FontMonospace { get; set; }

    // Body Typography
    /// <summary>
    /// Body font family. Default: var(--bs-font-sans-serif)
    /// </summary>
    public string? BodyFontFamily { get; set; }

    /// <summary>
    /// Body font size. Default: 1rem
    /// </summary>
    public string? BodyFontSize { get; set; }

    /// <summary>
    /// Body font weight. Default: 400
    /// </summary>
    public string? BodyFontWeight { get; set; }

    /// <summary>
    /// Body line height. Default: 1.5
    /// </summary>
    public string? BodyLineHeight { get; set; }

    /// <summary>
    /// Body color. Default: #212529
    /// </summary>
    public string? BodyColor { get; set; }

    /// <summary>
    /// Body color RGB values. Default: 33, 37, 41
    /// </summary>
    public string? BodyColorRgb { get; set; }

    /// <summary>
    /// Body background color. Default: #fff
    /// </summary>
    public string? BodyBg { get; set; }

    /// <summary>
    /// Body background color RGB values. Default: 255, 255, 255
    /// </summary>
    public string? BodyBgRgb { get; set; }

    // Emphasis and Secondary Colors
    /// <summary>
    /// Emphasis color. Default: #000
    /// </summary>
    public string? EmphasisColor { get; set; }

    /// <summary>
    /// Emphasis color RGB values. Default: 0, 0, 0
    /// </summary>
    public string? EmphasisColorRgb { get; set; }

    /// <summary>
    /// Secondary color. Default: rgba(33, 37, 41, 0.75)
    /// </summary>
    public string? SecondaryColor { get; set; }

    /// <summary>
    /// Secondary color RGB values. Default: 33, 37, 41
    /// </summary>
    public string? SecondaryColorRgb { get; set; }

    /// <summary>
    /// Secondary background color. Default: #e9ecef
    /// </summary>
    public string? SecondaryBg { get; set; }

    /// <summary>
    /// Secondary background color RGB values. Default: 233, 236, 239
    /// </summary>
    public string? SecondaryBgRgb { get; set; }

    /// <summary>
    /// Tertiary color. Default: rgba(33, 37, 41, 0.5)
    /// </summary>
    public string? TertiaryColor { get; set; }

    /// <summary>
    /// Tertiary color RGB values. Default: 33, 37, 41
    /// </summary>
    public string? TertiaryColorRgb { get; set; }

    /// <summary>
    /// Tertiary background color. Default: #f8f9fa
    /// </summary>
    public string? TertiaryBg { get; set; }

    /// <summary>
    /// Tertiary background color RGB values. Default: 248, 249, 250
    /// </summary>
    public string? TertiaryBgRgb { get; set; }

    // Heading and Link Colors
    /// <summary>
    /// Heading color. Default: inherit
    /// </summary>
    public string? HeadingColor { get; set; }

    /// <summary>
    /// Link color. Default: #0d6efd
    /// </summary>
    public string? LinkColor { get; set; }

    /// <summary>
    /// Link color RGB values. Default: 13, 110, 253
    /// </summary>
    public string? LinkColorRgb { get; set; }

    /// <summary>
    /// Link decoration. Default: underline
    /// </summary>
    public string? LinkDecoration { get; set; }

    /// <summary>
    /// Link hover color. Default: #0a58ca
    /// </summary>
    public string? LinkHoverColor { get; set; }

    /// <summary>
    /// Link hover color RGB values. Default: 10, 88, 202
    /// </summary>
    public string? LinkHoverColorRgb { get; set; }

    /// <summary>
    /// Link hover decoration. Default: underline
    /// </summary>
    public string? LinkHoverDecoration { get; set; }

    // Code and Highlight Colors
    /// <summary>
    /// Code color. Default: #d63384
    /// </summary>
    public string? CodeColor { get; set; }

    /// <summary>
    /// Highlight color. Default: #212529
    /// </summary>
    public string? HighlightColor { get; set; }

    /// <summary>
    /// Highlight background color. Default: #fff3cd
    /// </summary>
    public string? HighlightBg { get; set; }

    /// <summary>
    /// Body text align. Default: left
    /// </summary>
    public string? BodyTextAlign { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (FontSansSerif.HasContent())
            yield return ("--bs-font-sans-serif", FontSansSerif);
        if (FontMonospace.HasContent())
            yield return ("--bs-font-monospace", FontMonospace);
        if (BodyFontFamily.HasContent())
            yield return ("--bs-body-font-family", BodyFontFamily);
        if (BodyFontSize.HasContent())
            yield return ("--bs-body-font-size", BodyFontSize);
        if (BodyFontWeight.HasContent())
            yield return ("--bs-body-font-weight", BodyFontWeight);
        if (BodyLineHeight.HasContent())
            yield return ("--bs-body-line-height", BodyLineHeight);
        if (BodyColor.HasContent())
            yield return ("--bs-body-color", BodyColor);
        if (BodyColorRgb.HasContent())
            yield return ("--bs-body-color-rgb", BodyColorRgb);
        if (BodyBg.HasContent())
            yield return ("--bs-body-bg", BodyBg);
        if (BodyBgRgb.HasContent())
            yield return ("--bs-body-bg-rgb", BodyBgRgb);
        if (EmphasisColor.HasContent())
            yield return ("--bs-emphasis-color", EmphasisColor);
        if (EmphasisColorRgb.HasContent())
            yield return ("--bs-emphasis-color-rgb", EmphasisColorRgb);
        if (SecondaryColor.HasContent())
            yield return ("--bs-secondary-color", SecondaryColor);
        if (SecondaryColorRgb.HasContent())
            yield return ("--bs-secondary-color-rgb", SecondaryColorRgb);
        if (SecondaryBg.HasContent())
            yield return ("--bs-secondary-bg", SecondaryBg);
        if (SecondaryBgRgb.HasContent())
            yield return ("--bs-secondary-bg-rgb", SecondaryBgRgb);
        if (TertiaryColor.HasContent())
            yield return ("--bs-tertiary-color", TertiaryColor);
        if (TertiaryColorRgb.HasContent())
            yield return ("--bs-tertiary-color-rgb", TertiaryColorRgb);
        if (TertiaryBg.HasContent())
            yield return ("--bs-tertiary-bg", TertiaryBg);
        if (TertiaryBgRgb.HasContent())
            yield return ("--bs-tertiary-bg-rgb", TertiaryBgRgb);
        if (HeadingColor.HasContent())
            yield return ("--bs-heading-color", HeadingColor);
        if (LinkColor.HasContent())
            yield return ("--bs-link-color", LinkColor);
        if (LinkColorRgb.HasContent())
            yield return ("--bs-link-color-rgb", LinkColorRgb);
        if (LinkDecoration.HasContent())
            yield return ("--bs-link-decoration", LinkDecoration);
        if (LinkHoverColor.HasContent())
            yield return ("--bs-link-hover-color", LinkHoverColor);
        if (LinkHoverColorRgb.HasContent())
            yield return ("--bs-link-hover-color-rgb", LinkHoverColorRgb);
        if (LinkHoverDecoration.HasContent())
            yield return ("--bs-link-hover-decoration", LinkHoverDecoration);
        if (CodeColor.HasContent())
            yield return ("--bs-code-color", CodeColor);
        if (HighlightColor.HasContent())
            yield return ("--bs-highlight-color", HighlightColor);
        if (HighlightBg.HasContent())
            yield return ("--bs-highlight-bg", HighlightBg);
        if (BodyTextAlign.HasContent())
            yield return ("--bs-body-text-align", BodyTextAlign);
    }
}
