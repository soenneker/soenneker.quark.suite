namespace Soenneker.Quark.Variables.Bootstrap;

/// <summary>
/// Bootstrap's accordion CSS variables
/// </summary>
public sealed class BootstrapAccordionCssVariables
{
    /// <summary>
    /// Accordion padding Y. Default: 1rem
    /// </summary>
    [CssVariable("bs-accordion-padding-y")]
    public string? AccordionPaddingY { get; set; }

    /// <summary>
    /// Accordion padding X. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-accordion-padding-x")]
    public string? AccordionPaddingX { get; set; }

    /// <summary>
    /// Accordion color. Default: var(--bs-accordion-color)
    /// </summary>
    [CssVariable("bs-accordion-color")]
    public string? AccordionColor { get; set; }

    /// <summary>
    /// Accordion background color. Default: var(--bs-accordion-bg)
    /// </summary>
    [CssVariable("bs-accordion-bg")]
    public string? AccordionBg { get; set; }

    /// <summary>
    /// Accordion border. Default: var(--bs-accordion-border)
    /// </summary>
    [CssVariable("bs-accordion-border")]
    public string? AccordionBorder { get; set; }

    /// <summary>
    /// Accordion border radius. Default: var(--bs-accordion-border-radius)
    /// </summary>
    [CssVariable("bs-accordion-border-radius")]
    public string? AccordionBorderRadius { get; set; }

    /// <summary>
    /// Accordion inner border radius. Default: calc(var(--bs-border-radius) - var(--bs-border-width))
    /// </summary>
    [CssVariable("bs-accordion-inner-border-radius")]
    public string? AccordionInnerBorderRadius { get; set; }

    /// <summary>
    /// Accordion button padding Y. Default: 1rem
    /// </summary>
    [CssVariable("bs-accordion-button-padding-y")]
    public string? AccordionButtonPaddingY { get; set; }

    /// <summary>
    /// Accordion button padding X. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-accordion-button-padding-x")]
    public string? AccordionButtonPaddingX { get; set; }

    /// <summary>
    /// Accordion button color. Default: var(--bs-accordion-button-color)
    /// </summary>
    [CssVariable("bs-accordion-button-color")]
    public string? AccordionButtonColor { get; set; }

    /// <summary>
    /// Accordion button background color. Default: var(--bs-accordion-button-bg)
    /// </summary>
    [CssVariable("bs-accordion-button-bg")]
    public string? AccordionButtonBg { get; set; }

    /// <summary>
    /// Accordion button icon. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23212529'%3e%3cpath fill-rule='evenodd' d='M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z'/%3e%3c/svg%3e")
    /// </summary>
    [CssVariable("bs-accordion-button-icon")]
    public string? AccordionButtonIcon { get; set; }

    /// <summary>
    /// Accordion button active icon. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23052c65'%3e%3cpath fill-rule='evenodd' d='M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z'/%3e%3c/svg%3e")
    /// </summary>
    [CssVariable("bs-accordion-button-active-icon")]
    public string? AccordionButtonActiveIcon { get; set; }

    /// <summary>
    /// Accordion button icon width. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-accordion-button-icon-width")]
    public string? AccordionButtonIconWidth { get; set; }

    /// <summary>
    /// Accordion button icon transform. Default: rotate(-180deg)
    /// </summary>
    [CssVariable("bs-accordion-button-icon-transform")]
    public string? AccordionButtonIconTransform { get; set; }

    /// <summary>
    /// Accordion button icon transition. Default: transform 0.2s ease-in-out
    /// </summary>
    [CssVariable("bs-accordion-button-icon-transition")]
    public string? AccordionButtonIconTransition { get; set; }

    /// <summary>
    /// Accordion button active color. Default: var(--bs-primary-text-emphasis)
    /// </summary>
    [CssVariable("bs-accordion-button-active-color")]
    public string? AccordionButtonActiveColor { get; set; }

    /// <summary>
    /// Accordion button active background color. Default: var(--bs-primary-bg-subtle)
    /// </summary>
    [CssVariable("bs-accordion-button-active-bg")]
    public string? AccordionButtonActiveBg { get; set; }

    /// <summary>
    /// Accordion button focus z-index. Default: 3
    /// </summary>
    [CssVariable("bs-accordion-button-focus-z-index")]
    public string? AccordionButtonFocusZIndex { get; set; }

    /// <summary>
    /// Accordion button focus border color. Default: var(--bs-primary-border-subtle)
    /// </summary>
    [CssVariable("bs-accordion-button-focus-border-color")]
    public string? AccordionButtonFocusBorderColor { get; set; }

    /// <summary>
    /// Accordion button focus box shadow. Default: 0 0 0 0.25rem rgba(13, 110, 253, 0.25)
    /// </summary>
    [CssVariable("bs-accordion-button-focus-box-shadow")]
    public string? AccordionButtonFocusBoxShadow { get; set; }

    /// <summary>
    /// Accordion body padding Y. Default: 1rem
    /// </summary>
    [CssVariable("bs-accordion-body-padding-y")]
    public string? AccordionBodyPaddingY { get; set; }

    /// <summary>
    /// Accordion body padding X. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-accordion-body-padding-x")]
    public string? AccordionBodyPaddingX { get; set; }

    /// <summary>
    /// Accordion button disabled color. Default: var(--bs-secondary-color)
    /// </summary>
    [CssVariable("bs-accordion-button-disabled-color")]
    public string? AccordionButtonDisabledColor { get; set; }

    /// <summary>
    /// Accordion button disabled background color. Default: var(--bs-secondary-bg)
    /// </summary>
    [CssVariable("bs-accordion-button-disabled-bg")]
    public string? AccordionButtonDisabledBg { get; set; }

    /// <summary>
    /// Accordion button disabled border color. Default: var(--bs-border-color)
    /// </summary>
    [CssVariable("bs-accordion-button-disabled-border-color")]
    public string? AccordionButtonDisabledBorderColor { get; set; }

    /// <summary>
    /// Accordion button disabled opacity. Default: 0.65
    /// </summary>
    [CssVariable("bs-accordion-button-disabled-opacity")]
    public string? AccordionButtonDisabledOpacity { get; set; }
}
