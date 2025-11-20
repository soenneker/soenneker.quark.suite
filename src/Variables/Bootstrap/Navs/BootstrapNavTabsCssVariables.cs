using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapNavTabsCssVariables : IBootstrapCssVariableGroup
{
	public string? LinkHoverBorderColor { get; set; }

	public string? LinkActiveColor { get; set; }

	public string? LinkActiveBackground { get; set; }

	public string? LinkActiveBorderColor { get; set; }

    public string GetSelector()
    {
        return ".nav-tabs";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (LinkHoverBorderColor.HasContent())
            yield return ("--bs-nav-tabs-link-hover-border-color", LinkHoverBorderColor);
        if (LinkActiveColor.HasContent())
            yield return ("--bs-nav-tabs-link-active-color", LinkActiveColor);
        if (LinkActiveBackground.HasContent())
            yield return ("--bs-nav-tabs-link-active-bg", LinkActiveBackground);
        if (LinkActiveBorderColor.HasContent())
            yield return ("--bs-nav-tabs-link-active-border-color", LinkActiveBorderColor);
    }
}


