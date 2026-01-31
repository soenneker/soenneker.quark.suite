using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's border CSS variables
/// </summary>
public sealed class BootstrapBordersCssVariables : IBootstrapCssVariableGroup
{
    // Border Width
    /// <summary>
    /// Border width. Default: 1px
    /// </summary>
    public string? BorderWidth { get; set; }

    // Border Style
    /// <summary>
    /// Border style. Default: solid
    /// </summary>
    public string? BorderStyle { get; set; }

    // Border Color
    /// <summary>
    /// Border color. Default: #dee2e6
    /// </summary>
    public string? BorderColor { get; set; }

    /// <summary>
    /// Border color translucent. Default: rgba(0, 0, 0, 0.175)
    /// </summary>
    public string? BorderColorTranslucent { get; set; }

    // Border Radius
    /// <summary>
    /// Border radius. Default: 0.375rem
    /// </summary>
    public string? BorderRadius { get; set; }

    /// <summary>
    /// Border radius small. Default: 0.25rem
    /// </summary>
    public string? BorderRadiusSm { get; set; }

    /// <summary>
    /// Border radius large. Default: 0.5rem
    /// </summary>
    public string? BorderRadiusLg { get; set; }

    /// <summary>
    /// Border radius extra large. Default: 1rem
    /// </summary>
    public string? BorderRadiusXl { get; set; }

    /// <summary>
    /// Border radius XXL. Default: 2rem
    /// </summary>
    public string? BorderRadiusXxl { get; set; }

    /// <summary>
    /// Border radius 2XL. Default: var(--bs-border-radius-xxl)
    /// </summary>
    public string? BorderRadius2Xl { get; set; }

    /// <summary>
    /// Border radius pill. Default: 50rem
    /// </summary>
    public string? BorderRadiusPill { get; set; }

    // Border Width Variations
    /// <summary>
    /// Border width 0. Default: 0
    /// </summary>
    public string? BorderWidth0 { get; set; }

    /// <summary>
    /// Border width 1. Default: 1px
    /// </summary>
    public string? BorderWidth1 { get; set; }

    /// <summary>
    /// Border width 2. Default: 2px
    /// </summary>
    public string? BorderWidth2 { get; set; }

    /// <summary>
    /// Border width 3. Default: 3px
    /// </summary>
    public string? BorderWidth3 { get; set; }

    /// <summary>
    /// Border width 4. Default: 4px
    /// </summary>
    public string? BorderWidth4 { get; set; }

    /// <summary>
    /// Border width 5. Default: 5px
    /// </summary>
    public string? BorderWidth5 { get; set; }

    // Border Opacity
    /// <summary>
    /// Border opacity. Default: 0.25
    /// </summary>
    public string? BorderOpacity { get; set; }

    // Border Color Variations
    /// <summary>
    /// Border color white. Default: #fff
    /// </summary>
    public string? BorderColorWhite { get; set; }

    /// <summary>
    /// Border color black. Default: #000
    /// </summary>
    public string? BorderColorBlack { get; set; }

    /// <summary>
    /// Border color primary. Default: var(--bs-primary)
    /// </summary>
    public string? BorderColorPrimary { get; set; }

    /// <summary>
    /// Border color secondary. Default: var(--bs-secondary)
    /// </summary>
    public string? BorderColorSecondary { get; set; }

    /// <summary>
    /// Border color success. Default: var(--bs-success)
    /// </summary>
    public string? BorderColorSuccess { get; set; }

    /// <summary>
    /// Border color danger. Default: var(--bs-danger)
    /// </summary>
    public string? BorderColorDanger { get; set; }

    /// <summary>
    /// Border color warning. Default: var(--bs-warning)
    /// </summary>
    public string? BorderColorWarning { get; set; }

    /// <summary>
    /// Border color info. Default: var(--bs-info)
    /// </summary>
    public string? BorderColorInfo { get; set; }

    /// <summary>
    /// Border color light. Default: var(--bs-light)
    /// </summary>
    public string? BorderColorLight { get; set; }

    /// <summary>
    /// Border color dark. Default: var(--bs-dark)
    /// </summary>
    public string? BorderColorDark { get; set; }

	/// <summary>
	/// Gets the CSS selector for the borders component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the borders component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (BorderWidth.HasContent())
            yield return ("--bs-border-width", BorderWidth);
        if (BorderStyle.HasContent())
            yield return ("--bs-border-style", BorderStyle);
        if (BorderColor.HasContent())
            yield return ("--bs-border-color", BorderColor);
        if (BorderColorTranslucent.HasContent())
            yield return ("--bs-border-color-translucent", BorderColorTranslucent);
        if (BorderRadius.HasContent())
            yield return ("--bs-border-radius", BorderRadius);
        if (BorderRadiusSm.HasContent())
            yield return ("--bs-border-radius-sm", BorderRadiusSm);
        if (BorderRadiusLg.HasContent())
            yield return ("--bs-border-radius-lg", BorderRadiusLg);
        if (BorderRadiusXl.HasContent())
            yield return ("--bs-border-radius-xl", BorderRadiusXl);
        if (BorderRadiusXxl.HasContent())
            yield return ("--bs-border-radius-xxl", BorderRadiusXxl);
        if (BorderRadius2Xl.HasContent())
            yield return ("--bs-border-radius-2xl", BorderRadius2Xl);
        if (BorderRadiusPill.HasContent())
            yield return ("--bs-border-radius-pill", BorderRadiusPill);
        if (BorderWidth0.HasContent())
            yield return ("--bs-border-width-0", BorderWidth0);
        if (BorderWidth1.HasContent())
            yield return ("--bs-border-width-1", BorderWidth1);
        if (BorderWidth2.HasContent())
            yield return ("--bs-border-width-2", BorderWidth2);
        if (BorderWidth3.HasContent())
            yield return ("--bs-border-width-3", BorderWidth3);
        if (BorderWidth4.HasContent())
            yield return ("--bs-border-width-4", BorderWidth4);
        if (BorderWidth5.HasContent())
            yield return ("--bs-border-width-5", BorderWidth5);
        if (BorderOpacity.HasContent())
            yield return ("--bs-border-opacity", BorderOpacity);
        if (BorderColorWhite.HasContent())
            yield return ("--bs-border-color-white", BorderColorWhite);
        if (BorderColorBlack.HasContent())
            yield return ("--bs-border-color-black", BorderColorBlack);
        if (BorderColorPrimary.HasContent())
            yield return ("--bs-border-color-primary", BorderColorPrimary);
        if (BorderColorSecondary.HasContent())
            yield return ("--bs-border-color-secondary", BorderColorSecondary);
        if (BorderColorSuccess.HasContent())
            yield return ("--bs-border-color-success", BorderColorSuccess);
        if (BorderColorDanger.HasContent())
            yield return ("--bs-border-color-danger", BorderColorDanger);
        if (BorderColorWarning.HasContent())
            yield return ("--bs-border-color-warning", BorderColorWarning);
        if (BorderColorInfo.HasContent())
            yield return ("--bs-border-color-info", BorderColorInfo);
        if (BorderColorLight.HasContent())
            yield return ("--bs-border-color-light", BorderColorLight);
        if (BorderColorDark.HasContent())
            yield return ("--bs-border-color-dark", BorderColorDark);
    }
}
