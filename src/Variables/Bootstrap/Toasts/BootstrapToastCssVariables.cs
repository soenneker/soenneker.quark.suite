namespace Soenneker.Quark;

[CssSelector(".toast")]
public class BootstrapToastCssVariables
{
	[CssVariable("bs-toast-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-toast-color")]
	public string? Color { get; set; }

	[CssVariable("bs-toast-border-color")]
	public string? BorderColor { get; set; }
}


