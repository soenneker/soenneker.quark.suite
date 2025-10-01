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
}
