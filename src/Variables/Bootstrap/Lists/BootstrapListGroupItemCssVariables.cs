using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public sealed class BootstrapListGroupItemCssVariables : IBootstrapCssVariableGroup
{
	public string? PaddingY { get; set; }

	public string? PaddingX { get; set; }

	public string? ActionHoverColor { get; set; }

	public string? ActionHoverBackground { get; set; }

	public string? ActiveColor { get; set; }

	public string? ActiveBackground { get; set; }

	public string? ActiveBorderColor { get; set; }

    public string GetSelector()
    {
        return ".list-group-item";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (PaddingY.HasContent())
            yield return ("--bs-list-group-item-padding-y", PaddingY);
        if (PaddingX.HasContent())
            yield return ("--bs-list-group-item-padding-x", PaddingX);
        if (ActionHoverColor.HasContent())
            yield return ("--bs-list-group-action-hover-color", ActionHoverColor);
        if (ActionHoverBackground.HasContent())
            yield return ("--bs-list-group-action-hover-bg", ActionHoverBackground);
        if (ActiveColor.HasContent())
            yield return ("--bs-list-group-active-color", ActiveColor);
        if (ActiveBackground.HasContent())
            yield return ("--bs-list-group-active-bg", ActiveBackground);
        if (ActiveBorderColor.HasContent())
            yield return ("--bs-list-group-active-border-color", ActiveBorderColor);
    }
}

