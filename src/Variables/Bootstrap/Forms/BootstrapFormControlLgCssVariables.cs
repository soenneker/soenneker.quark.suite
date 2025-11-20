using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .form-control-lg
/// </summary>
public sealed class BootstrapFormControlLgCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Form control large min height. Default: calc(1.5em + 1rem + calc(var(--bs-border-width) * 2))
	/// </summary>
	public string? MinHeight { get; set; }

	/// <summary>
	/// Form control large padding. Default: 0.5rem 1rem
	/// </summary>
	public string? Padding { get; set; }

	/// <summary>
	/// Form control large font size. Default: 1.25rem
	/// </summary>
	public string? FontSize { get; set; }

	/// <summary>
	/// Form control large border radius. Default: var(--bs-border-radius-lg)
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Form control large file upload button padding. Default: 0.5rem 1rem
	/// </summary>
	public string? FileUploadPadding { get; set; }

	/// <summary>
	/// Form control large file upload button margin. Default: -0.5rem -1rem
	/// </summary>
	public string? FileUploadMargin { get; set; }

	/// <summary>
	/// Form control large file upload button margin end. Default: 1rem
	/// </summary>
	public string? FileUploadMarginEnd { get; set; }

	/// <summary>
	/// Gets the CSS selector for the large form control component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".form-control-lg";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the large form control component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (MinHeight.HasContent())
            yield return ("--bs-form-control-lg-min-height", MinHeight);
        if (Padding.HasContent())
            yield return ("--bs-form-control-lg-padding", Padding);
        if (FontSize.HasContent())
            yield return ("--bs-form-control-lg-font-size", FontSize);
        if (BorderRadius.HasContent())
            yield return ("--bs-form-control-lg-border-radius", BorderRadius);
        if (FileUploadPadding.HasContent())
            yield return ("--bs-form-control-lg-file-upload-padding", FileUploadPadding);
        if (FileUploadMargin.HasContent())
            yield return ("--bs-form-control-lg-file-upload-margin", FileUploadMargin);
        if (FileUploadMarginEnd.HasContent())
            yield return ("--bs-form-control-lg-file-upload-margin-end", FileUploadMarginEnd);
    }
}
