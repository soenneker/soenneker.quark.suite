using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for offcanvas.
/// </summary>
public sealed class BootstrapOffcanvasCssVariables : IBootstrapCssVariableGroup
{
	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas background color.
	/// </summary>
	public string? Background { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas text color.
	/// </summary>
	public string? Color { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas width.
	/// </summary>
	public string? Width { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas height.
	/// </summary>
	public string? Height { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas horizontal padding.
	/// </summary>
	public string? PaddingX { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas vertical padding.
	/// </summary>
	public string? PaddingY { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas border width.
	/// </summary>
	public string? BorderWidth { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas border color.
	/// </summary>
	public string? BorderColor { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas box shadow.
	/// </summary>
	public string? BoxShadow { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas title line height.
	/// </summary>
	public string? TitleLineHeight { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas close button size.
	/// </summary>
	public string? CloseSize { get; set; }

	/// <summary>
	/// Gets or sets the CSS variable value for the offcanvas z-index.
	/// </summary>
	public string? Zindex { get; set; }

	/// <summary>
	/// Gets the CSS selector for the offcanvas component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ".offcanvas";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the offcanvas component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
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
