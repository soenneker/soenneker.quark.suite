using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapPageLinkCssVariables : IBootstrapCssVariableGroup
{
	public string? HoverColor { get; set; }

	public string? HoverBackground { get; set; }

	public string? HoverBorderColor { get; set; }

	public string? ActiveColor { get; set; }

	public string? ActiveBackground { get; set; }

	public string? ActiveBorderColor { get; set; }

	public string? DisabledColor { get; set; }

	public string? DisabledBackground { get; set; }

	public string? DisabledBorderColor { get; set; }

    public string GetSelector()
    {
        return ".page-link";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (HoverColor.HasContent())
            yield return ("--bs-pagination-hover-color", HoverColor);
        if (HoverBackground.HasContent())
            yield return ("--bs-pagination-hover-bg", HoverBackground);
        if (HoverBorderColor.HasContent())
            yield return ("--bs-pagination-hover-border-color", HoverBorderColor);
        if (ActiveColor.HasContent())
            yield return ("--bs-pagination-active-color", ActiveColor);
        if (ActiveBackground.HasContent())
            yield return ("--bs-pagination-active-bg", ActiveBackground);
        if (ActiveBorderColor.HasContent())
            yield return ("--bs-pagination-active-border-color", ActiveBorderColor);
        if (DisabledColor.HasContent())
            yield return ("--bs-pagination-disabled-color", DisabledColor);
        if (DisabledBackground.HasContent())
            yield return ("--bs-pagination-disabled-bg", DisabledBackground);
        if (DisabledBorderColor.HasContent())
            yield return ("--bs-pagination-disabled-border-color", DisabledBorderColor);
    }
}

