using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for progress.
/// </summary>
public class BootstrapProgressCssVariables : IBootstrapCssVariableGroup
{
	public string? Background { get; set; }

    public string GetSelector()
    {
        return ".progress";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Background.HasContent())
            yield return ("--bs-progress-bg", Background);
    }
}

