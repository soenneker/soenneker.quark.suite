namespace Soenneker.Quark;

[CssSelector(".accordion")]
public class BootstrapAccordionCssVariables
{
	[CssVariable("bs-accordion-color")]
	public string? Color { get; set; }

	[CssVariable("bs-accordion-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-accordion-border-color")]
	public string? BorderColor { get; set; }
}

