namespace Soenneker.Quark;

/// <summary>
/// Variables for dropdown menus and items.
/// </summary>
[CssSelector(".dropdown-menu")]
public class BootstrapDropdownMenuCssVariables
{
	[CssVariable("bs-dropdown-color")]
	public string? Color { get; set; }

	[CssVariable("bs-dropdown-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-dropdown-border-color")]
	public string? BorderColor { get; set; }
}

