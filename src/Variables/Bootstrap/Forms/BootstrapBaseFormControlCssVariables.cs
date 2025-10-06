namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for .form-control and similar text inputs.
/// </summary>
[CssSelector]
public abstract class BootstrapBaseFormControlCssVariables
{
	[CssVariable("bs-border-width")]
	public string? BorderWidth { get; set; }

	[CssVariable("bs-border-color")]
	public string? BorderColor { get; set; }

	[CssVariable("bs-border-radius")]
	public string? BorderRadius { get; set; }

	[CssVariable("bs-focus-ring-color")]
	public string? FocusRingColor { get; set; }

	[CssVariable("bs-form-control-bg")]
	public string? Background { get; set; }

	[CssVariable("bs-form-control-color")]
	public string? Color { get; set; }

	[CssVariable("bs-form-control-disabled-bg")]
	public string? DisabledBackground { get; set; }

	/// <summary>
	/// Form control background color. Default: var(--bs-body-bg)
	/// </summary>
	[CssVariable("bs-body-bg")]
	public string? BackgroundColor { get; set; }

	/// <summary>
	/// Form control padding. Default: 0.375rem 0.75rem
	/// </summary>
	[CssVariable("bs-form-control-padding")]
	public string? Padding { get; set; }

	/// <summary>
	/// Form control font size. Default: 1rem
	/// </summary>
	[CssVariable("bs-form-control-font-size")]
	public string? FontSize { get; set; }

	/// <summary>
	/// Form control font weight. Default: 400
	/// </summary>
	[CssVariable("bs-form-control-font-weight")]
	public string? FontWeight { get; set; }

	/// <summary>
	/// Form control line height. Default: 1.5
	/// </summary>
	[CssVariable("bs-form-control-line-height")]
	public string? LineHeight { get; set; }

	/// <summary>
	/// Form control transition. Default: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out
	/// </summary>
	[CssVariable("bs-form-control-transition")]
	public string? Transition { get; set; }

	/// <summary>
	/// Form control focus border color. Default: #86b7fe
	/// </summary>
	[CssVariable("bs-form-control-focus-border-color")]
	public string? FocusBorderColor { get; set; }

	/// <summary>
	/// Form control focus box shadow. Default: 0 0 0 0.25rem rgba(13, 110, 253, 0.25)
	/// </summary>
	[CssVariable("bs-form-control-focus-box-shadow")]
	public string? FocusBoxShadow { get; set; }

	/// <summary>
	/// Form control placeholder color. Default: var(--bs-secondary-color)
	/// </summary>
	[CssVariable("bs-form-control-placeholder-color")]
	public string? PlaceholderColor { get; set; }
}


