namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's modal CSS variables
/// </summary>
[CssSelector(".modal")]
public class BootstrapModalCssVariables
{
	/// <summary>
	/// Modal z-index. Default: 1055
	/// </summary>
	[CssVariable("bs-modal-zindex")]
	public string? Zindex { get; set; }

	/// <summary>
	/// Modal width. Default: 500px
	/// </summary>
	[CssVariable("bs-modal-width")]
	public string? Width { get; set; }

	/// <summary>
	/// Modal padding. Default: 1rem
	/// </summary>
	[CssVariable("bs-modal-padding")]
	public string? Padding { get; set; }

	/// <summary>
	/// Modal margin. Default: 0.5rem
	/// </summary>
	[CssVariable("bs-modal-margin")]
	public string? Margin { get; set; }

	/// <summary>
	/// Modal color. Default: var(--bs-body-color)
	/// </summary>
	[CssVariable("bs-modal-color")]
	public string? Color { get; set; }

	/// <summary>
	/// Modal background. Default: var(--bs-body-bg)
	/// </summary>
	[CssVariable("bs-modal-bg")]
	public string? Background { get; set; }

	/// <summary>
	/// Modal border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	[CssVariable("bs-modal-border-color")]
	public string? BorderColor { get; set; }

	/// <summary>
	/// Modal border width. Default: var(--bs-border-width)
	/// </summary>
	[CssVariable("bs-modal-border-width")]
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Modal border radius. Default: var(--bs-border-radius-lg)
	/// </summary>
	[CssVariable("bs-modal-border-radius")]
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Modal box shadow. Default: var(--bs-box-shadow-sm)
	/// </summary>
	[CssVariable("bs-modal-box-shadow")]
	public string? BoxShadow { get; set; }

	/// <summary>
	/// Modal inner border radius. Default: calc(var(--bs-border-radius-lg) - (var(--bs-border-width)))
	/// </summary>
	[CssVariable("bs-modal-inner-border-radius")]
	public string? InnerBorderRadius { get; set; }

	/// <summary>
	/// Modal header padding X. Default: 1rem
	/// </summary>
	[CssVariable("bs-modal-header-padding-x")]
	public string? HeaderPaddingX { get; set; }

	/// <summary>
	/// Modal header padding Y. Default: 1rem
	/// </summary>
	[CssVariable("bs-modal-header-padding-y")]
	public string? HeaderPaddingY { get; set; }

	/// <summary>
	/// Modal header padding. Default: 1rem 1rem
	/// </summary>
	[CssVariable("bs-modal-header-padding")]
	public string? HeaderPadding { get; set; }

	/// <summary>
	/// Modal header border color. Default: var(--bs-border-color)
	/// </summary>
	[CssVariable("bs-modal-header-border-color")]
	public string? HeaderBorderColor { get; set; }

	/// <summary>
	/// Modal header border width. Default: var(--bs-border-width)
	/// </summary>
	[CssVariable("bs-modal-header-border-width")]
	public string? HeaderBorderWidth { get; set; }

	/// <summary>
	/// Modal title line height. Default: 1.5
	/// </summary>
	[CssVariable("bs-modal-title-line-height")]
	public string? TitleLineHeight { get; set; }

	/// <summary>
	/// Modal footer gap. Default: 0.5rem
	/// </summary>
	[CssVariable("bs-modal-footer-gap")]
	public string? FooterGap { get; set; }

	/// <summary>
	/// Modal footer background. Default: transparent
	/// </summary>
	[CssVariable("bs-modal-footer-bg")]
	public string? FooterBg { get; set; }

	/// <summary>
	/// Modal footer border color. Default: var(--bs-border-color)
	/// </summary>
	[CssVariable("bs-modal-footer-border-color")]
	public string? FooterBorderColor { get; set; }

	/// <summary>
	/// Modal footer border width. Default: var(--bs-border-width)
	/// </summary>
	[CssVariable("bs-modal-footer-border-width")]
	public string? FooterBorderWidth { get; set; }
}

