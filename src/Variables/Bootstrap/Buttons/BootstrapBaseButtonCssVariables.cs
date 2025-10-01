namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for any Bootstrap button variant.
/// </summary>
[CssSelector]
public abstract class BootstrapBaseButtonCssVariables
{
	[CssVariable("bs-btn-color")]
	public string? Color { get; set; }

	[CssVariable("bs-btn-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-btn-border-color")]
	public string? BorderColor { get; set; }

	[CssVariable("bs-btn-hover-color")]
	public string? HoverColor { get; set; }

	[CssVariable("bs-btn-hover-bg")]
	public string? HoverBackground { get; set; }

	[CssVariable("bs-btn-hover-border-color")]
	public string? HoverBorderColor { get; set; }

	[CssVariable("bs-btn-focus-shadow-rgb")]
	public string? FocusShadowRgb { get; set; }

	[CssVariable("bs-btn-active-color")]
	public string? ActiveColor { get; set; }

	[CssVariable("bs-btn-active-bg")]
	public string? ActiveBackground { get; set; }

	[CssVariable("bs-btn-active-border-color")]
	public string? ActiveBorderColor { get; set; }

	[CssVariable("bs-btn-active-shadow")]
	public string? ActiveShadow { get; set; }

	[CssVariable("bs-btn-disabled-color")]
	public string? DisabledColor { get; set; }

	[CssVariable("bs-btn-disabled-bg")]
	public string? DisabledBackground { get; set; }

	[CssVariable("bs-btn-disabled-border-color")]
	public string? DisabledBorderColor { get; set; }
}


