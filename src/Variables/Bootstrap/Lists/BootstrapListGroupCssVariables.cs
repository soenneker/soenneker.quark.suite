namespace Soenneker.Quark;

/// <summary>
/// Variables for .list-group and items.
/// </summary>
[CssSelector(".list-group")]
public class BootstrapListGroupCssVariables
{
	[CssVariable("bs-list-group-color")]
	public string? Color { get; set; }

	[CssVariable("bs-list-group-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-list-group-border-color")]
	public string? BorderColor { get; set; }
}

