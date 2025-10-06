namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-sm
/// </summary>
[CssSelector(".form-control-sm")]
public class BootstrapFormControlSmCssVariables
{
	/// <summary>
	/// Form control small min height. Default: calc(1.5em + 0.5rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	[CssVariable("bs-form-control-sm-min-height")]
	public string? MinHeight { get; set; }

	/// <summary>
	/// Form control small padding. Default: 0.25rem 0.5rem
	/// </summary>
	[CssVariable("bs-form-control-sm-padding")]
	public string? Padding { get; set; }

	/// <summary>
	/// Form control small font size. Default: 0.875rem
	/// </summary>
	[CssVariable("bs-form-control-sm-font-size")]
	public string? FontSize { get; set; }

	/// <summary>
	/// Form control small border radius. Default: var(--bs-border-radius-sm)
	/// </summary>
	[CssVariable("bs-form-control-sm-border-radius")]
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Form control small file upload button padding. Default: 0.25rem 0.5rem
	/// </summary>
	[CssVariable("bs-form-control-sm-file-upload-padding")]
	public string? FileUploadPadding { get; set; }

	/// <summary>
	/// Form control small file upload button margin. Default: -0.25rem -0.5rem
	/// </summary>
	[CssVariable("bs-form-control-sm-file-upload-margin")]
	public string? FileUploadMargin { get; set; }

	/// <summary>
	/// Form control small file upload button margin end. Default: 0.5rem
	/// </summary>
	[CssVariable("bs-form-control-sm-file-upload-margin-end")]
	public string? FileUploadMarginEnd { get; set; }
}
