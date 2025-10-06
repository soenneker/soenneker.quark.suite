namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's carousel CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapCarouselCssVariables
{
    /// <summary>
    /// Carousel indicator active background. Default: #fff
    /// </summary>
    [CssVariable("bs-carousel-indicator-active-bg")]
    public string? CarouselIndicatorActiveBg { get; set; }

    /// <summary>
    /// Carousel caption color. Default: #fff
    /// </summary>
    [CssVariable("bs-carousel-caption-color")]
    public string? CarouselCaptionColor { get; set; }

    /// <summary>
    /// Carousel control icon filter. Default: none
    /// </summary>
    [CssVariable("bs-carousel-control-icon-filter")]
    public string? CarouselControlIconFilter { get; set; }
}
