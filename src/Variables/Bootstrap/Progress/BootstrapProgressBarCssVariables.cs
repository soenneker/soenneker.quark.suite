namespace Soenneker.Quark;

[CssSelector(".progress-bar")]
public class BootstrapProgressBarCssVariables
{
	[CssVariable("bs-progress-bar-color")]
	public string? Color { get; set; }

	[CssVariable("bs-progress-bar-bg")]
	public string? Background { get; set; }
}

