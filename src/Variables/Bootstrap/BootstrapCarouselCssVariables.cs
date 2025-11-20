using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapCarouselCssVariables : IBootstrapCssVariableGroup
{
	public string? ControlColor { get; set; }

	public string? ControlWidth { get; set; }

	public string? IndicatorWidth { get; set; }

	public string? IndicatorHeight { get; set; }

    /// <summary>
    /// Carousel indicator active background. Default: #fff
    /// </summary>
    public string? IndicatorActiveBg { get; set; }

    /// <summary>
    /// Carousel caption color. Default: #fff
    /// </summary>
    public string? CaptionColor { get; set; }

    /// <summary>
    /// Carousel control icon filter. Default: none
    /// </summary>
    public string? ControlIconFilter { get; set; }

    public string GetSelector()
    {
        return ".carousel";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (ControlColor.HasContent())
            yield return ("--bs-carousel-control-color", ControlColor);
        if (ControlWidth.HasContent())
            yield return ("--bs-carousel-control-width", ControlWidth);
        if (IndicatorWidth.HasContent())
            yield return ("--bs-carousel-indicator-width", IndicatorWidth);
        if (IndicatorHeight.HasContent())
            yield return ("--bs-carousel-indicator-height", IndicatorHeight);
        if (IndicatorActiveBg.HasContent())
            yield return ("--bs-carousel-indicator-active-bg", IndicatorActiveBg);
        if (CaptionColor.HasContent())
            yield return ("--bs-carousel-caption-color", CaptionColor);
        if (ControlIconFilter.HasContent())
            yield return ("--bs-carousel-control-icon-filter", ControlIconFilter);
    }
}
