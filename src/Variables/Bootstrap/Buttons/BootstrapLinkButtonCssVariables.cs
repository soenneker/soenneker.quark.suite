namespace Soenneker.Quark;

/// <summary>
/// Bootstrap link button CSS variables
/// </summary>
[CssSelector(".btn-link")]
public class BootstrapLinkButtonCssVariables
{
	[CssVariable("bs-btn-color")]
	public string? Color { get; set; }

	[CssVariable("bs-btn-hover-color")]
	public string? HoverColor { get; set; }

	[CssVariable("bs-btn-disabled-color")]
	public string? DisabledColor { get; set; }
}


