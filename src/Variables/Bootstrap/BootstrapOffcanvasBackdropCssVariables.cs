namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's offcanvas backdrop CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapOffcanvasBackdropCssVariables
{
    /// <summary>
    /// Offcanvas backdrop z-index. Default: 1040
    /// </summary>
    [CssVariable("bs-offcanvas-backdrop-zindex")]
    public string? OffcanvasBackdropZindex { get; set; }

    /// <summary>
    /// Offcanvas backdrop background. Default: #000
    /// </summary>
    [CssVariable("bs-offcanvas-backdrop-bg")]
    public string? OffcanvasBackdropBg { get; set; }

    /// <summary>
    /// Offcanvas backdrop opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-offcanvas-backdrop-opacity")]
    public string? OffcanvasBackdropOpacity { get; set; }
}
