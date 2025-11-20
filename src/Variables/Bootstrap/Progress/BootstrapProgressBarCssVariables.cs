using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public sealed class BootstrapProgressBarCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

    public string GetSelector()
    {
        return ".progress-bar";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-progress-bar-color", Color);
        if (Background.HasContent())
            yield return ("--bs-progress-bar-bg", Background);
    }
}

