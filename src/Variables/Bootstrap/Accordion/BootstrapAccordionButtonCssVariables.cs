using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap accordion button component.
/// </summary>
public sealed class BootstrapAccordionButtonCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the accordion button text color.
	/// </summary>
    public string? ButtonColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the accordion button background color.
	/// </summary>
    public string? ButtonBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the accordion button focus border color.
	/// </summary>
    public string? ButtonFocusBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the accordion button focus box shadow.
	/// </summary>
    public string? ButtonFocusBoxShadow { get; set; }

	/// <summary>
	/// Gets the CSS selector for the accordion button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".accordion-button";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the accordion button component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (ButtonColor.HasContent())
            yield return ("--bs-accordion-btn-color", ButtonColor);

        if (ButtonBackground.HasContent())
            yield return ("--bs-accordion-btn-bg", ButtonBackground);

        if (ButtonFocusBorderColor.HasContent())
            yield return ("--bs-accordion-btn-focus-border-color", ButtonFocusBorderColor);

        if (ButtonFocusBoxShadow.HasContent())
            yield return ("--bs-accordion-btn-focus-box-shadow", ButtonFocusBoxShadow);
    }
}