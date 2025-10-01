namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-check inputs and switches.
/// </summary>
[CssSelector(".form-check-input")]
public class BootstrapFormCheckInputCssVariables
{
	[CssVariable("bs-form-check-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-form-check-border-color")]
	public string? BorderColor { get; set; }

	[CssVariable("bs-form-check-bg-image")]
	public string? BackgroundImage { get; set; }

	[CssVariable("bs-form-check-checked-bg-image")]
	public string? CheckedBackgroundImage { get; set; }

	[CssVariable("bs-form-check-checked-color")]
	public string? CheckedColor { get; set; }
}

