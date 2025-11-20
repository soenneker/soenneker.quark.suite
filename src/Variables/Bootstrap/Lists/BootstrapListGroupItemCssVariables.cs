using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// CSS variables for Bootstrap list group item component.
/// </summary>
public sealed class BootstrapListGroupItemCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the list group item vertical padding.
	/// </summary>
	public string? PaddingY { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group item horizontal padding.
	/// </summary>
	public string? PaddingX { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group item action hover color.
	/// </summary>
	public string? ActionHoverColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group item action hover background.
	/// </summary>
	public string? ActionHoverBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group item active color.
	/// </summary>
	public string? ActiveColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group item active background.
	/// </summary>
	public string? ActiveBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the list group item active border color.
	/// </summary>
	public string? ActiveBorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the list group item component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".list-group-item";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the list group item component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (PaddingY.HasContent())
            yield return ("--bs-list-group-item-padding-y", PaddingY);
        if (PaddingX.HasContent())
            yield return ("--bs-list-group-item-padding-x", PaddingX);
        if (ActionHoverColor.HasContent())
            yield return ("--bs-list-group-action-hover-color", ActionHoverColor);
        if (ActionHoverBackground.HasContent())
            yield return ("--bs-list-group-action-hover-bg", ActionHoverBackground);
        if (ActiveColor.HasContent())
            yield return ("--bs-list-group-active-color", ActiveColor);
        if (ActiveBackground.HasContent())
            yield return ("--bs-list-group-active-bg", ActiveBackground);
        if (ActiveBorderColor.HasContent())
            yield return ("--bs-list-group-active-border-color", ActiveBorderColor);
    }
}

