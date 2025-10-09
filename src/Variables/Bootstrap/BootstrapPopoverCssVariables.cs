namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's popover CSS variables
/// </summary>
[CssSelector(".popover")]
public class BootstrapPopoverCssVariables
{
	/// <summary>
	/// Popover z-index. Default: 1070
	/// </summary>
	[CssVariable("bs-popover-zindex")]
	public string? Zindex { get; set; }

	/// <summary>
	/// Popover max width. Default: 276px
	/// </summary>
	[CssVariable("bs-popover-max-width")]
	public string? MaxWidth { get; set; }

	/// <summary>
	/// Popover font size. Default: 0.875rem
	/// </summary>
	[CssVariable("bs-popover-font-size")]
	public string? FontSize { get; set; }

	/// <summary>
	/// Popover background. Default: var(--bs-body-bg)
	/// </summary>
	[CssVariable("bs-popover-bg")]
	public string? Background { get; set; }

	/// <summary>
	/// Popover border width. Default: var(--bs-border-width)
	/// </summary>
	[CssVariable("bs-popover-border-width")]
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Popover border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	[CssVariable("bs-popover-border-color")]
	public string? BorderColor { get; set; }

	/// <summary>
	/// Popover border radius. Default: var(--bs-border-radius-lg)
	/// </summary>
	[CssVariable("bs-popover-border-radius")]
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Popover inner border radius. Default: calc(var(--bs-border-radius-lg) - var(--bs-border-width))
	/// </summary>
	[CssVariable("bs-popover-inner-border-radius")]
	public string? InnerBorderRadius { get; set; }

	/// <summary>
	/// Popover box shadow. Default: var(--bs-box-shadow)
	/// </summary>
	[CssVariable("bs-popover-box-shadow")]
	public string? BoxShadow { get; set; }

	/// <summary>
	/// Popover header padding X. Default: 1rem
	/// </summary>
	[CssVariable("bs-popover-header-padding-x")]
	public string? HeaderPaddingX { get; set; }

	/// <summary>
	/// Popover header padding Y. Default: 0.5rem
	/// </summary>
	[CssVariable("bs-popover-header-padding-y")]
	public string? HeaderPaddingY { get; set; }

	/// <summary>
	/// Popover header font size. Default: 1rem
	/// </summary>
	[CssVariable("bs-popover-header-font-size")]
	public string? HeaderFontSize { get; set; }

	/// <summary>
	/// Popover header color. Default: inherit
	/// </summary>
	[CssVariable("bs-popover-header-color")]
	public string? HeaderColor { get; set; }

	/// <summary>
	/// Popover header background. Default: var(--bs-secondary-bg)
	/// </summary>
	[CssVariable("bs-popover-header-bg")]
	public string? HeaderBackground { get; set; }

	/// <summary>
	/// Popover body padding X. Default: 1rem
	/// </summary>
	[CssVariable("bs-popover-body-padding-x")]
	public string? BodyPaddingX { get; set; }

	/// <summary>
	/// Popover body padding Y. Default: 1rem
	/// </summary>
	[CssVariable("bs-popover-body-padding-y")]
	public string? BodyPaddingY { get; set; }

	/// <summary>
	/// Popover body color. Default: var(--bs-body-color)
	/// </summary>
	[CssVariable("bs-popover-body-color")]
	public string? BodyColor { get; set; }

	/// <summary>
	/// Popover arrow width. Default: 1rem
	/// </summary>
	[CssVariable("bs-popover-arrow-width")]
	public string? ArrowWidth { get; set; }

	/// <summary>
	/// Popover arrow height. Default: 0.5rem
	/// </summary>
	[CssVariable("bs-popover-arrow-height")]
	public string? ArrowHeight { get; set; }

	/// <summary>
	/// Popover arrow border. Default: var(--bs-popover-border-color)
	/// </summary>
	[CssVariable("bs-popover-arrow-border")]
	public string? ArrowBorder { get; set; }
}


