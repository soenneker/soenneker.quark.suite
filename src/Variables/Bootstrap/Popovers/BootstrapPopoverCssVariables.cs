namespace Soenneker.Quark;

[CssSelector(".popover")]
public class BootstrapPopoverCssVariables
{
	[CssVariable("bs-popover-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-popover-border-color")]
	public string? BorderColor { get; set; }

	[CssVariable("bs-popover-header-bg")]
	public string? HeaderBackground { get; set; }

	[CssVariable("bs-popover-header-color")]
	public string? HeaderColor { get; set; }
}


