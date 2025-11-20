using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for any Bootstrap button variant.
/// </summary>
public abstract class BootstrapBaseButtonCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the button text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button hover text color.
	/// </summary>
	public string? HoverColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button hover background color.
	/// </summary>
	public string? HoverBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button hover border color.
	/// </summary>
	public string? HoverBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button focus shadow RGB.
	/// </summary>
	public string? FocusShadowRgb { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button active text color.
	/// </summary>
	public string? ActiveColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button active background color.
	/// </summary>
	public string? ActiveBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button active border color.
	/// </summary>
	public string? ActiveBorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button active shadow.
	/// </summary>
	public string? ActiveShadow { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button disabled text color.
	/// </summary>
	public string? DisabledColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button disabled background color.
	/// </summary>
	public string? DisabledBackground { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the button disabled border color.
	/// </summary>
	public string? DisabledBorderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public virtual string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the button component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public virtual IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-btn-color", Color);
        if (Background.HasContent())
            yield return ("--bs-btn-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-btn-border-color", BorderColor);
        if (HoverColor.HasContent())
            yield return ("--bs-btn-hover-color", HoverColor);
        if (HoverBackground.HasContent())
            yield return ("--bs-btn-hover-bg", HoverBackground);
        if (HoverBorderColor.HasContent())
            yield return ("--bs-btn-hover-border-color", HoverBorderColor);
        if (FocusShadowRgb.HasContent())
            yield return ("--bs-btn-focus-shadow-rgb", FocusShadowRgb);
        if (ActiveColor.HasContent())
            yield return ("--bs-btn-active-color", ActiveColor);
        if (ActiveBackground.HasContent())
            yield return ("--bs-btn-active-bg", ActiveBackground);
        if (ActiveBorderColor.HasContent())
            yield return ("--bs-btn-active-border-color", ActiveBorderColor);
        if (ActiveShadow.HasContent())
            yield return ("--bs-btn-active-shadow", ActiveShadow);
        if (DisabledColor.HasContent())
            yield return ("--bs-btn-disabled-color", DisabledColor);
        if (DisabledBackground.HasContent())
            yield return ("--bs-btn-disabled-bg", DisabledBackground);
        if (DisabledBorderColor.HasContent())
            yield return ("--bs-btn-disabled-border-color", DisabledBorderColor);
    }
}


