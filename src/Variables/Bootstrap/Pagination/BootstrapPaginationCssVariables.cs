using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Variables for pagination.
/// </summary>
public sealed class BootstrapPaginationCssVariables : IBootstrapCssVariableGroup
{
	public string? Color { get; set; }

	public string? Background { get; set; }

	public string? BorderColor { get; set; }

    public string GetSelector()
    {
        return ".pagination";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Color.HasContent())
            yield return ("--bs-pagination-color", Color);
        if (Background.HasContent())
            yield return ("--bs-pagination-bg", Background);
        if (BorderColor.HasContent())
            yield return ("--bs-pagination-border-color", BorderColor);
    }
}

