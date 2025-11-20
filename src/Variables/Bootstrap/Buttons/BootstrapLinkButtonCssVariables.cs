using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap link button CSS variables
/// </summary>
public class BootstrapLinkButtonCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? HoverColor { get; set; }

	public string? DisabledColor { get; set; }

    public string GetSelector()
    {
        return ".btn-link";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-btn-color", Color);
        if (HoverColor.HasContent())
            yield return ("--bs-btn-hover-color", HoverColor);
        if (DisabledColor.HasContent())
            yield return ("--bs-btn-disabled-color", DisabledColor);
    }
}


