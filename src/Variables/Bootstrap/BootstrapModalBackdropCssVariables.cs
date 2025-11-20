using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's modal backdrop CSS variables
/// </summary>
public sealed class BootstrapModalBackdropCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Backdrop z-index. Default: 1050
    /// </summary>
    public string? BackdropZindex { get; set; }

    /// <summary>
    /// Backdrop background. Default: #000
    /// </summary>
    public string? BackdropBg { get; set; }

    /// <summary>
    /// Backdrop opacity. Default: 0.5
    /// </summary>
    public string? BackdropOpacity { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (BackdropZindex.HasContent())
            yield return ("--bs-backdrop-zindex", BackdropZindex);
        if (BackdropBg.HasContent())
            yield return ("--bs-backdrop-bg", BackdropBg);
        if (BackdropOpacity.HasContent())
            yield return ("--bs-backdrop-opacity", BackdropOpacity);
    }
}
