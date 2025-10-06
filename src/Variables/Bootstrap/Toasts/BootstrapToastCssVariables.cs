namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's toast CSS variables
/// </summary>
[CssSelector(".toast")]
public class BootstrapToastCssVariables
{
	/// <summary>
	/// Toast z-index. Default: 1090
	/// </summary>
	[CssVariable("bs-toast-zindex")]
	public string? Zindex { get; set; }

	/// <summary>
	/// Toast padding X. Default: 0.75rem
	/// </summary>
	[CssVariable("bs-toast-padding-x")]
	public string? PaddingX { get; set; }

	/// <summary>
	/// Toast padding Y. Default: 0.5rem
	/// </summary>
	[CssVariable("bs-toast-padding-y")]
	public string? PaddingY { get; set; }

	/// <summary>
	/// Toast spacing. Default: 1.5rem
	/// </summary>
	[CssVariable("bs-toast-spacing")]
	public string? Spacing { get; set; }

	/// <summary>
	/// Toast max width. Default: 350px
	/// </summary>
	[CssVariable("bs-toast-max-width")]
	public string? MaxWidth { get; set; }

	/// <summary>
	/// Toast font size. Default: 0.875rem
	/// </summary>
	[CssVariable("bs-toast-font-size")]
	public string? FontSize { get; set; }

	/// <summary>
	/// Toast color. Default: inherit
	/// </summary>
	[CssVariable("bs-toast-color")]
	public string? Color { get; set; }

	/// <summary>
	/// Toast background. Default: rgba(var(--bs-body-bg-rgb), 0.85)
	/// </summary>
	[CssVariable("bs-toast-bg")]
	public string? Background { get; set; }

	/// <summary>
	/// Toast border width. Default: var(--bs-border-width)
	/// </summary>
	[CssVariable("bs-toast-border-width")]
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Toast border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	[CssVariable("bs-toast-border-color")]
	public string? BorderColor { get; set; }

	/// <summary>
	/// Toast border radius. Default: var(--bs-border-radius)
	/// </summary>
	[CssVariable("bs-toast-border-radius")]
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Toast box shadow. Default: var(--bs-box-shadow)
	/// </summary>
	[CssVariable("bs-toast-box-shadow")]
	public string? BoxShadow { get; set; }

	/// <summary>
	/// Toast header color. Default: var(--bs-secondary-color)
	/// </summary>
	[CssVariable("bs-toast-header-color")]
	public string? HeaderColor { get; set; }

	/// <summary>
	/// Toast header background. Default: rgba(var(--bs-body-bg-rgb), 0.85)
	/// </summary>
	[CssVariable("bs-toast-header-bg")]
	public string? HeaderBg { get; set; }

	/// <summary>
	/// Toast header border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	[CssVariable("bs-toast-header-border-color")]
	public string? HeaderBorderColor { get; set; }
}


