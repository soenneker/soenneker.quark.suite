namespace Soenneker.Quark;

[CssSelector(".table")]
public class BootstrapTableCssVariables
{
	[CssVariable("bs-table-color")]
	public string? Color { get; set; }

	[CssVariable("bs-table-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-table-border-color")]
	public string? BorderColor { get; set; }
}

