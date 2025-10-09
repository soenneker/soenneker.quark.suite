namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's navbar CSS variables
/// </summary>
[CssSelector(".navbar")]
public sealed class BootstrapNavbarCssVariables
{
    /// <summary>
    /// Navbar padding X. Default: 0
    /// </summary>
    [CssVariable("bs-navbar-padding-x")]
    public string? NavbarPaddingX { get; set; }

    /// <summary>
    /// Navbar padding Y. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-navbar-padding-y")]
    public string? NavbarPaddingY { get; set; }

    /// <summary>
    /// Navbar color. Default: rgba(var(--bs-emphasis-color-rgb), 0.65)
    /// </summary>
    [CssVariable("bs-navbar-color")]
    public string? NavbarColor { get; set; }

    /// <summary>
    /// Navbar hover color. Default: rgba(var(--bs-emphasis-color-rgb), 0.8)
    /// </summary>
    [CssVariable("bs-navbar-hover-color")]
    public string? NavbarHoverColor { get; set; }

    /// <summary>
    /// Navbar disabled color. Default: rgba(var(--bs-emphasis-color-rgb), 0.3)
    /// </summary>
    [CssVariable("bs-navbar-disabled-color")]
    public string? NavbarDisabledColor { get; set; }

    /// <summary>
    /// Navbar active color. Default: rgba(var(--bs-emphasis-color-rgb), 1)
    /// </summary>
    [CssVariable("bs-navbar-active-color")]
    public string? NavbarActiveColor { get; set; }

    /// <summary>
    /// Navbar brand padding Y. Default: 0.3125rem
    /// </summary>
    [CssVariable("bs-navbar-brand-padding-y")]
    public string? NavbarBrandPaddingY { get; set; }

    /// <summary>
    /// Navbar brand margin end. Default: 1rem
    /// </summary>
    [CssVariable("bs-navbar-brand-margin-end")]
    public string? NavbarBrandMarginEnd { get; set; }

    /// <summary>
    /// Navbar brand font size. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-navbar-brand-font-size")]
    public string? NavbarBrandFontSize { get; set; }

    /// <summary>
    /// Navbar brand color. Default: rgba(var(--bs-emphasis-color-rgb), 1)
    /// </summary>
    [CssVariable("bs-navbar-brand-color")]
    public string? NavbarBrandColor { get; set; }

    /// <summary>
    /// Navbar brand hover color. Default: rgba(var(--bs-emphasis-color-rgb), 1)
    /// </summary>
    [CssVariable("bs-navbar-brand-hover-color")]
    public string? NavbarBrandHoverColor { get; set; }

    /// <summary>
    /// Navbar nav link padding X. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-navbar-nav-link-padding-x")]
    public string? NavbarNavLinkPaddingX { get; set; }

    /// <summary>
    /// Navbar toggler padding Y. Default: 0.25rem
    /// </summary>
    [CssVariable("bs-navbar-toggler-padding-y")]
    public string? NavbarTogglerPaddingY { get; set; }

    /// <summary>
    /// Navbar toggler padding X. Default: 0.75rem
    /// </summary>
    [CssVariable("bs-navbar-toggler-padding-x")]
    public string? NavbarTogglerPaddingX { get; set; }

    /// <summary>
    /// Navbar toggler font size. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-navbar-toggler-font-size")]
    public string? NavbarTogglerFontSize { get; set; }

    /// <summary>
    /// Navbar toggler icon background. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 30 30'%3e%3cpath stroke='rgba%2833, 37, 41, 0.75%29' stroke-linecap='round' stroke-miterlimit='10' stroke-width='2' d='M4 7h22M4 15h22M4 23h22'/%3e%3c/svg%3e")
    /// </summary>
    [CssVariable("bs-navbar-toggler-icon-bg")]
    public string? NavbarTogglerIconBg { get; set; }

    /// <summary>
    /// Navbar toggler border color. Default: rgba(var(--bs-emphasis-color-rgb), 0.15)
    /// </summary>
    [CssVariable("bs-navbar-toggler-border-color")]
    public string? NavbarTogglerBorderColor { get; set; }

    /// <summary>
    /// Navbar toggler border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-navbar-toggler-border-radius")]
    public string? NavbarTogglerBorderRadius { get; set; }

    /// <summary>
    /// Navbar toggler focus width. Default: 0.25rem
    /// </summary>
    [CssVariable("bs-navbar-toggler-focus-width")]
    public string? NavbarTogglerFocusWidth { get; set; }

    /// <summary>
    /// Navbar toggler transition. Default: box-shadow 0.15s ease-in-out
    /// </summary>
    [CssVariable("bs-navbar-toggler-transition")]
    public string? NavbarTogglerTransition { get; set; }
}
