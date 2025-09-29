namespace Soenneker.Quark.Variables.Bootstrap;

/// <summary>
/// Bootstrap's typography CSS variables
/// </summary>
public sealed class BootstrapTypographyCssVariables
{
    // Font Families
    /// <summary>
    /// Sans-serif font family. Default: system-ui, -apple-system, "Segoe UI", Roboto, "Helvetica Neue", "Noto Sans", "Liberation Sans", Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji"
    /// </summary>
    [CssVariable("bs-font-sans-serif")]
    public string? FontSansSerif { get; set; }

    /// <summary>
    /// Monospace font family. Default: SFMono-Regular, Menlo, Monaco, Consolas, "Liberation Mono", "Courier New", monospace
    /// </summary>
    [CssVariable("bs-font-monospace")]
    public string? FontMonospace { get; set; }

    // Body Typography
    /// <summary>
    /// Body font family. Default: var(--bs-font-sans-serif)
    /// </summary>
    [CssVariable("bs-body-font-family")]
    public string? BodyFontFamily { get; set; }

    /// <summary>
    /// Body font size. Default: 1rem
    /// </summary>
    [CssVariable("bs-body-font-size")]
    public string? BodyFontSize { get; set; }

    /// <summary>
    /// Body font weight. Default: 400
    /// </summary>
    [CssVariable("bs-body-font-weight")]
    public string? BodyFontWeight { get; set; }

    /// <summary>
    /// Body line height. Default: 1.5
    /// </summary>
    [CssVariable("bs-body-line-height")]
    public string? BodyLineHeight { get; set; }

    /// <summary>
    /// Body color. Default: #212529
    /// </summary>
    [CssVariable("bs-body-color")]
    public string? BodyColor { get; set; }

    /// <summary>
    /// Body color RGB values. Default: 33, 37, 41
    /// </summary>
    [CssVariable("bs-body-color-rgb")]
    public string? BodyColorRgb { get; set; }

    /// <summary>
    /// Body background color. Default: #fff
    /// </summary>
    [CssVariable("bs-body-bg")]
    public string? BodyBg { get; set; }

    /// <summary>
    /// Body background color RGB values. Default: 255, 255, 255
    /// </summary>
    [CssVariable("bs-body-bg-rgb")]
    public string? BodyBgRgb { get; set; }

    // Emphasis and Secondary Colors
    /// <summary>
    /// Emphasis color. Default: #000
    /// </summary>
    [CssVariable("bs-emphasis-color")]
    public string? EmphasisColor { get; set; }

    /// <summary>
    /// Emphasis color RGB values. Default: 0, 0, 0
    /// </summary>
    [CssVariable("bs-emphasis-color-rgb")]
    public string? EmphasisColorRgb { get; set; }

    /// <summary>
    /// Secondary color. Default: rgba(33, 37, 41, 0.75)
    /// </summary>
    [CssVariable("bs-secondary-color")]
    public string? SecondaryColor { get; set; }

    /// <summary>
    /// Secondary color RGB values. Default: 33, 37, 41
    /// </summary>
    [CssVariable("bs-secondary-color-rgb")]
    public string? SecondaryColorRgb { get; set; }

    /// <summary>
    /// Secondary background color. Default: #e9ecef
    /// </summary>
    [CssVariable("bs-secondary-bg")]
    public string? SecondaryBg { get; set; }

    /// <summary>
    /// Secondary background color RGB values. Default: 233, 236, 239
    /// </summary>
    [CssVariable("bs-secondary-bg-rgb")]
    public string? SecondaryBgRgb { get; set; }

    /// <summary>
    /// Tertiary color. Default: rgba(33, 37, 41, 0.5)
    /// </summary>
    [CssVariable("bs-tertiary-color")]
    public string? TertiaryColor { get; set; }

    /// <summary>
    /// Tertiary color RGB values. Default: 33, 37, 41
    /// </summary>
    [CssVariable("bs-tertiary-color-rgb")]
    public string? TertiaryColorRgb { get; set; }

    /// <summary>
    /// Tertiary background color. Default: #f8f9fa
    /// </summary>
    [CssVariable("bs-tertiary-bg")]
    public string? TertiaryBg { get; set; }

    /// <summary>
    /// Tertiary background color RGB values. Default: 248, 249, 250
    /// </summary>
    [CssVariable("bs-tertiary-bg-rgb")]
    public string? TertiaryBgRgb { get; set; }

    // Heading and Link Colors
    /// <summary>
    /// Heading color. Default: inherit
    /// </summary>
    [CssVariable("bs-heading-color")]
    public string? HeadingColor { get; set; }

    /// <summary>
    /// Link color. Default: #0d6efd
    /// </summary>
    [CssVariable("bs-link-color")]
    public string? LinkColor { get; set; }

    /// <summary>
    /// Link color RGB values. Default: 13, 110, 253
    /// </summary>
    [CssVariable("bs-link-color-rgb")]
    public string? LinkColorRgb { get; set; }

    /// <summary>
    /// Link decoration. Default: underline
    /// </summary>
    [CssVariable("bs-link-decoration")]
    public string? LinkDecoration { get; set; }

    /// <summary>
    /// Link hover color. Default: #0a58ca
    /// </summary>
    [CssVariable("bs-link-hover-color")]
    public string? LinkHoverColor { get; set; }

    /// <summary>
    /// Link hover color RGB values. Default: 10, 88, 202
    /// </summary>
    [CssVariable("bs-link-hover-color-rgb")]
    public string? LinkHoverColorRgb { get; set; }

    // Code and Highlight Colors
    /// <summary>
    /// Code color. Default: #d63384
    /// </summary>
    [CssVariable("bs-code-color")]
    public string? CodeColor { get; set; }

    /// <summary>
    /// Highlight color. Default: #212529
    /// </summary>
    [CssVariable("bs-highlight-color")]
    public string? HighlightColor { get; set; }

    /// <summary>
    /// Highlight background color. Default: #fff3cd
    /// </summary>
    [CssVariable("bs-highlight-bg")]
    public string? HighlightBg { get; set; }
}
