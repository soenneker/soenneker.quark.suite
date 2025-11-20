using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for offcanvas.
/// </summary>
public class BootstrapOffcanvasCssVariables : IBootstrapCssVariableGroup
{
	public string? Background { get; set; }

	public string? Color { get; set; }

	public string? Width { get; set; }

	public string? Height { get; set; }

	public string? PaddingX { get; set; }

	public string? PaddingY { get; set; }

	public string? BorderWidth { get; set; }

	public string? BorderColor { get; set; }

	public string? BoxShadow { get; set; }

	public string? TitleLineHeight { get; set; }

	public string? CloseSize { get; set; }

	public string? Zindex { get; set; }

    public string GetSelector()
    {
        return ".offcanvas";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Background.HasContent())
            yield return ("--bs-offcanvas-bg", Background);
        if (Color.HasContent())
            yield return ("--bs-offcanvas-color", Color);
        if (Width.HasContent())
            yield return ("--bs-offcanvas-width", Width);
        if (Height.HasContent())
            yield return ("--bs-offcanvas-height", Height);
        if (PaddingX.HasContent())
            yield return ("--bs-offcanvas-padding-x", PaddingX);
        if (PaddingY.HasContent())
            yield return ("--bs-offcanvas-padding-y", PaddingY);
        if (BorderWidth.HasContent())
            yield return ("--bs-offcanvas-border-width", BorderWidth);
        if (BorderColor.HasContent())
            yield return ("--bs-offcanvas-border-color", BorderColor);
        if (BoxShadow.HasContent())
            yield return ("--bs-offcanvas-box-shadow", BoxShadow);
        if (TitleLineHeight.HasContent())
            yield return ("--bs-offcanvas-title-line-height", TitleLineHeight);
        if (CloseSize.HasContent())
            yield return ("--bs-offcanvas-close-size", CloseSize);
        if (Zindex.HasContent())
            yield return ("--bs-offcanvas-zindex", Zindex);
    }
}
