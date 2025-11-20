using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapNavLinkCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? HoverColor { get; set; }

	public string? DisabledColor { get; set; }

    public string GetSelector()
    {
        return ".nav-link";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-nav-link-color", Color);
        if (HoverColor.HasContent())
            yield return ("--bs-nav-link-hover-color", HoverColor);
        if (DisabledColor.HasContent())
            yield return ("--bs-nav-link-disabled-color", DisabledColor);
    }
}


