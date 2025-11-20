using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for progress.
/// </summary>
public sealed class BootstrapProgressCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the progress background.
	/// </summary>
    public string? Background { get; set; }

	/// <summary>
	/// Gets the CSS selector for the progress component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".progress";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the progress component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Background.HasContent())
            yield return ("--bs-progress-bg", Background);
    }
}