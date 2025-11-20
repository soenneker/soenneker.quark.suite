using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control[type=file] and file upload button styling
/// </summary>
public class BootstrapFormControlFileCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Form control file upload button padding. Default: 0.375rem 0.75rem
	/// </summary>
	public string? FileUploadPadding { get; set; }

	/// <summary>
	/// Form control file upload button margin. Default: -0.375rem -0.75rem
	/// </summary>
	public string? FileUploadMargin { get; set; }

	/// <summary>
	/// Form control file upload button margin end. Default: 0.75rem
	/// </summary>
	public string? FileUploadMarginEnd { get; set; }

	/// <summary>
	/// Form control file upload button color. Default: var(--bs-body-color)
	/// </summary>
	public string? FileUploadColor { get; set; }

	/// <summary>
	/// Form control file upload button background color. Default: var(--bs-tertiary-bg)
	/// </summary>
	public string? FileUploadBackground { get; set; }

	/// <summary>
	/// Form control file upload button border color. Default: inherit
	/// </summary>
	public string? FileUploadBorderColor { get; set; }

	/// <summary>
	/// Form control file upload button border style. Default: solid
	/// </summary>
	public string? FileUploadBorderStyle { get; set; }

	/// <summary>
	/// Form control file upload button border width. Default: 0
	/// </summary>
	public string? FileUploadBorderWidth { get; set; }

	/// <summary>
	/// Form control file upload button border inline end width. Default: var(--bs-border-width)
	/// </summary>
	public string? FileUploadBorderInlineEndWidth { get; set; }

	/// <summary>
	/// Form control file upload button border radius. Default: 0
	/// </summary>
	public string? FileUploadBorderRadius { get; set; }

	/// <summary>
	/// Form control file upload button transition. Default: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out
	/// </summary>
	public string? FileUploadTransition { get; set; }

	/// <summary>
	/// Form control file upload button hover background color. Default: var(--bs-secondary-bg)
	/// </summary>
	public string? FileUploadHoverBackground { get; set; }

    public string GetSelector()
    {
        return ".form-control[type=file]";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (FileUploadPadding.HasContent())
            yield return ("--bs-form-control-file-upload-padding", FileUploadPadding);
        if (FileUploadMargin.HasContent())
            yield return ("--bs-form-control-file-upload-margin", FileUploadMargin);
        if (FileUploadMarginEnd.HasContent())
            yield return ("--bs-form-control-file-upload-margin-end", FileUploadMarginEnd);
        if (FileUploadColor.HasContent())
            yield return ("--bs-form-control-file-upload-color", FileUploadColor);
        if (FileUploadBackground.HasContent())
            yield return ("--bs-form-control-file-upload-bg", FileUploadBackground);
        if (FileUploadBorderColor.HasContent())
            yield return ("--bs-form-control-file-upload-border-color", FileUploadBorderColor);
        if (FileUploadBorderStyle.HasContent())
            yield return ("--bs-form-control-file-upload-border-style", FileUploadBorderStyle);
        if (FileUploadBorderWidth.HasContent())
            yield return ("--bs-form-control-file-upload-border-width", FileUploadBorderWidth);
        if (FileUploadBorderInlineEndWidth.HasContent())
            yield return ("--bs-form-control-file-upload-border-inline-end-width", FileUploadBorderInlineEndWidth);
        if (FileUploadBorderRadius.HasContent())
            yield return ("--bs-form-control-file-upload-border-radius", FileUploadBorderRadius);
        if (FileUploadTransition.HasContent())
            yield return ("--bs-form-control-file-upload-transition", FileUploadTransition);
        if (FileUploadHoverBackground.HasContent())
            yield return ("--bs-form-control-file-upload-hover-bg", FileUploadHoverBackground);
    }
}
