namespace Soenneker.Quark;

[CssSelector(".list-group-item")]
public class BootstrapListGroupItemCssVariables
{
	[CssVariable("bs-list-group-item-padding-y")]
	public string? PaddingY { get; set; }

	[CssVariable("bs-list-group-item-padding-x")]
	public string? PaddingX { get; set; }

	[CssVariable("bs-list-group-action-hover-color")]
	public string? ActionHoverColor { get; set; }

	[CssVariable("bs-list-group-action-hover-bg")]
	public string? ActionHoverBackground { get; set; }

	[CssVariable("bs-list-group-active-color")]
	public string? ActiveColor { get; set; }

	[CssVariable("bs-list-group-active-bg")]
	public string? ActiveBackground { get; set; }

	[CssVariable("bs-list-group-active-border-color")]
	public string? ActiveBorderColor { get; set; }
}

