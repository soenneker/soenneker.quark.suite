using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap hover table component.
/// </summary>
public sealed class BootstrapTableHoverCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the hover table background.
	/// </summary>
	public string? HoverBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the hover table color.
	/// </summary>
	public string? HoverColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the hover table component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".table-hover";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the hover table component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (HoverBackground.HasContent())
            yield return ("--bs-table-hover-bg", HoverBackground);
        if (HoverColor.HasContent())
            yield return ("--bs-table-hover-color", HoverColor);
    }
}

