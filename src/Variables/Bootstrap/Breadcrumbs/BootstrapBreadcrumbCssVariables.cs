using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's breadcrumb CSS variables
/// </summary>
public sealed class BootstrapBreadcrumbCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Breadcrumb padding X. Default: 0
    /// </summary>
    public string? BreadcrumbPaddingX { get; set; }

    /// <summary>
    /// Breadcrumb padding Y. Default: 0
    /// </summary>
    public string? BreadcrumbPaddingY { get; set; }

    /// <summary>
    /// Breadcrumb margin bottom. Default: 1rem
    /// </summary>
    public string? BreadcrumbMarginBottom { get; set; }

    /// <summary>
    /// Breadcrumb font size. Default: var(--bs-font-size-sm)
    /// </summary>
    public string? BreadcrumbFontSize { get; set; }

    /// <summary>
    /// Breadcrumb background color. Default: transparent
    /// </summary>
    public string? BreadcrumbBg { get; set; }

    /// <summary>
    /// Breadcrumb border radius. Default: var(--bs-border-radius)
    /// </summary>
    public string? BreadcrumbBorderRadius { get; set; }

    /// <summary>
    /// Breadcrumb divider color. Default: var(--bs-secondary-color)
    /// </summary>
    public string? BreadcrumbDividerColor { get; set; }

    /// <summary>
    /// Breadcrumb divider. Default: "/"
    /// </summary>
    public string? BreadcrumbDivider { get; set; }

    /// <summary>
    /// Breadcrumb item padding X. Default: 0.5rem
    /// </summary>
    public string? BreadcrumbItemPaddingX { get; set; }

    /// <summary>
    /// Breadcrumb item active color. Default: var(--bs-secondary-color)
    /// </summary>
    public string? BreadcrumbItemActiveColor { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (BreadcrumbPaddingX.HasContent())
            yield return ("--bs-breadcrumb-padding-x", BreadcrumbPaddingX);
        if (BreadcrumbPaddingY.HasContent())
            yield return ("--bs-breadcrumb-padding-y", BreadcrumbPaddingY);
        if (BreadcrumbMarginBottom.HasContent())
            yield return ("--bs-breadcrumb-margin-bottom", BreadcrumbMarginBottom);
        if (BreadcrumbFontSize.HasContent())
            yield return ("--bs-breadcrumb-font-size", BreadcrumbFontSize);
        if (BreadcrumbBg.HasContent())
            yield return ("--bs-breadcrumb-bg", BreadcrumbBg);
        if (BreadcrumbBorderRadius.HasContent())
            yield return ("--bs-breadcrumb-border-radius", BreadcrumbBorderRadius);
        if (BreadcrumbDividerColor.HasContent())
            yield return ("--bs-breadcrumb-divider-color", BreadcrumbDividerColor);
        if (BreadcrumbDivider.HasContent())
            yield return ("--bs-breadcrumb-divider", BreadcrumbDivider);
        if (BreadcrumbItemPaddingX.HasContent())
            yield return ("--bs-breadcrumb-item-padding-x", BreadcrumbItemPaddingX);
        if (BreadcrumbItemActiveColor.HasContent())
            yield return ("--bs-breadcrumb-item-active-color", BreadcrumbItemActiveColor);
    }
}
