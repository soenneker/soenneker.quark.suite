namespace Soenneker.Quark.Variables.Bootstrap;

/// <summary>
/// Bootstrap's card CSS variables
/// </summary>
public sealed class BootstrapCardsCssVariables
{
    /// <summary>
    /// Card spacer Y. Default: 1rem
    /// </summary>
    [CssVariable("bs-card-spacer-y")]
    public string? CardSpacerY { get; set; }

    /// <summary>
    /// Card spacer X. Default: 1rem
    /// </summary>
    [CssVariable("bs-card-spacer-x")]
    public string? CardSpacerX { get; set; }

    /// <summary>
    /// Card title spacer Y. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-card-title-spacer-y")]
    public string? CardTitleSpacerY { get; set; }

    /// <summary>
    /// Card title color. Default: var(--bs-heading-color)
    /// </summary>
    [CssVariable("bs-card-title-color")]
    public string? CardTitleColor { get; set; }

    /// <summary>
    /// Card subtitle color. Default: var(--bs-secondary-color)
    /// </summary>
    [CssVariable("bs-card-subtitle-color")]
    public string? CardSubtitleColor { get; set; }

    /// <summary>
    /// Card border width. Default: var(--bs-border-width)
    /// </summary>
    [CssVariable("bs-card-border-width")]
    public string? CardBorderWidth { get; set; }

    /// <summary>
    /// Card border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-card-border-radius")]
    public string? CardBorderRadius { get; set; }

    /// <summary>
    /// Card border color. Default: var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-card-border-color")]
    public string? CardBorderColor { get; set; }

    /// <summary>
    /// Card inner border radius. Default: calc(var(--bs-border-radius) - var(--bs-border-width))
    /// </summary>
    [CssVariable("bs-card-inner-border-radius")]
    public string? CardInnerBorderRadius { get; set; }

    /// <summary>
    /// Card cap padding Y. Default: 0.5rem
    /// </summary>
    [CssVariable("bs-card-cap-padding-y")]
    public string? CardCapPaddingY { get; set; }

    /// <summary>
    /// Card cap padding X. Default: 1rem
    /// </summary>
    [CssVariable("bs-card-cap-padding-x")]
    public string? CardCapPaddingX { get; set; }

    /// <summary>
    /// Card cap color. Default: var(--bs-body-color)
    /// </summary>
    [CssVariable("bs-card-cap-color")]
    public string? CardCapColor { get; set; }

    /// <summary>
    /// Card cap background color. Default: rgba(var(--bs-body-color-rgb), 0.03)
    /// </summary>
    [CssVariable("bs-card-cap-bg")]
    public string? CardCapBg { get; set; }

    /// <summary>
    /// Card img overlay padding. Default: 1rem
    /// </summary>
    [CssVariable("bs-card-img-overlay-padding")]
    public string? CardImgOverlayPadding { get; set; }

    /// <summary>
    /// Card group margin. Default: calc(var(--bs-gutter-x) * 0.5)
    /// </summary>
    [CssVariable("bs-card-group-margin")]
    public string? CardGroupMargin { get; set; }

    /// <summary>
    /// Card deck gap. Default: var(--bs-card-group-margin)
    /// </summary>
    [CssVariable("bs-card-deck-gap")]
    public string? CardDeckGap { get; set; }

    /// <summary>
    /// Card columns count. Default: 3
    /// </summary>
    [CssVariable("bs-card-columns-count")]
    public string? CardColumnsCount { get; set; }

    /// <summary>
    /// Card columns gap. Default: 1.25rem
    /// </summary>
    [CssVariable("bs-card-columns-gap")]
    public string? CardColumnsGap { get; set; }

    /// <summary>
    /// Card columns margin. Default: var(--bs-card-columns-gap)
    /// </summary>
    [CssVariable("bs-card-columns-margin")]
    public string? CardColumnsMargin { get; set; }
}
