using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for .form-control and similar text inputs.
/// </summary>
public abstract class BootstrapBaseFormControlCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the form control border width.
	/// </summary>
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form control border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form control border radius.
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form control focus ring color.
	/// </summary>
	public string? FocusRingColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form control background.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form control text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the form control disabled background.
	/// </summary>
	public string? DisabledBackground { get; set; }

	/// <summary>
	/// Form control background color. Default: var(--bs-body-bg)
	/// </summary>
	public string? BackgroundColor { get; set; }

	/// <summary>
	/// Form control padding. Default: 0.375rem 0.75rem
	/// </summary>
	public string? Padding { get; set; }

	/// <summary>
	/// Form control font size. Default: 1rem
	/// </summary>
	public string? FontSize { get; set; }

	/// <summary>
	/// Form control font weight. Default: 400
	/// </summary>
	public string? FontWeight { get; set; }

	/// <summary>
	/// Form control line height. Default: 1.5
	/// </summary>
	public string? LineHeight { get; set; }

	/// <summary>
	/// Form control transition. Default: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out
	/// </summary>
	public string? Transition { get; set; }

	/// <summary>
	/// Form control focus border color. Default: #86b7fe
	/// </summary>
	public string? FocusBorderColor { get; set; }

	/// <summary>
	/// Form control focus box shadow. Default: 0 0 0 0.25rem rgba(13, 110, 253, 0.25)
	/// </summary>
	public string? FocusBoxShadow { get; set; }

	/// <summary>
	/// Form control placeholder color. Default: var(--bs-secondary-color)
	/// </summary>
	public string? PlaceholderColor { get; set; }

	/// <summary>
	/// Gets the CSS selector for the form control component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public virtual string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the form control component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public virtual IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (BorderWidth.HasContent())
            yield return ("--bs-border-width", BorderWidth);
        if (BorderColor.HasContent())
            yield return ("--bs-border-color", BorderColor);
        if (BorderRadius.HasContent())
            yield return ("--bs-border-radius", BorderRadius);
        if (FocusRingColor.HasContent())
            yield return ("--bs-focus-ring-color", FocusRingColor);
        if (Background.HasContent())
            yield return ("--bs-form-control-bg", Background);
        if (Color.HasContent())
            yield return ("--bs-form-control-color", Color);
        if (DisabledBackground.HasContent())
            yield return ("--bs-form-control-disabled-bg", DisabledBackground);
        if (BackgroundColor.HasContent())
            yield return ("--bs-body-bg", BackgroundColor);
        if (Padding.HasContent())
            yield return ("--bs-form-control-padding", Padding);
        if (FontSize.HasContent())
            yield return ("--bs-form-control-font-size", FontSize);
        if (FontWeight.HasContent())
            yield return ("--bs-form-control-font-weight", FontWeight);
        if (LineHeight.HasContent())
            yield return ("--bs-form-control-line-height", LineHeight);
        if (Transition.HasContent())
            yield return ("--bs-form-control-transition", Transition);
        if (FocusBorderColor.HasContent())
            yield return ("--bs-form-control-focus-border-color", FocusBorderColor);
        if (FocusBoxShadow.HasContent())
            yield return ("--bs-form-control-focus-box-shadow", FocusBoxShadow);
        if (PlaceholderColor.HasContent())
            yield return ("--bs-form-control-placeholder-color", PlaceholderColor);
    }
}


