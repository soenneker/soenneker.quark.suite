using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for pagination.
/// </summary>
public sealed class BootstrapPaginationCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the pagination text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the pagination background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the pagination border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the pagination component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".pagination";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the pagination component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-pagination-color", Color);
        if (Background.HasContent())
            yield return ("--bs-pagination-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-pagination-border-color", BorderColor);
    }
}

