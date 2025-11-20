using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's general CSS variables
/// </summary>
public sealed class BootstrapGeneralCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Gradient. Default: linear-gradient(180deg, rgba(255, 255, 255, 0.15), rgba(255, 255, 255, 0))
    /// </summary>
    public string? Gradient { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Gradient.HasContent())
            yield return ("--bs-gradient", Gradient);
    }
}
