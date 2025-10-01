namespace Soenneker.Quark;

[CssSelector(".accordion-button")]
public class BootstrapAccordionButtonCssVariables
{
	[CssVariable("bs-accordion-btn-color")]
	public string? ButtonColor { get; set; }

	[CssVariable("bs-accordion-btn-bg")]
	public string? ButtonBackground { get; set; }

	[CssVariable("bs-accordion-btn-focus-border-color")]
	public string? ButtonFocusBorderColor { get; set; }

	[CssVariable("bs-accordion-btn-focus-box-shadow")]
	public string? ButtonFocusBoxShadow { get; set; }
}

