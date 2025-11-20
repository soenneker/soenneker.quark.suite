using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap progress bar component.
/// </summary>
public sealed class BootstrapProgressBarCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the progress bar text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the progress bar background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets the CSS selector for the progress bar component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".progress-bar";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the progress bar component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-progress-bar-color", Color);
        if (Background.HasContent())
            yield return ("--bs-progress-bar-bg", Background);
    }
}

