using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-check inputs and switches.
/// </summary>
public sealed class BootstrapFormCheckInputCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the form check input background.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form check input border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form check input background image.
	/// </summary>
	public string? BackgroundImage { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form check input checked background image.
	/// </summary>
	public string? CheckedBackgroundImage { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form check input checked color.
	/// </summary>
	public string? CheckedColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the form check input component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".form-check-input";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the form check input component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Background.HasContent())
            yield return ("--bs-form-check-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-form-check-border-color", BorderColor);
        if (BackgroundImage.HasContent())
            yield return ("--bs-form-check-bg-image", BackgroundImage);
        if (CheckedBackgroundImage.HasContent())
            yield return ("--bs-form-check-checked-bg-image", CheckedBackgroundImage);
        if (CheckedColor.HasContent())
            yield return ("--bs-form-check-checked-color", CheckedColor);
    }
}

