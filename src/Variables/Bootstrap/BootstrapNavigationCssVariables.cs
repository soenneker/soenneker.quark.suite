
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's navigation CSS variables
/// </summary>
public sealed class BootstrapNavigationCssVariables
{
    // Dropdown
    /// <summary>
    /// Dropdown z-index. Default: 1000
    /// </summary>
    [CssVariable("bs-dropdown-zindex")]
    public string? DropdownZindex { get; set; }

    /// <summary>
    /// Dropdown min width. Default: 10rem
    /// </summary>
    [CssVariable("bs-dropdown-min-width")]
    public string? DropdownMinWidth { get; set; }

    /// <summary>
    /// Dropdown padding X. Default: 0
    /// </summary>
    [CssVariable("bs-dropdown-padding-x")]
    public string? DropdownPaddingX { get; set; }

    /// <summary>
    /// Dropdown padding Y. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-dropdown-padding-y")]
    public string? DropdownPaddingY { get; set; }

    /// <summary>
    /// Dropdown spacer. Default: 0.125rem
    /// </summary>
    [CssVariable("bs-dropdown-spacer")]
    public string? DropdownSpacer { get; set; }

    /// <summary>
    /// Dropdown font size. Default: var(--bs-body-font-size)
    /// </summary>
    [CssVariable("bs-dropdown-font-size")]
    public string? DropdownFontSize { get; set; }

    /// <summary>
    /// Dropdown color. Default: var(--bs-body-color)
    /// </summary>
    [CssVariable("bs-dropdown-color")]
    public string? DropdownColor { get; set; }

    /// <summary>
    /// Dropdown background color. Default: var(--bs-body-bg)
    /// </summary>
    [CssVariable("bs-dropdown-bg")]
    public string? DropdownBg { get; set; }

    /// <summary>
    /// Dropdown border. Default: var(--bs-border-width) solid var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-dropdown-border")]
    public string? DropdownBorder { get; set; }

    /// <summary>
    /// Dropdown border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-dropdown-border-radius")]
    public string? DropdownBorderRadius { get; set; }

    /// <summary>
    /// Dropdown box shadow. Default: var(--bs-box-shadow)
    /// </summary>
    [CssVariable("bs-dropdown-box-shadow")]
    public string? DropdownBoxShadow { get; set; }

    /// <summary>
    /// Dropdown link color. Default: var(--bs-body-color)
    /// </summary>
    [CssVariable("bs-dropdown-link-color")]
    public string? DropdownLinkColor { get; set; }

    /// <summary>
    /// Dropdown link hover color. Default: var(--bs-body-color)
    /// </summary>
    [CssVariable("bs-dropdown-link-hover-color")]
    public string? DropdownLinkHoverColor { get; set; }

    /// <summary>
    /// Dropdown link hover background color. Default: var(--bs-secondary-bg)
    /// </summary>
    [CssVariable("bs-dropdown-link-hover-bg")]
    public string? DropdownLinkHoverBg { get; set; }

    /// <summary>
    /// Dropdown link active color. Default: var(--bs-body-color)
    /// </summary>
    [CssVariable("bs-dropdown-link-active-color")]
    public string? DropdownLinkActiveColor { get; set; }

    /// <summary>
    /// Dropdown link active background color. Default: var(--bs-primary)
    /// </summary>
    [CssVariable("bs-dropdown-link-active-bg")]
    public string? DropdownLinkActiveBg { get; set; }

    /// <summary>
    /// Dropdown link disabled color. Default: var(--bs-secondary-color)
    /// </summary>
    [CssVariable("bs-dropdown-link-disabled-color")]
    public string? DropdownLinkDisabledColor { get; set; }

    /// <summary>
    /// Dropdown item padding X. Default: 1rem
    /// </summary>
    [CssVariable("bs-dropdown-item-padding-x")]
    public string? DropdownItemPaddingX { get; set; }

    /// <summary>
    /// Dropdown item padding Y. Default: 0.25rem
    /// </summary>
    [CssVariable("bs-dropdown-item-padding-y")]
    public string? DropdownItemPaddingY { get; set; }

    /// <summary>
    /// Dropdown header color. Default: var(--bs-secondary-color)
    /// </summary>
    [CssVariable("bs-dropdown-header-color")]
    public string? DropdownHeaderColor { get; set; }

    /// <summary>
    /// Dropdown header padding. Default: var(--bs-dropdown-item-padding-y) var(--bs-dropdown-item-padding-x)
    /// </summary>
    [CssVariable("bs-dropdown-header-padding")]
    public string? DropdownHeaderPadding { get; set; }

    /// <summary>
    /// Dropdown divider margin Y. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-dropdown-divider-margin-y")]
    public string? DropdownDividerMarginY { get; set; }

    /// <summary>
    /// Dropdown divider background color. Default: var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-dropdown-divider-bg")]
    public string? DropdownDividerBg { get; set; }

    /// <summary>
    /// Dropdown text color. Default: var(--bs-dropdown-link-color)
    /// </summary>
    [CssVariable("bs-dropdown-text-color")]
    public string? DropdownTextColor { get; set; }

    // Navbar
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
    /// Navbar color. Default: var(--bs-navbar-color)
    /// </summary>
    [CssVariable("bs-navbar-color")]
    public string? NavbarColor { get; set; }

    /// <summary>
    /// Navbar hover color. Default: var(--bs-navbar-hover-color)
    /// </summary>
    [CssVariable("bs-navbar-hover-color")]
    public string? NavbarHoverColor { get; set; }

    /// <summary>
    /// Navbar disabled color. Default: var(--bs-navbar-disabled-color)
    /// </summary>
    [CssVariable("bs-navbar-disabled-color")]
    public string? NavbarDisabledColor { get; set; }

    /// <summary>
    /// Navbar active color. Default: var(--bs-navbar-active-color)
    /// </summary>
    [CssVariable("bs-navbar-active-color")]
    public string? NavbarActiveColor { get; set; }

    /// <summary>
    /// Navbar brand padding Y. Default: var(--bs-navbar-padding-y)
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
    /// Navbar brand color. Default: var(--bs-navbar-brand-color)
    /// </summary>
    [CssVariable("bs-navbar-brand-color")]
    public string? NavbarBrandColor { get; set; }

    /// <summary>
    /// Navbar brand hover color. Default: var(--bs-navbar-brand-hover-color)
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
    /// Navbar toggler border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-navbar-toggler-border-radius")]
    public string? NavbarTogglerBorderRadius { get; set; }

    /// <summary>
    /// Navbar toggler border. Default: 1px solid var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-navbar-toggler-border")]
    public string? NavbarTogglerBorder { get; set; }

    /// <summary>
    /// Navbar toggler box shadow. Default: 0 1px 2px rgba(0, 0, 0, 0.05)
    /// </summary>
    [CssVariable("bs-navbar-toggler-box-shadow")]
    public string? NavbarTogglerBoxShadow { get; set; }

    /// <summary>
    /// Navbar toggler focus box shadow. Default: 0 0 0 0.25rem
    /// </summary>
    [CssVariable("bs-navbar-toggler-focus-box-shadow")]
    public string? NavbarTogglerFocusBoxShadow { get; set; }

    /// <summary>
    /// Navbar toggler icon background image. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 30 30'%3e%3cpath stroke='rgba%2833, 37, 41, 0.75%29' stroke-linecap='round' stroke-miterlimit='10' stroke-width='2' d='M4 7h22M4 15h22M4 23h22'/%3e%3c/svg%3e")
    /// </summary>
    [CssVariable("bs-navbar-toggler-icon-bg")]
    public string? NavbarTogglerIconBg { get; set; }

    /// <summary>
    /// Navbar toggler border color. Default: rgba(var(--bs-emphasis-color-rgb), 0.15)
    /// </summary>
    [CssVariable("bs-navbar-toggler-border-color")]
    public string? NavbarTogglerBorderColor { get; set; }

    /// <summary>
    /// Navbar toggler hover border color. Default: rgba(var(--bs-emphasis-color-rgb), 0.25)
    /// </summary>
    [CssVariable("bs-navbar-toggler-hover-border-color")]
    public string? NavbarTogglerHoverBorderColor { get; set; }

    /// <summary>
    /// Navbar toggler focus border color. Default: rgba(var(--bs-emphasis-color-rgb), 0.25)
    /// </summary>
    [CssVariable("bs-navbar-toggler-focus-border-color")]
    public string? NavbarTogglerFocusBorderColor { get; set; }

    /// <summary>
    /// Navbar toggler focus ring color. Default: rgba(var(--bs-primary-rgb), 0.25)
    /// </summary>
    [CssVariable("bs-navbar-toggler-focus-ring-color")]
    public string? NavbarTogglerFocusRingColor { get; set; }

    /// <summary>
    /// Navbar toggler focus ring width. Default: 0.25rem
    /// </summary>
    [CssVariable("bs-navbar-toggler-focus-ring-width")]
    public string? NavbarTogglerFocusRingWidth { get; set; }

    /// <summary>
    /// Navbar toggler focus ring opacity. Default: 0.25
    /// </summary>
    [CssVariable("bs-navbar-toggler-focus-ring-opacity")]
    public string? NavbarTogglerFocusRingOpacity { get; set; }

    /// <summary>
    /// Navbar toggler transition. Default: box-shadow 0.15s ease-in-out
    /// </summary>
    [CssVariable("bs-navbar-toggler-transition")]
    public string? NavbarTogglerTransition { get; set; }

    /// <summary>
    /// Navbar collapse max height. Default: 75vh
    /// </summary>
    [CssVariable("bs-navbar-collapse-max-height")]
    public string? NavbarCollapseMaxHeight { get; set; }

    /// <summary>
    /// Navbar collapse padding top. Default: 1rem
    /// </summary>
    [CssVariable("bs-navbar-collapse-padding-top")]
    public string? NavbarCollapsePaddingTop { get; set; }

    /// <summary>
    /// Navbar collapse padding bottom. Default: 1rem
    /// </summary>
    [CssVariable("bs-navbar-collapse-padding-bottom")]
    public string? NavbarCollapsePaddingBottom { get; set; }

    /// <summary>
    /// Navbar collapse border top. Default: 1px solid var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-navbar-collapse-border-top")]
    public string? NavbarCollapseBorderTop { get; set; }

    /// <summary>
    /// Navbar collapse border bottom. Default: 1px solid var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-navbar-collapse-border-bottom")]
    public string? NavbarCollapseBorderBottom { get; set; }

    /// <summary>
    /// Navbar collapse border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-navbar-collapse-border-radius")]
    public string? NavbarCollapseBorderRadius { get; set; }

    /// <summary>
    /// Navbar collapse box shadow. Default: var(--bs-box-shadow-sm)
    /// </summary>
    [CssVariable("bs-navbar-collapse-box-shadow")]
    public string? NavbarCollapseBoxShadow { get; set; }

    /// <summary>
    /// Navbar collapse background color. Default: var(--bs-body-bg)
    /// </summary>
    [CssVariable("bs-navbar-collapse-bg")]
    public string? NavbarCollapseBg { get; set; }

    /// <summary>
    /// Navbar collapse color. Default: var(--bs-body-color)
    /// </summary>
    [CssVariable("bs-navbar-collapse-color")]
    public string? NavbarCollapseColor { get; set; }

    /// <summary>
    /// Navbar collapse link color. Default: var(--bs-navbar-color)
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-color")]
    public string? NavbarCollapseLinkColor { get; set; }

    /// <summary>
    /// Navbar collapse link hover color. Default: var(--bs-navbar-hover-color)
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-hover-color")]
    public string? NavbarCollapseLinkHoverColor { get; set; }

    /// <summary>
    /// Navbar collapse link active color. Default: var(--bs-navbar-active-color)
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-active-color")]
    public string? NavbarCollapseLinkActiveColor { get; set; }

    /// <summary>
    /// Navbar collapse link disabled color. Default: var(--bs-navbar-disabled-color)
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-disabled-color")]
    public string? NavbarCollapseLinkDisabledColor { get; set; }

    /// <summary>
    /// Navbar collapse link padding Y. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-padding-y")]
    public string? NavbarCollapseLinkPaddingY { get; set; }

    /// <summary>
    /// Navbar collapse link padding X. Default: 1rem
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-padding-x")]
    public string? NavbarCollapseLinkPaddingX { get; set; }

    /// <summary>
    /// Navbar collapse link border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-border-radius")]
    public string? NavbarCollapseLinkBorderRadius { get; set; }

    /// <summary>
    /// Navbar collapse link hover background color. Default: var(--bs-tertiary-bg)
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-hover-bg")]
    public string? NavbarCollapseLinkHoverBg { get; set; }

    /// <summary>
    /// Navbar collapse link active background color. Default: var(--bs-primary)
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-active-bg")]
    public string? NavbarCollapseLinkActiveBg { get; set; }

    /// <summary>
    /// Navbar collapse link disabled background color. Default: transparent
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-disabled-bg")]
    public string? NavbarCollapseLinkDisabledBg { get; set; }

    /// <summary>
    /// Navbar collapse link transition. Default: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out
    /// </summary>
    [CssVariable("bs-navbar-collapse-link-transition")]
    public string? NavbarCollapseLinkTransition { get; set; }
}
