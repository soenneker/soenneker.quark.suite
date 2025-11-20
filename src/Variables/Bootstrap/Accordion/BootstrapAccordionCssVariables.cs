using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap accordion component.
/// </summary>
public sealed class BootstrapAccordionCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the accordion text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the accordion background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the accordion border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the accordion component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
	public string GetSelector()
	{
		return ".accordion";
	}

	/// <summary>
	/// Gets the collection of CSS variables for the accordion component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
	public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
	{
		if (Color.HasContent())
			yield return ("--bs-accordion-color", Color);

		if (Background.HasContent())
			yield return ("--bs-accordion-bg", Background);

		if (BorderColor.HasContent())
			yield return ("--bs-accordion-border-color", BorderColor);
	}
}

