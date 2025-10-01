namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap cards.
/// </summary>
[CssSelector(".card")]
public class BootstrapCardCssVariables
{
	[CssVariable("bs-card-color")]
	public string? Color { get; set; }

	[CssVariable("bs-card-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-card-border-color")]
	public string? BorderColor { get; set; }

	[CssVariable("bs-card-border-width")]
	public string? BorderWidth { get; set; }

	[CssVariable("bs-card-border-radius")]
	public string? BorderRadius { get; set; }

	[CssVariable("bs-card-cap-bg")]
	public string? CapBackground { get; set; }

	[CssVariable("bs-card-cap-color")]
	public string? CapColor { get; set; }
}


