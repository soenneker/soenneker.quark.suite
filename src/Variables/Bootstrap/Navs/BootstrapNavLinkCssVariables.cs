namespace Soenneker.Quark;

[CssSelector(".nav-link")]
public class BootstrapNavLinkCssVariables
{
	[CssVariable("bs-nav-link-color")]
	public string? Color { get; set; }

	[CssVariable("bs-nav-link-hover-color")]
	public string? HoverColor { get; set; }

	[CssVariable("bs-nav-link-disabled-color")]
	public string? DisabledColor { get; set; }
}


