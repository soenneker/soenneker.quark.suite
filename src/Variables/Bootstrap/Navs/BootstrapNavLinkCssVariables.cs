using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap nav link component.
/// </summary>
public sealed class BootstrapNavLinkCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the nav link text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the nav link hover text color.
	/// </summary>
	public string? HoverColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the nav link disabled text color.
	/// </summary>
	public string? DisabledColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the nav link component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".nav-link";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the nav link component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-nav-link-color", Color);
        if (HoverColor.HasContent())
            yield return ("--bs-nav-link-hover-color", HoverColor);
        if (DisabledColor.HasContent())
            yield return ("--bs-nav-link-disabled-color", DisabledColor);
    }
}


