using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's offcanvas backdrop CSS variables
/// </summary>
public sealed class BootstrapOffcanvasBackdropCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Offcanvas backdrop z-index. Default: 1040
    /// </summary>
    public string? OffcanvasBackdropZindex { get; set; }

    /// <summary>
    /// Offcanvas backdrop background. Default: #000
    /// </summary>
    public string? OffcanvasBackdropBg { get; set; }

    /// <summary>
    /// Offcanvas backdrop opacity. Default: 0.5
    /// </summary>
    public string? OffcanvasBackdropOpacity { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (OffcanvasBackdropZindex.HasContent())
            yield return ("--bs-offcanvas-backdrop-zindex", OffcanvasBackdropZindex);
        if (OffcanvasBackdropBg.HasContent())
            yield return ("--bs-offcanvas-backdrop-bg", OffcanvasBackdropBg);
        if (OffcanvasBackdropOpacity.HasContent())
            yield return ("--bs-offcanvas-backdrop-opacity", OffcanvasBackdropOpacity);
    }
}
