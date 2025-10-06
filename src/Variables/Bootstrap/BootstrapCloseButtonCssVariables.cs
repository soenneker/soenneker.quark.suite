namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's close button CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapCloseButtonCssVariables
{
    /// <summary>
    /// Close button color. Default: #000
    /// </summary>
    [CssVariable("bs-btn-close-color")]
    public string? CloseButtonColor { get; set; }

    /// <summary>
    /// Close button background. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23000'%3e%3cpath d='M.293.293a1 1 0 0 1 1.414 0L8 6.586 14.293.293a1 1 0 1 1 1.414 1.414L9.414 8l6.293 6.293a1 1 0 0 1-1.414 1.414L8 9.414l-6.293 6.293a1 1 0 0 1-1.414-1.414L6.586 8 .293 1.707a1 1 0 0 1 0-1.414'/%3e%3c/svg%3e")
    /// </summary>
    [CssVariable("bs-btn-close-bg")]
    public string? CloseButtonBg { get; set; }

    /// <summary>
    /// Close button opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-btn-close-opacity")]
    public string? CloseButtonOpacity { get; set; }

    /// <summary>
    /// Close button hover opacity. Default: 0.75
    /// </summary>
    [CssVariable("bs-btn-close-hover-opacity")]
    public string? CloseButtonHoverOpacity { get; set; }

    /// <summary>
    /// Close button focus shadow. Default: 0 0 0 0.25rem rgba(13, 110, 253, 0.25)
    /// </summary>
    [CssVariable("bs-btn-close-focus-shadow")]
    public string? CloseButtonFocusShadow { get; set; }

    /// <summary>
    /// Close button focus opacity. Default: 1
    /// </summary>
    [CssVariable("bs-btn-close-focus-opacity")]
    public string? CloseButtonFocusOpacity { get; set; }

    /// <summary>
    /// Close button disabled opacity. Default: 0.25
    /// </summary>
    [CssVariable("bs-btn-close-disabled-opacity")]
    public string? CloseButtonDisabledOpacity { get; set; }

    /// <summary>
    /// Close button white filter. Default: invert(1) grayscale(100%) brightness(200%)
    /// </summary>
    [CssVariable("bs-btn-close-white-filter")]
    public string? CloseButtonWhiteFilter { get; set; }
}
