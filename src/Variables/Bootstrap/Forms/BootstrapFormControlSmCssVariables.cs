using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-sm
/// </summary>
public sealed class BootstrapFormControlSmCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Form control small min height. Default: calc(1.5em + 0.5rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? MinHeight { get; set; }

	/// <summary>
	/// Form control small padding. Default: 0.25rem 0.5rem
	/// </summary>
	public string? Padding { get; set; }

	/// <summary>
	/// Form control small font size. Default: 0.875rem
	/// </summary>
	public string? FontSize { get; set; }

	/// <summary>
	/// Form control small border radius. Default: var(--bs-border-radius-sm)
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Form control small file upload button padding. Default: 0.25rem 0.5rem
	/// </summary>
	public string? FileUploadPadding { get; set; }

	/// <summary>
	/// Form control small file upload button margin. Default: -0.25rem -0.5rem
	/// </summary>
	public string? FileUploadMargin { get; set; }

	/// <summary>
	/// Form control small file upload button margin end. Default: 0.5rem
	/// </summary>
	public string? FileUploadMarginEnd { get; set; }

    public string GetSelector()
    {
        return ".form-control-sm";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (MinHeight.HasContent())
            yield return ("--bs-form-control-sm-min-height", MinHeight);
        if (Padding.HasContent())
            yield return ("--bs-form-control-sm-padding", Padding);
        if (FontSize.HasContent())
            yield return ("--bs-form-control-sm-font-size", FontSize);
        if (BorderRadius.HasContent())
            yield return ("--bs-form-control-sm-border-radius", BorderRadius);
        if (FileUploadPadding.HasContent())
            yield return ("--bs-form-control-sm-file-upload-padding", FileUploadPadding);
        if (FileUploadMargin.HasContent())
            yield return ("--bs-form-control-sm-file-upload-margin", FileUploadMargin);
        if (FileUploadMarginEnd.HasContent())
            yield return ("--bs-form-control-sm-file-upload-margin-end", FileUploadMarginEnd);
    }
}
