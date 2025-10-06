namespace Soenneker.Quark;

[CssSelector(".carousel")]
public class BootstrapCarouselCssVariables
{
	[CssVariable("bs-carousel-control-color")]
	public string? ControlColor { get; set; }

	[CssVariable("bs-carousel-control-width")]
	public string? ControlWidth { get; set; }

	[CssVariable("bs-carousel-indicator-width")]
	public string? IndicatorWidth { get; set; }

	[CssVariable("bs-carousel-indicator-height")]
	public string? IndicatorHeight { get; set; }

    /// <summary>
    /// Carousel indicator active background. Default: #fff
    /// </summary>
    [CssVariable("bs-carousel-indicator-active-bg")]
    public string? IndicatorActiveBg { get; set; }

    /// <summary>
    /// Carousel caption color. Default: #fff
    /// </summary>
    [CssVariable("bs-carousel-caption-color")]
    public string? CaptionColor { get; set; }

    /// <summary>
    /// Carousel control icon filter. Default: none
    /// </summary>
    [CssVariable("bs-carousel-control-icon-filter")]
    public string? ControlIconFilter { get; set; }
}
