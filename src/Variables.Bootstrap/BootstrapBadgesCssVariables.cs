
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's badge CSS variables
/// </summary>
public sealed class BootstrapBadgesCssVariables
{
    /// <summary>
    /// Badge padding X. Default: 0.65em
    /// </summary>
    [CssVariable("bs-badge-padding-x")]
    public string? BadgePaddingX { get; set; }

    /// <summary>
    /// Badge padding Y. Default: 0.35em
    /// </summary>
    [CssVariable("bs-badge-padding-y")]
    public string? BadgePaddingY { get; set; }

    /// <summary>
    /// Badge font size. Default: 0.75em
    /// </summary>
    [CssVariable("bs-badge-font-size")]
    public string? BadgeFontSize { get; set; }

    /// <summary>
    /// Badge font weight. Default: 700
    /// </summary>
    [CssVariable("bs-badge-font-weight")]
    public string? BadgeFontWeight { get; set; }

    /// <summary>
    /// Badge color. Default: var(--bs-white)
    /// </summary>
    [CssVariable("bs-badge-color")]
    public string? BadgeColor { get; set; }

    /// <summary>
    /// Badge background color. Default: var(--bs-secondary)
    /// </summary>
    [CssVariable("bs-badge-bg")]
    public string? BadgeBg { get; set; }
}
