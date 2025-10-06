namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's table CSS variables
/// </summary>
[CssSelector(".table")]
public class BootstrapTableCssVariables
{
	/// <summary>
	/// Table color type. Default: initial
	/// </summary>
	[CssVariable("bs-table-color-type")]
	public string? ColorType { get; set; }

	/// <summary>
	/// Table background type. Default: initial
	/// </summary>
	[CssVariable("bs-table-bg-type")]
	public string? BgType { get; set; }

	/// <summary>
	/// Table color state. Default: initial
	/// </summary>
	[CssVariable("bs-table-color-state")]
	public string? ColorState { get; set; }

	/// <summary>
	/// Table background state. Default: initial
	/// </summary>
	[CssVariable("bs-table-bg-state")]
	public string? BgState { get; set; }

	/// <summary>
	/// Table color. Default: var(--bs-emphasis-color)
	/// </summary>
	[CssVariable("bs-table-color")]
	public string? Color { get; set; }

	/// <summary>
	/// Table background. Default: var(--bs-body-bg)
	/// </summary>
	[CssVariable("bs-table-bg")]
	public string? Background { get; set; }

	/// <summary>
	/// Table border color. Default: var(--bs-border-color)
	/// </summary>
	[CssVariable("bs-table-border-color")]
	public string? BorderColor { get; set; }

	/// <summary>
	/// Table accent background. Default: transparent
	/// </summary>
	[CssVariable("bs-table-accent-bg")]
	public string? AccentBg { get; set; }

	/// <summary>
	/// Table striped color. Default: var(--bs-emphasis-color)
	/// </summary>
	[CssVariable("bs-table-striped-color")]
	public string? StripedColor { get; set; }

	/// <summary>
	/// Table striped background. Default: rgba(var(--bs-emphasis-color-rgb), 0.05)
	/// </summary>
	[CssVariable("bs-table-striped-bg")]
	public string? StripedBg { get; set; }

	/// <summary>
	/// Table active color. Default: var(--bs-emphasis-color)
	/// </summary>
	[CssVariable("bs-table-active-color")]
	public string? ActiveColor { get; set; }

	/// <summary>
	/// Table active background. Default: rgba(var(--bs-emphasis-color-rgb), 0.1)
	/// </summary>
	[CssVariable("bs-table-active-bg")]
	public string? ActiveBg { get; set; }

	/// <summary>
	/// Table hover color. Default: var(--bs-emphasis-color)
	/// </summary>
	[CssVariable("bs-table-hover-color")]
	public string? HoverColor { get; set; }

	/// <summary>
	/// Table hover background. Default: rgba(var(--bs-emphasis-color-rgb), 0.075)
	/// </summary>
	[CssVariable("bs-table-hover-bg")]
	public string? HoverBg { get; set; }
}

