namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for .form-control and similar text inputs.
/// </summary>
[CssSelector]
public abstract class BootstrapBaseFormControlCssVariables
{
	[CssVariable("bs-border-width")]
	public string? BorderWidth { get; set; }

	[CssVariable("bs-border-color")]
	public string? BorderColor { get; set; }

	[CssVariable("bs-border-radius")]
	public string? BorderRadius { get; set; }

	[CssVariable("bs-focus-ring-color")]
	public string? FocusRingColor { get; set; }

	[CssVariable("bs-form-control-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-form-control-color")]
	public string? Color { get; set; }

	[CssVariable("bs-form-control-disabled-bg")]
	public string? DisabledBackground { get; set; }
}


