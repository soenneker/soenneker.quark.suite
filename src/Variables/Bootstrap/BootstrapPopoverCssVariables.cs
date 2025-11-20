using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's popover CSS variables
/// </summary>
public class BootstrapPopoverCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Popover z-index. Default: 1070
	/// </summary>
	public string? Zindex { get; set; }

	/// <summary>
	/// Popover max width. Default: 276px
	/// </summary>
	public string? MaxWidth { get; set; }

	/// <summary>
	/// Popover font size. Default: 0.875rem
	/// </summary>
	public string? FontSize { get; set; }

	/// <summary>
	/// Popover background. Default: var(--bs-body-bg)
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Popover border width. Default: var(--bs-border-width)
	/// </summary>
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Popover border color. Default: var(--bs-border-color-translucent)
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Popover border radius. Default: var(--bs-border-radius-lg)
	/// </summary>
	public string? BorderRadius { get; set; }

	/// <summary>
	/// Popover inner border radius. Default: calc(var(--bs-border-radius-lg) - var(--bs-border-width))
	/// </summary>
	public string? InnerBorderRadius { get; set; }

	/// <summary>
	/// Popover box shadow. Default: var(--bs-box-shadow)
	/// </summary>
	public string? BoxShadow { get; set; }

	/// <summary>
	/// Popover header padding X. Default: 1rem
	/// </summary>
	public string? HeaderPaddingX { get; set; }

	/// <summary>
	/// Popover header padding Y. Default: 0.5rem
	/// </summary>
	public string? HeaderPaddingY { get; set; }

	/// <summary>
	/// Popover header font size. Default: 1rem
	/// </summary>
	public string? HeaderFontSize { get; set; }

	/// <summary>
	/// Popover header color. Default: inherit
	/// </summary>
	public string? HeaderColor { get; set; }

	/// <summary>
	/// Popover header background. Default: var(--bs-secondary-bg)
	/// </summary>
	public string? HeaderBackground { get; set; }

	/// <summary>
	/// Popover body padding X. Default: 1rem
	/// </summary>
	public string? BodyPaddingX { get; set; }

	/// <summary>
	/// Popover body padding Y. Default: 1rem
	/// </summary>
	public string? BodyPaddingY { get; set; }

	/// <summary>
	/// Popover body color. Default: var(--bs-body-color)
	/// </summary>
	public string? BodyColor { get; set; }

	/// <summary>
	/// Popover arrow width. Default: 1rem
	/// </summary>
	public string? ArrowWidth { get; set; }

	/// <summary>
	/// Popover arrow height. Default: 0.5rem
	/// </summary>
	public string? ArrowHeight { get; set; }

	/// <summary>
	/// Popover arrow border. Default: var(--bs-popover-border-color)
	/// </summary>
	public string? ArrowBorder { get; set; }

    public string GetSelector()
    {
        return ".popover";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Zindex.HasContent())
            yield return ("--bs-popover-zindex", Zindex);
        if (MaxWidth.HasContent())
            yield return ("--bs-popover-max-width", MaxWidth);
        if (FontSize.HasContent())
            yield return ("--bs-popover-font-size", FontSize);
        if (Background.HasContent())
            yield return ("--bs-popover-bg", Background);
        if (BorderWidth.HasContent())
            yield return ("--bs-popover-border-width", BorderWidth);
        if (BorderColor.HasContent())
            yield return ("--bs-popover-border-color", BorderColor);
        if (BorderRadius.HasContent())
            yield return ("--bs-popover-border-radius", BorderRadius);
        if (InnerBorderRadius.HasContent())
            yield return ("--bs-popover-inner-border-radius", InnerBorderRadius);
        if (BoxShadow.HasContent())
            yield return ("--bs-popover-box-shadow", BoxShadow);
        if (HeaderPaddingX.HasContent())
            yield return ("--bs-popover-header-padding-x", HeaderPaddingX);
        if (HeaderPaddingY.HasContent())
            yield return ("--bs-popover-header-padding-y", HeaderPaddingY);
        if (HeaderFontSize.HasContent())
            yield return ("--bs-popover-header-font-size", HeaderFontSize);
        if (HeaderColor.HasContent())
            yield return ("--bs-popover-header-color", HeaderColor);
        if (HeaderBackground.HasContent())
            yield return ("--bs-popover-header-bg", HeaderBackground);
        if (BodyPaddingX.HasContent())
            yield return ("--bs-popover-body-padding-x", BodyPaddingX);
        if (BodyPaddingY.HasContent())
            yield return ("--bs-popover-body-padding-y", BodyPaddingY);
        if (BodyColor.HasContent())
            yield return ("--bs-popover-body-color", BodyColor);
        if (ArrowWidth.HasContent())
            yield return ("--bs-popover-arrow-width", ArrowWidth);
        if (ArrowHeight.HasContent())
            yield return ("--bs-popover-arrow-height", ArrowHeight);
        if (ArrowBorder.HasContent())
            yield return ("--bs-popover-arrow-border", ArrowBorder);
    }
}


