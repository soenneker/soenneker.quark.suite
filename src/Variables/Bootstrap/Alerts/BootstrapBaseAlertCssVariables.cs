namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap alerts.
/// </summary>
[CssSelector]
public abstract class BootstrapBaseAlertCssVariables
{
	[CssVariable("bs-alert-color")]
	public string? Color { get; set; }

	[CssVariable("bs-alert-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-alert-border-color")]
	public string? BorderColor { get; set; }

	[CssVariable("bs-alert-link-color")]
	public string? LinkColor { get; set; }
}


