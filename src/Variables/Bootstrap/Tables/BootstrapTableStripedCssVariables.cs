using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public sealed class BootstrapTableStripedCssVariables : IBootstrapCssVariableGroup
{
	public string? StripedBackground { get; set; }

	public string? StripedColor { get; set; }

    public string GetSelector()
    {
        return ".table-striped";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (StripedBackground.HasContent())
            yield return ("--bs-table-striped-bg", StripedBackground);
        if (StripedColor.HasContent())
            yield return ("--bs-table-striped-color", StripedColor);
    }
}

