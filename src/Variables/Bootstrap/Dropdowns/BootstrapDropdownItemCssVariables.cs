using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapDropdownItemCssVariables : IBootstrapCssVariableGroup
{
	public string? LinkColor { get; set; }

	public string? LinkHoverColor { get; set; }

	public string? LinkHoverBackground { get; set; }

	public string? LinkActiveColor { get; set; }

	public string? LinkActiveBackground { get; set; }

	public string? LinkDisabledColor { get; set; }

    public string GetSelector()
    {
        return ".dropdown-item";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (LinkColor.HasContent())
            yield return ("--bs-dropdown-link-color", LinkColor);
        if (LinkHoverColor.HasContent())
            yield return ("--bs-dropdown-link-hover-color", LinkHoverColor);
        if (LinkHoverBackground.HasContent())
            yield return ("--bs-dropdown-link-hover-bg", LinkHoverBackground);
        if (LinkActiveColor.HasContent())
            yield return ("--bs-dropdown-link-active-color", LinkActiveColor);
        if (LinkActiveBackground.HasContent())
            yield return ("--bs-dropdown-link-active-bg", LinkActiveBackground);
        if (LinkDisabledColor.HasContent())
            yield return ("--bs-dropdown-link-disabled-color", LinkDisabledColor);
    }
}


