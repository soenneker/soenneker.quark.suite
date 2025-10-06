namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's modal backdrop CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapModalBackdropCssVariables
{
    /// <summary>
    /// Backdrop z-index. Default: 1050
    /// </summary>
    [CssVariable("bs-backdrop-zindex")]
    public string? BackdropZindex { get; set; }

    /// <summary>
    /// Backdrop background. Default: #000
    /// </summary>
    [CssVariable("bs-backdrop-bg")]
    public string? BackdropBg { get; set; }

    /// <summary>
    /// Backdrop opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-backdrop-opacity")]
    public string? BackdropOpacity { get; set; }
}
