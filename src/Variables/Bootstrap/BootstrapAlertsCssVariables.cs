
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's alert CSS variables
/// </summary>
public sealed class BootstrapAlertsCssVariables
{
    /// <summary>
    /// Alert background color. Default: transparent
    /// </summary>
    [CssVariable("bs-alert-bg")]
    public string? AlertBg { get; set; }

    /// <summary>
    /// Alert padding X. Default: 1rem
    /// </summary>
    [CssVariable("bs-alert-padding-x")]
    public string? AlertPaddingX { get; set; }

    /// <summary>
    /// Alert padding Y. Default: 1rem
    /// </summary>
    [CssVariable("bs-alert-padding-y")]
    public string? AlertPaddingY { get; set; }

    /// <summary>
    /// Alert margin bottom. Default: 1rem
    /// </summary>
    [CssVariable("bs-alert-margin-bottom")]
    public string? AlertMarginBottom { get; set; }

    /// <summary>
    /// Alert color. Default: inherit
    /// </summary>
    [CssVariable("bs-alert-color")]
    public string? AlertColor { get; set; }

    /// <summary>
    /// Alert border color. Default: transparent
    /// </summary>
    [CssVariable("bs-alert-border-color")]
    public string? AlertBorderColor { get; set; }

    /// <summary>
    /// Alert border. Default: var(--bs-border-width) solid var(--bs-alert-border-color)
    /// </summary>
    [CssVariable("bs-alert-border")]
    public string? AlertBorder { get; set; }

    /// <summary>
    /// Alert border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-alert-border-radius")]
    public string? AlertBorderRadius { get; set; }

    /// <summary>
    /// Alert link color. Default: inherit
    /// </summary>
    [CssVariable("bs-alert-link-color")]
    public string? AlertLinkColor { get; set; }

    // Primary Alert
    /// <summary>
    /// Alert primary color. Default: var(--bs-primary-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-primary-color")]
    public string? AlertPrimaryColor { get; set; }

    /// <summary>
    /// Alert primary background color. Default: var(--bs-primary-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-primary-bg")]
    public string? AlertPrimaryBg { get; set; }

    /// <summary>
    /// Alert primary border color. Default: var(--bs-primary-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-primary-border-color")]
    public string? AlertPrimaryBorderColor { get; set; }

    /// <summary>
    /// Alert primary link color. Default: var(--bs-primary-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-primary-link-color")]
    public string? AlertPrimaryLinkColor { get; set; }

    // Secondary Alert
    /// <summary>
    /// Alert secondary color. Default: var(--bs-secondary-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-secondary-color")]
    public string? AlertSecondaryColor { get; set; }

    /// <summary>
    /// Alert secondary background color. Default: var(--bs-secondary-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-secondary-bg")]
    public string? AlertSecondaryBg { get; set; }

    /// <summary>
    /// Alert secondary border color. Default: var(--bs-secondary-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-secondary-border-color")]
    public string? AlertSecondaryBorderColor { get; set; }

    /// <summary>
    /// Alert secondary link color. Default: var(--bs-secondary-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-secondary-link-color")]
    public string? AlertSecondaryLinkColor { get; set; }

    // Success Alert
    /// <summary>
    /// Alert success color. Default: var(--bs-success-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-success-color")]
    public string? AlertSuccessColor { get; set; }

    /// <summary>
    /// Alert success background color. Default: var(--bs-success-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-success-bg")]
    public string? AlertSuccessBg { get; set; }

    /// <summary>
    /// Alert success border color. Default: var(--bs-success-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-success-border-color")]
    public string? AlertSuccessBorderColor { get; set; }

    /// <summary>
    /// Alert success link color. Default: var(--bs-success-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-success-link-color")]
    public string? AlertSuccessLinkColor { get; set; }

    // Info Alert
    /// <summary>
    /// Alert info color. Default: var(--bs-info-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-info-color")]
    public string? AlertInfoColor { get; set; }

    /// <summary>
    /// Alert info background color. Default: var(--bs-info-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-info-bg")]
    public string? AlertInfoBg { get; set; }

    /// <summary>
    /// Alert info border color. Default: var(--bs-info-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-info-border-color")]
    public string? AlertInfoBorderColor { get; set; }

    /// <summary>
    /// Alert info link color. Default: var(--bs-info-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-info-link-color")]
    public string? AlertInfoLinkColor { get; set; }

    // Warning Alert
    /// <summary>
    /// Alert warning color. Default: var(--bs-warning-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-warning-color")]
    public string? AlertWarningColor { get; set; }

    /// <summary>
    /// Alert warning background color. Default: var(--bs-warning-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-warning-bg")]
    public string? AlertWarningBg { get; set; }

    /// <summary>
    /// Alert warning border color. Default: var(--bs-warning-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-warning-border-color")]
    public string? AlertWarningBorderColor { get; set; }

    /// <summary>
    /// Alert warning link color. Default: var(--bs-warning-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-warning-link-color")]
    public string? AlertWarningLinkColor { get; set; }

    // Danger Alert
    /// <summary>
    /// Alert danger color. Default: var(--bs-danger-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-danger-color")]
    public string? AlertDangerColor { get; set; }

    /// <summary>
    /// Alert danger background color. Default: var(--bs-danger-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-danger-bg")]
    public string? AlertDangerBg { get; set; }

    /// <summary>
    /// Alert danger border color. Default: var(--bs-danger-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-danger-border-color")]
    public string? AlertDangerBorderColor { get; set; }

    /// <summary>
    /// Alert danger link color. Default: var(--bs-danger-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-danger-link-color")]
    public string? AlertDangerLinkColor { get; set; }

    // Light Alert
    /// <summary>
    /// Alert light color. Default: var(--bs-light-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-light-color")]
    public string? AlertLightColor { get; set; }

    /// <summary>
    /// Alert light background color. Default: var(--bs-light-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-light-bg")]
    public string? AlertLightBg { get; set; }

    /// <summary>
    /// Alert light border color. Default: var(--bs-light-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-light-border-color")]
    public string? AlertLightBorderColor { get; set; }

    /// <summary>
    /// Alert light link color. Default: var(--bs-light-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-light-link-color")]
    public string? AlertLightLinkColor { get; set; }

    // Dark Alert
    /// <summary>
    /// Alert dark color. Default: var(--bs-dark-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-dark-color")]
    public string? AlertDarkColor { get; set; }

    /// <summary>
    /// Alert dark background color. Default: var(--bs-dark-bg-subtle)
    /// </summary>
    [CssVariable("bs-alert-dark-bg")]
    public string? AlertDarkBg { get; set; }

    /// <summary>
    /// Alert dark border color. Default: var(--bs-dark-border-subtle)
    /// </summary>
    [CssVariable("bs-alert-dark-border-color")]
    public string? AlertDarkBorderColor { get; set; }

    /// <summary>
    /// Alert dark link color. Default: var(--bs-dark-text-emphasis)
    /// </summary>
    [CssVariable("bs-alert-dark-link-color")]
    public string? AlertDarkLinkColor { get; set; }
}
