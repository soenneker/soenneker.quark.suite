namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's breadcrumb CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapBreadcrumbCssVariables
{
    /// <summary>
    /// Breadcrumb padding X. Default: 0
    /// </summary>
    [CssVariable("bs-breadcrumb-padding-x")]
    public string? BreadcrumbPaddingX { get; set; }

    /// <summary>
    /// Breadcrumb padding Y. Default: 0
    /// </summary>
    [CssVariable("bs-breadcrumb-padding-y")]
    public string? BreadcrumbPaddingY { get; set; }

    /// <summary>
    /// Breadcrumb margin bottom. Default: 1rem
    /// </summary>
    [CssVariable("bs-breadcrumb-margin-bottom")]
    public string? BreadcrumbMarginBottom { get; set; }

    /// <summary>
    /// Breadcrumb font size. Default: var(--bs-font-size-sm)
    /// </summary>
    [CssVariable("bs-breadcrumb-font-size")]
    public string? BreadcrumbFontSize { get; set; }

    /// <summary>
    /// Breadcrumb background color. Default: transparent
    /// </summary>
    [CssVariable("bs-breadcrumb-bg")]
    public string? BreadcrumbBg { get; set; }

    /// <summary>
    /// Breadcrumb border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-breadcrumb-border-radius")]
    public string? BreadcrumbBorderRadius { get; set; }

    /// <summary>
    /// Breadcrumb divider color. Default: var(--bs-secondary-color)
    /// </summary>
    [CssVariable("bs-breadcrumb-divider-color")]
    public string? BreadcrumbDividerColor { get; set; }

    /// <summary>
    /// Breadcrumb divider. Default: "/"
    /// </summary>
    [CssVariable("bs-breadcrumb-divider")]
    public string? BreadcrumbDivider { get; set; }

    /// <summary>
    /// Breadcrumb item padding X. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-breadcrumb-item-padding-x")]
    public string? BreadcrumbItemPaddingX { get; set; }

    /// <summary>
    /// Breadcrumb item active color. Default: var(--bs-secondary-color)
    /// </summary>
    [CssVariable("bs-breadcrumb-item-active-color")]
    public string? BreadcrumbItemActiveColor { get; set; }
}
