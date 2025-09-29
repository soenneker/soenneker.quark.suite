
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's offcanvas CSS variables
/// </summary>
public sealed class BootstrapOffcanvasCssVariables
{
    /// <summary>
    /// Offcanvas z-index. Default: 1045
    /// </summary>
    [CssVariable("bs-offcanvas-zindex")]
    public string? OffcanvasZindex { get; set; }

    /// <summary>
    /// Offcanvas padding Y. Default: 1rem
    /// </summary>
    [CssVariable("bs-offcanvas-padding-y")]
    public string? OffcanvasPaddingY { get; set; }

    /// <summary>
    /// Offcanvas padding X. Default: 1rem
    /// </summary>
    [CssVariable("bs-offcanvas-padding-x")]
    public string? OffcanvasPaddingX { get; set; }

    /// <summary>
    /// Offcanvas vertical width. Default: 400px
    /// </summary>
    [CssVariable("bs-offcanvas-vertical-width")]
    public string? OffcanvasVerticalWidth { get; set; }

    /// <summary>
    /// Offcanvas horizontal height. Default: 30vh
    /// </summary>
    [CssVariable("bs-offcanvas-horizontal-height")]
    public string? OffcanvasHorizontalHeight { get; set; }

    /// <summary>
    /// Offcanvas border width. Default: var(--bs-border-width)
    /// </summary>
    [CssVariable("bs-offcanvas-border-width")]
    public string? OffcanvasBorderWidth { get; set; }

    /// <summary>
    /// Offcanvas border color. Default: var(--bs-border-color-translucent)
    /// </summary>
    [CssVariable("bs-offcanvas-border-color")]
    public string? OffcanvasBorderColor { get; set; }

    /// <summary>
    /// Offcanvas box shadow. Default: var(--bs-box-shadow)
    /// </summary>
    [CssVariable("bs-offcanvas-box-shadow")]
    public string? OffcanvasBoxShadow { get; set; }

    /// <summary>
    /// Offcanvas backdrop background color. Default: rgba(0, 0, 0, 0.5)
    /// </summary>
    [CssVariable("bs-offcanvas-backdrop-bg")]
    public string? OffcanvasBackdropBg { get; set; }

    /// <summary>
    /// Offcanvas backdrop opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-offcanvas-backdrop-opacity")]
    public string? OffcanvasBackdropOpacity { get; set; }
}
