using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Base CSS variables for Bootstrap badges.
/// </summary>
public abstract class BootstrapBaseBadgeCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? PaddingX { get; set; }

	public string? PaddingY { get; set; }

	public string? FontSize { get; set; }

	public string? FontWeight { get; set; }

    public virtual string GetSelector()
    {
        return ":root";
    }

    public virtual IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-badge-color", Color);
        if (Background.HasContent())
            yield return ("--bs-badge-bg", Background);
        if (PaddingX.HasContent())
            yield return ("--bs-badge-padding-x", PaddingX);
        if (PaddingY.HasContent())
            yield return ("--bs-badge-padding-y", PaddingY);
        if (FontSize.HasContent())
            yield return ("--bs-badge-font-size", FontSize);
        if (FontWeight.HasContent())
            yield return ("--bs-badge-font-weight", FontWeight);
    }
}


