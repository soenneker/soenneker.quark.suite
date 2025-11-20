using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for dropdown menus and items.
/// </summary>
public sealed class BootstrapDropdownMenuCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown menu text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown menu background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown menu border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the dropdown menu component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".dropdown-menu";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the dropdown menu component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-dropdown-color", Color);
        if (Background.HasContent())
            yield return ("--bs-dropdown-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-dropdown-border-color", BorderColor);
    }
}

