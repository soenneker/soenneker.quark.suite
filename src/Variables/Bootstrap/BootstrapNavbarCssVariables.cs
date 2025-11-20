using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's navbar CSS variables
/// </summary>
public sealed class BootstrapNavbarCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Navbar padding X. Default: 0
    /// </summary>
    public string? NavbarPaddingX { get; set; }

    /// <summary>
    /// Navbar padding Y. Default: 0.5rem
    /// </summary>
    public string? NavbarPaddingY { get; set; }

    /// <summary>
    /// Navbar color. Default: rgba(var(--bs-emphasis-color-rgb), 0.65)
    /// </summary>
    public string? NavbarColor { get; set; }

    /// <summary>
    /// Navbar hover color. Default: rgba(var(--bs-emphasis-color-rgb), 0.8)
    /// </summary>
    public string? NavbarHoverColor { get; set; }

    /// <summary>
    /// Navbar disabled color. Default: rgba(var(--bs-emphasis-color-rgb), 0.3)
    /// </summary>
    public string? NavbarDisabledColor { get; set; }

    /// <summary>
    /// Navbar active color. Default: rgba(var(--bs-emphasis-color-rgb), 1)
    /// </summary>
    public string? NavbarActiveColor { get; set; }

    /// <summary>
    /// Navbar brand padding Y. Default: 0.3125rem
    /// </summary>
    public string? NavbarBrandPaddingY { get; set; }

    /// <summary>
    /// Navbar brand margin end. Default: 1rem
    /// </summary>
    public string? NavbarBrandMarginEnd { get; set; }

    /// <summary>
    /// Navbar brand font size. Default: 1.25rem
    /// </summary>
    public string? NavbarBrandFontSize { get; set; }

    /// <summary>
    /// Navbar brand color. Default: rgba(var(--bs-emphasis-color-rgb), 1)
    /// </summary>
    public string? NavbarBrandColor { get; set; }

    /// <summary>
    /// Navbar brand hover color. Default: rgba(var(--bs-emphasis-color-rgb), 1)
    /// </summary>
    public string? NavbarBrandHoverColor { get; set; }

    /// <summary>
    /// Navbar nav link padding X. Default: 0.5rem
    /// </summary>
    public string? NavbarNavLinkPaddingX { get; set; }

    /// <summary>
    /// Navbar toggler padding Y. Default: 0.25rem
    /// </summary>
    public string? NavbarTogglerPaddingY { get; set; }

    /// <summary>
    /// Navbar toggler padding X. Default: 0.75rem
    /// </summary>
    public string? NavbarTogglerPaddingX { get; set; }

    /// <summary>
    /// Navbar toggler font size. Default: 1.25rem
    /// </summary>
    public string? NavbarTogglerFontSize { get; set; }

    /// <summary>
    /// Navbar toggler icon background. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 30 30'%3e%3cpath stroke='rgba%2833, 37, 41, 0.75%29' stroke-linecap='round' stroke-miterlimit='10' stroke-width='2' d='M4 7h22M4 15h22M4 23h22'/%3e%3c/svg%3e")
    /// </summary>
    public string? NavbarTogglerIconBg { get; set; }

    /// <summary>
    /// Navbar toggler border color. Default: rgba(var(--bs-emphasis-color-rgb), 0.15)
    /// </summary>
    public string? NavbarTogglerBorderColor { get; set; }

    /// <summary>
    /// Navbar toggler border radius. Default: var(--bs-border-radius)
    /// </summary>
    public string? NavbarTogglerBorderRadius { get; set; }

    /// <summary>
    /// Navbar toggler focus width. Default: 0.25rem
    /// </summary>
    public string? NavbarTogglerFocusWidth { get; set; }

    /// <summary>
    /// Navbar toggler transition. Default: box-shadow 0.15s ease-in-out
    /// </summary>
    public string? NavbarTogglerTransition { get; set; }

    public string GetSelector()
    {
        return ".navbar";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (NavbarPaddingX.HasContent())
            yield return ("--bs-navbar-padding-x", NavbarPaddingX);
        if (NavbarPaddingY.HasContent())
            yield return ("--bs-navbar-padding-y", NavbarPaddingY);
        if (NavbarColor.HasContent())
            yield return ("--bs-navbar-color", NavbarColor);
        if (NavbarHoverColor.HasContent())
            yield return ("--bs-navbar-hover-color", NavbarHoverColor);
        if (NavbarDisabledColor.HasContent())
            yield return ("--bs-navbar-disabled-color", NavbarDisabledColor);
        if (NavbarActiveColor.HasContent())
            yield return ("--bs-navbar-active-color", NavbarActiveColor);
        if (NavbarBrandPaddingY.HasContent())
            yield return ("--bs-navbar-brand-padding-y", NavbarBrandPaddingY);
        if (NavbarBrandMarginEnd.HasContent())
            yield return ("--bs-navbar-brand-margin-end", NavbarBrandMarginEnd);
        if (NavbarBrandFontSize.HasContent())
            yield return ("--bs-navbar-brand-font-size", NavbarBrandFontSize);
        if (NavbarBrandColor.HasContent())
            yield return ("--bs-navbar-brand-color", NavbarBrandColor);
        if (NavbarBrandHoverColor.HasContent())
            yield return ("--bs-navbar-brand-hover-color", NavbarBrandHoverColor);
        if (NavbarNavLinkPaddingX.HasContent())
            yield return ("--bs-navbar-nav-link-padding-x", NavbarNavLinkPaddingX);
        if (NavbarTogglerPaddingY.HasContent())
            yield return ("--bs-navbar-toggler-padding-y", NavbarTogglerPaddingY);
        if (NavbarTogglerPaddingX.HasContent())
            yield return ("--bs-navbar-toggler-padding-x", NavbarTogglerPaddingX);
        if (NavbarTogglerFontSize.HasContent())
            yield return ("--bs-navbar-toggler-font-size", NavbarTogglerFontSize);
        if (NavbarTogglerIconBg.HasContent())
            yield return ("--bs-navbar-toggler-icon-bg", NavbarTogglerIconBg);
        if (NavbarTogglerBorderColor.HasContent())
            yield return ("--bs-navbar-toggler-border-color", NavbarTogglerBorderColor);
        if (NavbarTogglerBorderRadius.HasContent())
            yield return ("--bs-navbar-toggler-border-radius", NavbarTogglerBorderRadius);
        if (NavbarTogglerFocusWidth.HasContent())
            yield return ("--bs-navbar-toggler-focus-width", NavbarTogglerFocusWidth);
        if (NavbarTogglerTransition.HasContent())
            yield return ("--bs-navbar-toggler-transition", NavbarTogglerTransition);
    }
}
