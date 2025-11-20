using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap dropdown item component.
/// </summary>
public sealed class BootstrapDropdownItemCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown item link color.
	/// </summary>
	public string? LinkColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown item link hover text color.
	/// </summary>
	public string? LinkHoverColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown item link hover background color.
	/// </summary>
	public string? LinkHoverBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown item link active text color.
	/// </summary>
	public string? LinkActiveColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown item link active background color.
	/// </summary>
	public string? LinkActiveBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown item link disabled text color.
	/// </summary>
	public string? LinkDisabledColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the dropdown item component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".dropdown-item";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the dropdown item component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (LinkColor.HasContent())
            yield return ("--bs-dropdown-link-color", LinkColor);
        if (LinkHoverColor.HasContent())
            yield return ("--bs-dropdown-link-hover-color", LinkHoverColor);
        if (LinkHoverBackground.HasContent())
            yield return ("--bs-dropdown-link-hover-bg", LinkHoverBackground);
        if (LinkActiveColor.HasContent())
            yield return ("--bs-dropdown-link-active-color", LinkActiveColor);
        if (LinkActiveBackground.HasContent())
            yield return ("--bs-dropdown-link-active-bg", LinkActiveBackground);
        if (LinkDisabledColor.HasContent())
            yield return ("--bs-dropdown-link-disabled-color", LinkDisabledColor);
    }
}


