using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for dropdown menus and items.
/// </summary>
public class BootstrapDropdownMenuCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

    public string GetSelector()
    {
        return ".dropdown-menu";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-dropdown-color", Color);
        if (Background.HasContent())
            yield return ("--bs-dropdown-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-dropdown-border-color", BorderColor);
    }
}

