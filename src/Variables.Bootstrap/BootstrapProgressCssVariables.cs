
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's progress CSS variables
/// </summary>
public sealed class BootstrapProgressCssVariables
{
    /// <summary>
    /// Progress height. Default: 1rem
    /// </summary>
    [CssVariable("bs-progress-height")]
    public string? ProgressHeight { get; set; }

    /// <summary>
    /// Progress font size. Default: var(--bs-font-size-sm)
    /// </summary>
    [CssVariable("bs-progress-font-size")]
    public string? ProgressFontSize { get; set; }

    /// <summary>
    /// Progress background color. Default: var(--bs-secondary-bg)
    /// </summary>
    [CssVariable("bs-progress-bg")]
    public string? ProgressBg { get; set; }

    /// <summary>
    /// Progress border radius. Default: var(--bs-border-radius)
    /// </summary>
    [CssVariable("bs-progress-border-radius")]
    public string? ProgressBorderRadius { get; set; }

    /// <summary>
    /// Progress box shadow. Default: inset 0 1px 2px rgba(0, 0, 0, 0.075)
    /// </summary>
    [CssVariable("bs-progress-box-shadow")]
    public string? ProgressBoxShadow { get; set; }

    /// <summary>
    /// Progress bar color. Default: var(--bs-white)
    /// </summary>
    [CssVariable("bs-progress-bar-color")]
    public string? ProgressBarColor { get; set; }

    /// <summary>
    /// Progress bar background color. Default: var(--bs-primary)
    /// </summary>
    [CssVariable("bs-progress-bar-bg")]
    public string? ProgressBarBg { get; set; }

    /// <summary>
    /// Progress bar border radius. Default: var(--bs-progress-border-radius)
    /// </summary>
    [CssVariable("bs-progress-bar-border-radius")]
    public string? ProgressBarBorderRadius { get; set; }

    /// <summary>
    /// Progress bar transition. Default: width 0.6s ease
    /// </summary>
    [CssVariable("bs-progress-bar-transition")]
    public string? ProgressBarTransition { get; set; }

    /// <summary>
    /// Progress bar animation. Default: progress-bar-stripes 1s linear infinite
    /// </summary>
    [CssVariable("bs-progress-bar-animation")]
    public string? ProgressBarAnimation { get; set; }

    /// <summary>
    /// Progress bar striped background image. Default: linear-gradient(45deg, rgba(255, 255, 255, 0.15) 25%, transparent 25%, transparent 50%, rgba(255, 255, 255, 0.15) 50%, rgba(255, 255, 255, 0.15) 75%, transparent 75%, transparent)
    /// </summary>
    [CssVariable("bs-progress-bar-striped-bg")]
    public string? ProgressBarStripedBg { get; set; }

    /// <summary>
    /// Progress bar striped background size. Default: 1rem 1rem
    /// </summary>
    [CssVariable("bs-progress-bar-striped-bg-size")]
    public string? ProgressBarStripedBgSize { get; set; }
}
