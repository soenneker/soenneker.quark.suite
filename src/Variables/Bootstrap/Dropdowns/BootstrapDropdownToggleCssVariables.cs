using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public sealed class BootstrapDropdownToggleCssVariables : IBootstrapCssVariableGroup
{
	public string? Spacer { get; set; }

    public string GetSelector()
    {
        return "[data-bs-toggle=dropdown]";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Spacer.HasContent())
            yield return ("--bs-dropdown-spacer", Spacer);
    }
}


