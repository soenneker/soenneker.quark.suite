using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's link CSS variables and direct CSS properties
/// </summary>
public sealed class BootstrapLinkCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Link color RGB values. Default: 13, 110, 253
    /// </summary>
    public string? LinkColorRgb { get; set; }

    /// <summary>
    /// Link opacity. Default: 1
    /// </summary>
    public string? LinkOpacity { get; set; }

    /// <summary>
    /// Link hover color RGB values. Default: 10, 88, 202
    /// </summary>
    public string? LinkHoverColorRgb { get; set; }

    /// <summary>
    /// Link hover opacity. Default: 1
    /// </summary>
    public string? LinkHoverOpacity { get; set; }

    /// <summary>
    /// Link underline offset. Default: 0.125em
    /// </summary>
    public string? LinkUnderlineOffset { get; set; }

    /// <summary>
    /// Link underline opacity. Default: 1
    /// </summary>
    public string? LinkUnderlineOpacity { get; set; }

    /// <summary>
    /// Direct CSS text-decoration property. Set to "none" to remove underlines, "underline" to add them.
    /// </summary>
    public string? TextDecoration { get; set; }

	/// <summary>
	/// Gets the CSS selector for the link component.
	/// </summary>
	/// <returns>The CSS selector string.</returns>
    public string GetSelector()
    {
        return ":root";
    }

	/// <summary>
	/// Gets the collection of CSS variables for the link component.
	/// </summary>
	/// <returns>An enumerable collection of CSS property name and value tuples.</returns>
    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (LinkColorRgb.HasContent())
            yield return ("--bs-link-color-rgb", LinkColorRgb);
        if (LinkOpacity.HasContent())
            yield return ("--bs-link-opacity", LinkOpacity);
        if (LinkHoverColorRgb.HasContent())
            yield return ("--bs-link-hover-color-rgb", LinkHoverColorRgb);
        if (LinkHoverOpacity.HasContent())
            yield return ("--bs-link-hover-opacity", LinkHoverOpacity);
        if (LinkUnderlineOffset.HasContent())
            yield return ("--bs-link-underline-offset", LinkUnderlineOffset);
        if (LinkUnderlineOpacity.HasContent())
            yield return ("--bs-link-underline-opacity", LinkUnderlineOpacity);
        if (TextDecoration.HasContent())
            yield return ("text-decoration", TextDecoration);
    }
}

