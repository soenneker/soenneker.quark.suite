using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's tooltip CSS variables
/// </summary>
public sealed class BootstrapTooltipCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Tooltip z-index. Default: 1080
    /// </summary>
    public string? Zindex { get; set; }

    /// <summary>
    /// Tooltip max width. Default: 200px
    /// </summary>
    public string? MaxWidth { get; set; }

    /// <summary>
    /// Tooltip padding X. Default: 0.5rem
    /// </summary>
    public string? PaddingX { get; set; }

    /// <summary>
    /// Tooltip padding Y. Default: 0.25rem
    /// </summary>
    public string? PaddingY { get; set; }

    /// <summary>
    /// Tooltip margin. Default: 0
    /// </summary>
    public string? Margin { get; set; }

    /// <summary>
    /// Tooltip font size. Default: 0.875rem
    /// </summary>
    public string? FontSize { get; set; }

    /// <summary>
    /// Tooltip color. Default: var(--bs-body-bg)
    /// </summary>
    public string? Color { get; set; }

    /// <summary>
    /// Tooltip background. Default: var(--bs-emphasis-color)
    /// </summary>
    public string? Background { get; set; }

    /// <summary>
    /// Tooltip border radius. Default: var(--bs-border-radius)
    /// </summary>
    public string? BorderRadius { get; set; }

    /// <summary>
    /// Tooltip opacity. Default: 0.9
    /// </summary>
    public string? Opacity { get; set; }

    /// <summary>
    /// Tooltip arrow width. Default: 0.8rem
    /// </summary>
    public string? ArrowWidth { get; set; }

    /// <summary>
    /// Tooltip arrow height. Default: 0.4rem
    /// </summary>
    public string? ArrowHeight { get; set; }

    public string GetSelector()
    {
        return ".tooltip";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Zindex.HasContent())
            yield return ("--bs-tooltip-zindex", Zindex);
        if (MaxWidth.HasContent())
            yield return ("--bs-tooltip-max-width", MaxWidth);
        if (PaddingX.HasContent())
            yield return ("--bs-tooltip-padding-x", PaddingX);
        if (PaddingY.HasContent())
            yield return ("--bs-tooltip-padding-y", PaddingY);
        if (Margin.HasContent())
            yield return ("--bs-tooltip-margin", Margin);
        if (FontSize.HasContent())
            yield return ("--bs-tooltip-font-size", FontSize);
        if (Color.HasContent())
            yield return ("--bs-tooltip-color", Color);
        if (Background.HasContent())
            yield return ("--bs-tooltip-bg", Background);
        if (BorderRadius.HasContent())
            yield return ("--bs-tooltip-border-radius", BorderRadius);
        if (Opacity.HasContent())
            yield return ("--bs-tooltip-opacity", Opacity);
        if (ArrowWidth.HasContent())
            yield return ("--bs-tooltip-arrow-width", ArrowWidth);
        if (ArrowHeight.HasContent())
            yield return ("--bs-tooltip-arrow-height", ArrowHeight);
    }
}