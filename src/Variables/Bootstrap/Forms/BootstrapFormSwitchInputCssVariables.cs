using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public class BootstrapFormSwitchInputCssVariables : IBootstrapCssVariableGroup
{
	public string? Background { get; set; }

	public string? Color { get; set; }

    public string GetSelector()
    {
        return ".form-switch .form-check-input";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Background.HasContent())
            yield return ("--bs-form-switch-bg", Background);
        if (Color.HasContent())
            yield return ("--bs-form-switch-color", Color);
    }
}

