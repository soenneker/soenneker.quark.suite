using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's close button CSS variables
/// </summary>
public sealed class BootstrapCloseButtonCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Close button color. Default: #000
    /// </summary>
    public string? CloseButtonColor { get; set; }

    /// <summary>
    /// Close button background. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23000'%3e%3cpath d='M.293.293a1 1 0 0 1 1.414 0L8 6.586 14.293.293a1 1 0 1 1 1.414 1.414L9.414 8l6.293 6.293a1 1 0 0 1-1.414 1.414L8 9.414l-6.293 6.293a1 1 0 0 1-1.414-1.414L6.586 8 .293 1.707a1 1 0 0 1 0-1.414'/%3e%3c/svg%3e")
    /// </summary>
    public string? CloseButtonBg { get; set; }

    /// <summary>
    /// Close button opacity. Default: 0.5
    /// </summary>
    public string? CloseButtonOpacity { get; set; }

    /// <summary>
    /// Close button hover opacity. Default: 0.75
    /// </summary>
    public string? CloseButtonHoverOpacity { get; set; }

    /// <summary>
    /// Close button focus shadow. Default: 0 0 0 0.25rem rgba(13, 110, 253, 0.25)
    /// </summary>
    public string? CloseButtonFocusShadow { get; set; }

    /// <summary>
    /// Close button focus opacity. Default: 1
    /// </summary>
    public string? CloseButtonFocusOpacity { get; set; }

    /// <summary>
    /// Close button disabled opacity. Default: 0.25
    /// </summary>
    public string? CloseButtonDisabledOpacity { get; set; }

    /// <summary>
    /// Close button white filter. Default: invert(1) grayscale(100%) brightness(200%)
    /// </summary>
	public string? CloseButtonWhiteFilter { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the close button border.
	/// </summary>
	public string? CloseButtonBorder { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the close button border radius.
	/// </summary>
	public string? CloseButtonBorderRadius { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the close button focus box shadow.
	/// </summary>
	public string? CloseButtonFocusBoxShadow { get; set; }

	/// <summary>
	/// Gets the CSS selector for the close button component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the close button component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (CloseButtonColor.HasContent())
            yield return ("--bs-btn-close-color", CloseButtonColor);
        if (CloseButtonBg.HasContent())
            yield return ("--bs-btn-close-bg", CloseButtonBg);
        if (CloseButtonBorder.HasContent())
            yield return ("--bs-btn-close-border", CloseButtonBorder);
        if (CloseButtonBorderRadius.HasContent())
            yield return ("--bs-btn-close-border-radius", CloseButtonBorderRadius);
        if (CloseButtonOpacity.HasContent())
            yield return ("--bs-btn-close-opacity", CloseButtonOpacity);
        if (CloseButtonHoverOpacity.HasContent())
            yield return ("--bs-btn-close-hover-opacity", CloseButtonHoverOpacity);
        if (CloseButtonFocusOpacity.HasContent())
            yield return ("--bs-btn-close-focus-opacity", CloseButtonFocusOpacity);
        if (CloseButtonFocusBoxShadow.HasContent())
            yield return ("--bs-btn-close-focus-box-shadow", CloseButtonFocusBoxShadow);
        if (CloseButtonDisabledOpacity.HasContent())
            yield return ("--bs-btn-close-disabled-opacity", CloseButtonDisabledOpacity);
        if (CloseButtonWhiteFilter.HasContent())
            yield return ("--bs-btn-close-white-filter", CloseButtonWhiteFilter);
    }
}
