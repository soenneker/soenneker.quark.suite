using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .list-group and items.
/// </summary>
public sealed class BootstrapListGroupCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the list group text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the list group component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".list-group";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the list group component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-list-group-color", Color);
        if (Background.HasContent())
            yield return ("--bs-list-group-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-list-group-border-color", BorderColor);
    }
}

