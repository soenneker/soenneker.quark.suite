namespace Soenneker.Quark;

[CssSelector(".modal-content")]
public class BootstrapModalContentCssVariables
{
	[CssVariable("bs-modal-color")]
	public string? Color { get; set; }

	[CssVariable("bs-modal-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-modal-border-color")]
	public string? BorderColor { get; set; }
}

