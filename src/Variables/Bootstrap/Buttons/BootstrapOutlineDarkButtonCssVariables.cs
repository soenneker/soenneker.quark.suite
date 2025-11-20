using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap outline dark button CSS variables
/// </summary>
public sealed class BootstrapOutlineDarkButtonCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button hover text color.
	/// </summary>
	public string? HoverColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button hover background color.
	/// </summary>
	public string? HoverBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button hover border color.
	/// </summary>
	public string? HoverBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button active text color.
	/// </summary>
	public string? ActiveColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button active background color.
	/// </summary>
	public string? ActiveBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button active border color.
	/// </summary>
	public string? ActiveBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button disabled text color.
	/// </summary>
	public string? DisabledColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button disabled background color.
	/// </summary>
	public string? DisabledBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the outline dark button disabled border color.
	/// </summary>
	public string? DisabledBorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the outline dark button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".btn-outline-dark";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the outline dark button component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-btn-color", Color);
        if (BorderColor.HasContent())
            yield return ("--bs-btn-border-color", BorderColor);
        if (HoverColor.HasContent())
            yield return ("--bs-btn-hover-color", HoverColor);
        if (HoverBackground.HasContent())
            yield return ("--bs-btn-hover-bg", HoverBackground);
        if (HoverBorderColor.HasContent())
            yield return ("--bs-btn-hover-border-color", HoverBorderColor);
        if (ActiveColor.HasContent())
            yield return ("--bs-btn-active-color", ActiveColor);
        if (ActiveBackground.HasContent())
            yield return ("--bs-btn-active-bg", ActiveBackground);
        if (ActiveBorderColor.HasContent())
            yield return ("--bs-btn-active-border-color", ActiveBorderColor);
        if (DisabledColor.HasContent())
            yield return ("--bs-btn-disabled-color", DisabledColor);
        if (DisabledBackground.HasContent())
            yield return ("--bs-btn-disabled-bg", DisabledBackground);
        if (DisabledBorderColor.HasContent())
            yield return ("--bs-btn-disabled-border-color", DisabledBorderColor);
    }
}


