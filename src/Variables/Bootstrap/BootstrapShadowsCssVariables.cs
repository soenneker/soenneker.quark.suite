using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's shadow CSS variables
/// </summary>
public sealed class BootstrapShadowsCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Box shadow. Default: 0 0.5rem 1rem rgba(0, 0, 0, 0.15)
    /// </summary>
    public string? BoxShadow { get; set; }

    /// <summary>
    /// Box shadow small. Default: 0 0.125rem 0.25rem rgba(0, 0, 0, 0.075)
    /// </summary>
    public string? BoxShadowSm { get; set; }

    /// <summary>
    /// Box shadow large. Default: 0 1rem 3rem rgba(0, 0, 0, 0.175)
    /// </summary>
    public string? BoxShadowLg { get; set; }

    /// <summary>
    /// Box shadow inset. Default: inset 0 1px 2px rgba(0, 0, 0, 0.075)
    /// </summary>
    public string? BoxShadowInset { get; set; }

    // Focus Ring
    /// <summary>
    /// Focus ring width. Default: 0.25rem
    /// </summary>
    public string? FocusRingWidth { get; set; }

    /// <summary>
    /// Focus ring opacity. Default: 0.25
    /// </summary>
    public string? FocusRingOpacity { get; set; }

    /// <summary>
    /// Focus ring color. Default: rgba(13, 110, 253, 0.25)
    /// </summary>
    public string? FocusRingColor { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (BoxShadow.HasContent())
            yield return ("--bs-box-shadow", BoxShadow);
        if (BoxShadowSm.HasContent())
            yield return ("--bs-box-shadow-sm", BoxShadowSm);
        if (BoxShadowLg.HasContent())
            yield return ("--bs-box-shadow-lg", BoxShadowLg);
        if (BoxShadowInset.HasContent())
            yield return ("--bs-box-shadow-inset", BoxShadowInset);
        if (FocusRingWidth.HasContent())
            yield return ("--bs-focus-ring-width", FocusRingWidth);
        if (FocusRingOpacity.HasContent())
            yield return ("--bs-focus-ring-opacity", FocusRingOpacity);
        if (FocusRingColor.HasContent())
            yield return ("--bs-focus-ring-color", FocusRingColor);
    }
}
