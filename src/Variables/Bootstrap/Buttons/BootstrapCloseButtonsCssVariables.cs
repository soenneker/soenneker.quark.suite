namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's close button CSS variables
/// </summary>
[CssSelector]
public sealed class BootstrapCloseButtonsCssVariables
{
    /// <summary>
    /// Close button color. Default: #000
    /// </summary>
    [CssVariable("bs-btn-close-color")]
    public string? BtnCloseColor { get; set; }

    /// <summary>
    /// Close button background. Default: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23000'%3e%3cpath d='m.5.5 15 15m-15 0 15-15'/%3e%3c/svg%3e") center/1em auto no-repeat
    /// </summary>
    [CssVariable("bs-btn-close-bg")]
    public string? BtnCloseBg { get; set; }

    /// <summary>
    /// Close button border. Default: 0
    /// </summary>
    [CssVariable("bs-btn-close-border")]
    public string? BtnCloseBorder { get; set; }

    /// <summary>
    /// Close button border radius. Default: 0.375rem
    /// </summary>
    [CssVariable("bs-btn-close-border-radius")]
    public string? BtnCloseBorderRadius { get; set; }

    /// <summary>
    /// Close button opacity. Default: 0.5
    /// </summary>
    [CssVariable("bs-btn-close-opacity")]
    public string? BtnCloseOpacity { get; set; }

    /// <summary>
    /// Close button hover opacity. Default: 0.75
    /// </summary>
    [CssVariable("bs-btn-close-hover-opacity")]
    public string? BtnCloseHoverOpacity { get; set; }

    /// <summary>
    /// Close button focus opacity. Default: 1
    /// </summary>
    [CssVariable("bs-btn-close-focus-opacity")]
    public string? BtnCloseFocusOpacity { get; set; }

    /// <summary>
    /// Close button focus box shadow. Default: 0 0 0 0.25rem rgba(13, 110, 253, 0.25)
    /// </summary>
    [CssVariable("bs-btn-close-focus-box-shadow")]
    public string? BtnCloseFocusBoxShadow { get; set; }

    /// <summary>
    /// Close button disabled opacity. Default: 0.25
    /// </summary>
    [CssVariable("bs-btn-close-disabled-opacity")]
    public string? BtnCloseDisabledOpacity { get; set; }

    /// <summary>
    /// Close button white filter. Default: invert(1) grayscale(100%) brightness(200%)
    /// </summary>
    [CssVariable("bs-btn-close-white-filter")]
    public string? BtnCloseWhiteFilter { get; set; }
}
