using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap badges.
/// </summary>
public abstract class BootstrapBaseBadgeCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the badge text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the badge background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the badge horizontal padding.
	/// </summary>
	public string? PaddingX { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the badge vertical padding.
	/// </summary>
	public string? PaddingY { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the badge font size.
	/// </summary>
	public string? FontSize { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the badge font weight.
	/// </summary>
	public string? FontWeight { get; set; }

	/// <summary>
	/// Gets the CSS selector for the badge component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public virtual string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the badge component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public virtual IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-badge-color", Color);
        if (Background.HasContent())
            yield return ("--bs-badge-bg", Background);
        if (PaddingX.HasContent())
            yield return ("--bs-badge-padding-x", PaddingX);
        if (PaddingY.HasContent())
            yield return ("--bs-badge-padding-y", PaddingY);
        if (FontSize.HasContent())
            yield return ("--bs-badge-font-size", FontSize);
        if (FontWeight.HasContent())
            yield return ("--bs-badge-font-weight", FontWeight);
    }
}


