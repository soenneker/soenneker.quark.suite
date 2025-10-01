namespace Soenneker.Quark;

[CssSelector(".page-link")]
public class BootstrapPageLinkCssVariables
{
	[CssVariable("bs-pagination-hover-color")]
	public string? HoverColor { get; set; }

	[CssVariable("bs-pagination-hover-bg")]
	public string? HoverBackground { get; set; }

	[CssVariable("bs-pagination-hover-border-color")]
	public string? HoverBorderColor { get; set; }

	[CssVariable("bs-pagination-active-color")]
	public string? ActiveColor { get; set; }

	[CssVariable("bs-pagination-active-bg")]
	public string? ActiveBackground { get; set; }

	[CssVariable("bs-pagination-active-border-color")]
	public string? ActiveBorderColor { get; set; }

	[CssVariable("bs-pagination-disabled-color")]
	public string? DisabledColor { get; set; }

	[CssVariable("bs-pagination-disabled-bg")]
	public string? DisabledBackground { get; set; }

	[CssVariable("bs-pagination-disabled-border-color")]
	public string? DisabledBorderColor { get; set; }
}

