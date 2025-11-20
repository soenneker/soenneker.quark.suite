using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap striped table component.
/// </summary>
public sealed class BootstrapTableStripedCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the striped table background.
	/// </summary>
	public string? StripedBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the striped table color.
	/// </summary>
	public string? StripedColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the striped table component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".table-striped";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the striped table component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (StripedBackground.HasContent())
            yield return ("--bs-table-striped-bg", StripedBackground);
        if (StripedColor.HasContent())
            yield return ("--bs-table-striped-color", StripedColor);
    }
}

