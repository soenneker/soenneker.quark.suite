using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap link button CSS variables
/// </summary>
public sealed class BootstrapLinkButtonCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the link button text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the link button hover text color.
	/// </summary>
	public string? HoverColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the link button disabled text color.
	/// </summary>
	public string? DisabledColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the link button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".btn-link";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the link button component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-btn-color", Color);
        if (HoverColor.HasContent())
            yield return ("--bs-btn-hover-color", HoverColor);
        if (DisabledColor.HasContent())
            yield return ("--bs-btn-disabled-color", DisabledColor);
    }
}


