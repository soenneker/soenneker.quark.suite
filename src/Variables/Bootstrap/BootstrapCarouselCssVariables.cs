using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's carousel CSS variables.
/// </summary>
public sealed class BootstrapCarouselCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the carousel control color.
	/// </summary>
	public string? ControlColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the carousel control width.
	/// </summary>
	public string? ControlWidth { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the carousel indicator width.
	/// </summary>
	public string? IndicatorWidth { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the carousel indicator height.
	/// </summary>
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

	/// <summary>
	/// Gets the CSS selector for the carousel component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".carousel";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the carousel component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
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
