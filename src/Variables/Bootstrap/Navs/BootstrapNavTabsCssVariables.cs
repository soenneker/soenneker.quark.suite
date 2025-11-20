using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap nav tabs component.
/// </summary>
public sealed class BootstrapNavTabsCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the nav tabs link hover border color.
	/// </summary>
	public string? LinkHoverBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the nav tabs link active text color.
	/// </summary>
	public string? LinkActiveColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the nav tabs link active background color.
	/// </summary>
	public string? LinkActiveBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the nav tabs link active border color.
	/// </summary>
	public string? LinkActiveBorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the nav tabs component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".nav-tabs";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the nav tabs component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (LinkHoverBorderColor.HasContent())
            yield return ("--bs-nav-tabs-link-hover-border-color", LinkHoverBorderColor);
        if (LinkActiveColor.HasContent())
            yield return ("--bs-nav-tabs-link-active-color", LinkActiveColor);
        if (LinkActiveBackground.HasContent())
            yield return ("--bs-nav-tabs-link-active-bg", LinkActiveBackground);
        if (LinkActiveBorderColor.HasContent())
            yield return ("--bs-nav-tabs-link-active-border-color", LinkActiveBorderColor);
    }
}


