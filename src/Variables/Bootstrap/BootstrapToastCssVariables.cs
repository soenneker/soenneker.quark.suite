using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's toast CSS variables
/// </summary>
public sealed class BootstrapToastCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Toast z-index. Default: 1090
	/// </summary>
	public string? Zindex { get; set; }

	/// <summary>
	/// Toast padding X. Default: 0.75rem
	/// </summary>
	public string? PaddingX { get; set; }

	/// <summary>
	/// Toast padding Y. Default: 0.5rem
	/// </summary>
	public string? PaddingY { get; set; }

	/// <summary>
	/// Toast spacing. Default: 1.5rem
	/// </summary>
	public string? Spacing { get; set; }

	/// <summary>
	/// Toast max width. Default: 350px
	/// </summary>
	public string? MaxWidth { get; set; }

	/// <summary>
	/// Toast font size. Default: 0.875rem
	/// </summary>
	public string? FontSize { get; set; }

	/// <summary>
	/// Toast color. Default: inherit
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Toast background. Default: rgba(var(--bs-body-bg-rgb), 0.85)
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Toast border width. Default: var(--bs-border-width)
	/// </summary>
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Toast border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Toast border radius. Default: var(--bs-border-radius)
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Toast box shadow. Default: var(--bs-box-shadow)
	/// </summary>
	public string? BoxShadow { get; set; }

	/// <summary>
	/// Toast header color. Default: var(--bs-secondary-color)
	/// </summary>
	public string? HeaderColor { get; set; }

	/// <summary>
	/// Toast header background. Default: rgba(var(--bs-body-bg-rgb), 0.85)
	/// </summary>
	public string? HeaderBg { get; set; }

	/// <summary>
	/// Toast header border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	public string? HeaderBorderColor { get; set; }

    public string GetSelector()
    {
        return ".toast";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Zindex.HasContent())
            yield return ("--bs-toast-zindex", Zindex);
        if (PaddingX.HasContent())
            yield return ("--bs-toast-padding-x", PaddingX);
        if (PaddingY.HasContent())
            yield return ("--bs-toast-padding-y", PaddingY);
        if (Spacing.HasContent())
            yield return ("--bs-toast-spacing", Spacing);
        if (MaxWidth.HasContent())
            yield return ("--bs-toast-max-width", MaxWidth);
        if (FontSize.HasContent())
            yield return ("--bs-toast-font-size", FontSize);
        if (Color.HasContent())
            yield return ("--bs-toast-color", Color);
        if (Background.HasContent())
            yield return ("--bs-toast-bg", Background);
        if (BorderWidth.HasContent())
            yield return ("--bs-toast-border-width", BorderWidth);
        if (BorderColor.HasContent())
            yield return ("--bs-toast-border-color", BorderColor);
        if (BorderRadius.HasContent())
            yield return ("--bs-toast-border-radius", BorderRadius);
        if (BoxShadow.HasContent())
            yield return ("--bs-toast-box-shadow", BoxShadow);
        if (HeaderColor.HasContent())
            yield return ("--bs-toast-header-color", HeaderColor);
        if (HeaderBg.HasContent())
            yield return ("--bs-toast-header-bg", HeaderBg);
        if (HeaderBorderColor.HasContent())
            yield return ("--bs-toast-header-border-color", HeaderBorderColor);
    }
}


