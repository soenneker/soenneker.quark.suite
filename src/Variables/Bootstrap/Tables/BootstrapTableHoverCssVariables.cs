using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapTableHoverCssVariables : IBootstrapCssVariableGroup
{
	public string? HoverBackground { get; set; }

	public string? HoverColor { get; set; }

    public string GetSelector()
    {
        return ".table-hover";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (HoverBackground.HasContent())
            yield return ("--bs-table-hover-bg", HoverBackground);
        if (HoverColor.HasContent())
            yield return ("--bs-table-hover-color", HoverColor);
    }
}

