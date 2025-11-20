using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap dropdown toggle component.
/// </summary>
public sealed class BootstrapDropdownToggleCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the dropdown toggle spacer.
	/// </summary>
	public string? Spacer { get; set; }

	/// <summary>
	/// Gets the CSS selector for the dropdown toggle component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return "[data-bs-toggle=dropdown]";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the dropdown toggle component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Spacer.HasContent())
            yield return ("--bs-dropdown-spacer", Spacer);
    }
}


