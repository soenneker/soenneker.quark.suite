using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for .list-group and items.
/// </summary>
public class BootstrapListGroupCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

    public string GetSelector()
    {
        return ".list-group";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-list-group-color", Color);
        if (Background.HasContent())
            yield return ("--bs-list-group-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-list-group-border-color", BorderColor);
    }
}

