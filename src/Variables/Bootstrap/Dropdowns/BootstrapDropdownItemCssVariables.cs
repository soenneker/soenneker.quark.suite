namespace Soenneker.Quark;

[CssSelector(".dropdown-item")]
public class BootstrapDropdownItemCssVariables
{
	[CssVariable("bs-dropdown-link-color")]
	public string? LinkColor { get; set; }

	[CssVariable("bs-dropdown-link-hover-color")]
	public string? LinkHoverColor { get; set; }

	[CssVariable("bs-dropdown-link-hover-bg")]
	public string? LinkHoverBackground { get; set; }

	[CssVariable("bs-dropdown-link-active-color")]
	public string? LinkActiveColor { get; set; }

	[CssVariable("bs-dropdown-link-active-bg")]
	public string? LinkActiveBackground { get; set; }

	[CssVariable("bs-dropdown-link-disabled-color")]
	public string? LinkDisabledColor { get; set; }
}


