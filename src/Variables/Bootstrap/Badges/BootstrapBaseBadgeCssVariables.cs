namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap badges.
/// </summary>
[CssSelector]
public abstract class BootstrapBaseBadgeCssVariables
{
	[CssVariable("bs-badge-color")]
	public string? Color { get; set; }

	[CssVariable("bs-badge-bg")]
	public string? Background { get; set; }
}


