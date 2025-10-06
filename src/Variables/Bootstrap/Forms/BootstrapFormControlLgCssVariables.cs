namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-lg
/// </summary>
[CssSelector(".form-control-lg")]
public class BootstrapFormControlLgCssVariables
{
	/// <summary>
	/// Form control large min height. Default: calc(1.5em + 1rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-form-control-lg-min-height")]
	public string? MinHeight { get; set; }

	/// <summary>
	/// Form control large padding. Default: 0.5rem 1rem
	/// </summary>
	[CssVariable("bs-form-control-lg-padding")]
	public string? Padding { get; set; }

	/// <summary>
	/// Form control large font size. Default: 1.25rem
	/// </summary>
	[CssVariable("bs-form-control-lg-font-size")]
	public string? FontSize { get; set; }

	/// <summary>
	/// Form control large border radius. Default: var(--bs-border-radius-lg)
	/// </summary>
	[CssVariable("bs-form-control-lg-border-radius")]
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Form control large file upload button padding. Default: 0.5rem 1rem
	/// </summary>
	[CssVariable("bs-form-control-lg-file-upload-padding")]
	public string? FileUploadPadding { get; set; }

	/// <summary>
	/// Form control large file upload button margin. Default: -0.5rem -1rem
	/// </summary>
	[CssVariable("bs-form-control-lg-file-upload-margin")]
	public string? FileUploadMargin { get; set; }

	/// <summary>
	/// Form control large file upload button margin end. Default: 1rem
	/// </summary>
	[CssVariable("bs-form-control-lg-file-upload-margin-end")]
	public string? FileUploadMarginEnd { get; set; }
}
