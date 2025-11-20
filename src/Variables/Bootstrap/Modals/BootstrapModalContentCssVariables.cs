using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

public sealed class BootstrapModalContentCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

    public string GetSelector()
    {
        return ".modal-content";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-modal-color", Color);
        if (Background.HasContent())
            yield return ("--bs-modal-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-modal-border-color", BorderColor);
    }
}

