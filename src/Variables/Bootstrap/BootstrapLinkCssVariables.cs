
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's link CSS variables and direct CSS properties
/// </summary>
public sealed class BootstrapLinkCssVariables
{
    /// <summary>
    /// Link color RGB values. Default: 13, 110, 253
    /// </summary>
    [CssVariable("bs-link-color-rgb")]
    public string? LinkColorRgb { get; set; }

    /// <summary>
    /// Link opacity. Default: 1
    /// </summary>
    [CssVariable("bs-link-opacity")]
    public string? LinkOpacity { get; set; }

    /// <summary>
    /// Link hover color RGB values. Default: 10, 88, 202
    /// </summary>
    [CssVariable("bs-link-hover-color-rgb")]
    public string? LinkHoverColorRgb { get; set; }

    /// <summary>
    /// Link hover opacity. Default: 1
    /// </summary>
    [CssVariable("bs-link-hover-opacity")]
    public string? LinkHoverOpacity { get; set; }

    /// <summary>
    /// Link underline offset. Default: 0.125em
    /// </summary>
    [CssVariable("bs-link-underline-offset")]
    public string? LinkUnderlineOffset { get; set; }

    /// <summary>
    /// Link underline opacity. Default: 1
    /// </summary>
    [CssVariable("bs-link-underline-opacity")]
    public string? LinkUnderlineOpacity { get; set; }

    /// <summary>
    /// Direct CSS text-decoration property. Set to "none" to remove underlines, "underline" to add them.
    /// </summary>
    [CssVariable("text-decoration", isVariable: false)]
    public string? TextDecoration { get; set; }
}

