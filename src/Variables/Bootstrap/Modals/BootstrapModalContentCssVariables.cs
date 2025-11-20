using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap modal content component.
/// </summary>
public sealed class BootstrapModalContentCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the modal content text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal content background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the modal content border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the modal content component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".modal-content";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the modal content component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-modal-color", Color);
        if (Background.HasContent())
            yield return ("--bs-modal-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-modal-border-color", BorderColor);
    }
}

