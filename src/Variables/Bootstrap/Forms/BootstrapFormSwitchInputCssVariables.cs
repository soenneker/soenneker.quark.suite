using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap form switch input component.
/// </summary>
public sealed class BootstrapFormSwitchInputCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the form switch input background.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form switch input color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets the CSS selector for the form switch input component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".form-switch .form-check-input";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the form switch input component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Background.HasContent())
            yield return ("--bs-form-switch-bg", Background);
        if (Color.HasContent())
            yield return ("--bs-form-switch-color", Color);
    }
}

