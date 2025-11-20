using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for any Bootstrap button variant.
/// </summary>
public abstract class BootstrapBaseButtonCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

	public string? HoverColor { get; set; }

	public string? HoverBackground { get; set; }

	public string? HoverBorderColor { get; set; }

	public string? FocusShadowRgb { get; set; }

	public string? ActiveColor { get; set; }

	public string? ActiveBackground { get; set; }

	public string? ActiveBorderColor { get; set; }

	public string? ActiveShadow { get; set; }

	public string? DisabledColor { get; set; }

	public string? DisabledBackground { get; set; }

	public string? DisabledBorderColor { get; set; }

    public virtual string GetSelector()
    {
        return ":root";
    }

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


