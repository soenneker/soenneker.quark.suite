namespace Soenneker.Quark;

[CssSelector(".table-hover")]
public class BootstrapTableHoverCssVariables
{
	[CssVariable("bs-table-hover-bg")]
	public string? HoverBackground { get; set; }

	[CssVariable("bs-table-hover-color")]
	public string? HoverColor { get; set; }
}

