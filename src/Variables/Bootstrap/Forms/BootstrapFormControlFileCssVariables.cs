namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control[type=file] and file upload button styling
/// </summary>
[CssSelector(".form-control[type=file]")]
public class BootstrapFormControlFileCssVariables
{
	/// <summary>
	/// Form control file upload button padding. Default: 0.375rem 0.75rem
	/// </summary>
	[CssVariable("bs-form-control-file-upload-padding")]
	public string? FileUploadPadding { get; set; }

	/// <summary>
	/// Form control file upload button margin. Default: -0.375rem -0.75rem
	/// </summary>
	[CssVariable("bs-form-control-file-upload-margin")]
	public string? FileUploadMargin { get; set; }

	/// <summary>
	/// Form control file upload button margin end. Default: 0.75rem
	/// </summary>
	[CssVariable("bs-form-control-file-upload-margin-end")]
	public string? FileUploadMarginEnd { get; set; }

	/// <summary>
	/// Form control file upload button color. Default: var(--bs-body-color)
	/// </summary>
	[CssVariable("bs-form-control-file-upload-color")]
	public string? FileUploadColor { get; set; }

	/// <summary>
	/// Form control file upload button background color. Default: var(--bs-tertiary-bg)
	/// </summary>
	[CssVariable("bs-form-control-file-upload-bg")]
	public string? FileUploadBackground { get; set; }

	/// <summary>
	/// Form control file upload button border color. Default: inherit
	/// </summary>
	[CssVariable("bs-form-control-file-upload-border-color")]
	public string? FileUploadBorderColor { get; set; }

	/// <summary>
	/// Form control file upload button border style. Default: solid
	/// </summary>
	[CssVariable("bs-form-control-file-upload-border-style")]
	public string? FileUploadBorderStyle { get; set; }

	/// <summary>
	/// Form control file upload button border width. Default: 0
	/// </summary>
	[CssVariable("bs-form-control-file-upload-border-width")]
	public string? FileUploadBorderWidth { get; set; }

	/// <summary>
	/// Form control file upload button border inline end width. Default: var(--bs-border-width)
	/// </summary>
	[CssVariable("bs-form-control-file-upload-border-inline-end-width")]
	public string? FileUploadBorderInlineEndWidth { get; set; }

	/// <summary>
	/// Form control file upload button border radius. Default: 0
	/// </summary>
	[CssVariable("bs-form-control-file-upload-border-radius")]
	public string? FileUploadBorderRadius { get; set; }

	/// <summary>
	/// Form control file upload button transition. Default: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out
	/// </summary>
	[CssVariable("bs-form-control-file-upload-transition")]
	public string? FileUploadTransition { get; set; }

	/// <summary>
	/// Form control file upload button hover background color. Default: var(--bs-secondary-bg)
	/// </summary>
	[CssVariable("bs-form-control-file-upload-hover-bg")]
	public string? FileUploadHoverBackground { get; set; }
}
