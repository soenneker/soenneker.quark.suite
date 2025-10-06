namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's tooltip CSS variables
/// </summary>
[CssSelector(".tooltip")]
public class BootstrapTooltipCssVariables
{
	/// <summary>
	/// Tooltip z-index. Default: 1080
	/// </summary>
	[CssVariable("bs-tooltip-zindex")]
	public string? Zindex { get; set; }

	/// <summary>
	/// Tooltip max width. Default: 200px
	/// </summary>
	[CssVariable("bs-tooltip-max-width")]
	public string? MaxWidth { get; set; }

	/// <summary>
	/// Tooltip padding X. Default: 0.5rem
	/// </summary>
	[CssVariable("bs-tooltip-padding-x")]
	public string? PaddingX { get; set; }

	/// <summary>
	/// Tooltip padding Y. Default: 0.25rem
	/// </summary>
	[CssVariable("bs-tooltip-padding-y")]
	public string? PaddingY { get; set; }

	/// <summary>
	/// Tooltip margin. Default: 0
	/// </summary>
	[CssVariable("bs-tooltip-margin")]
	public string? Margin { get; set; }

	/// <summary>
	/// Tooltip font size. Default: 0.875rem
	/// </summary>
	[CssVariable("bs-tooltip-font-size")]
	public string? FontSize { get; set; }

	/// <summary>
	/// Tooltip color. Default: var(--bs-body-bg)
	/// </summary>
	[CssVariable("bs-tooltip-color")]
	public string? Color { get; set; }

	/// <summary>
	/// Tooltip background. Default: var(--bs-emphasis-color)
	/// </summary>
	[CssVariable("bs-tooltip-bg")]
	public string? Background { get; set; }

	/// <summary>
	/// Tooltip border radius. Default: var(--bs-border-radius)
	/// </summary>
	[CssVariable("bs-tooltip-border-radius")]
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Tooltip opacity. Default: 0.9
	/// </summary>
	[CssVariable("bs-tooltip-opacity")]
	public string? Opacity { get; set; }

	/// <summary>
	/// Tooltip arrow width. Default: 0.8rem
	/// </summary>
	[CssVariable("bs-tooltip-arrow-width")]
	public string? ArrowWidth { get; set; }

	/// <summary>
	/// Tooltip arrow height. Default: 0.4rem
	/// </summary>
	[CssVariable("bs-tooltip-arrow-height")]
	public string? ArrowHeight { get; set; }
}


