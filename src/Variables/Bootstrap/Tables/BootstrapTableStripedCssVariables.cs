namespace Soenneker.Quark;

[CssSelector(".table-striped")]
public class BootstrapTableStripedCssVariables
{
	[CssVariable("bs-table-striped-bg")]
	public string? StripedBackground { get; set; }

	[CssVariable("bs-table-striped-color")]
	public string? StripedColor { get; set; }
}

