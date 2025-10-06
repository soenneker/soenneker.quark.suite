namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's spinner CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapSpinnerCssVariables
{
    /// <summary>
    /// Spinner width. Default: 2rem
    /// </summary>
    [CssVariable("bs-spinner-width")]
    public string? SpinnerWidth { get; set; }

    /// <summary>
    /// Spinner height. Default: 2rem
    /// </summary>
    [CssVariable("bs-spinner-height")]
    public string? SpinnerHeight { get; set; }

    /// <summary>
    /// Spinner vertical align. Default: -0.125em
    /// </summary>
    [CssVariable("bs-spinner-vertical-align")]
    public string? SpinnerVerticalAlign { get; set; }

    /// <summary>
    /// Spinner border width. Default: 0.25em
    /// </summary>
    [CssVariable("bs-spinner-border-width")]
    public string? SpinnerBorderWidth { get; set; }

    /// <summary>
    /// Spinner animation speed. Default: 0.75s
    /// </summary>
    [CssVariable("bs-spinner-animation-speed")]
    public string? SpinnerAnimationSpeed { get; set; }

    /// <summary>
    /// Spinner animation name. Default: spinner-border
    /// </summary>
    [CssVariable("bs-spinner-animation-name")]
    public string? SpinnerAnimationName { get; set; }
}
