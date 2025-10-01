namespace Soenneker.Quark;

[CssSelector(".nav-tabs")]
public class BootstrapNavTabsCssVariables
{
	[CssVariable("bs-nav-tabs-link-hover-border-color")]
	public string? LinkHoverBorderColor { get; set; }

	[CssVariable("bs-nav-tabs-link-active-color")]
	public string? LinkActiveColor { get; set; }

	[CssVariable("bs-nav-tabs-link-active-bg")]
	public string? LinkActiveBackground { get; set; }

	[CssVariable("bs-nav-tabs-link-active-border-color")]
	public string? LinkActiveBorderColor { get; set; }
}


