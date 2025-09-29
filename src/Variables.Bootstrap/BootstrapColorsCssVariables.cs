
namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's color CSS variables
/// </summary>
public sealed class BootstrapColorsCssVariables
{
    // Basic Colors
    /// <summary>
    /// Blue color. Default: #0d6efd
    /// </summary>
    [CssVariable("bs-blue")]
    public string? Blue { get; set; }

    /// <summary>
    /// Indigo color. Default: #6610f2
    /// </summary>
    [CssVariable("bs-indigo")]
    public string? Indigo { get; set; }

    /// <summary>
    /// Purple color. Default: #6f42c1
    /// </summary>
    [CssVariable("bs-purple")]
    public string? Purple { get; set; }

    /// <summary>
    /// Pink color. Default: #d63384
    /// </summary>
    [CssVariable("bs-pink")]
    public string? Pink { get; set; }

    /// <summary>
    /// Red color. Default: #dc3545
    /// </summary>
    [CssVariable("bs-red")]
    public string? Red { get; set; }

    /// <summary>
    /// Orange color. Default: #fd7e14
    /// </summary>
    [CssVariable("bs-orange")]
    public string? Orange { get; set; }

    /// <summary>
    /// Yellow color. Default: #ffc107
    /// </summary>
    [CssVariable("bs-yellow")]
    public string? Yellow { get; set; }

    /// <summary>
    /// Green color. Default: #198754
    /// </summary>
    [CssVariable("bs-green")]
    public string? Green { get; set; }

    /// <summary>
    /// Teal color. Default: #20c997
    /// </summary>
    [CssVariable("bs-teal")]
    public string? Teal { get; set; }

    /// <summary>
    /// Cyan color. Default: #0dcaf0
    /// </summary>
    [CssVariable("bs-cyan")]
    public string? Cyan { get; set; }

    /// <summary>
    /// Black color. Default: #000
    /// </summary>
    [CssVariable("bs-black")]
    public string? Black { get; set; }

    /// <summary>
    /// White color. Default: #fff
    /// </summary>
    [CssVariable("bs-white")]
    public string? White { get; set; }

    /// <summary>
    /// Gray color. Default: #6c757d
    /// </summary>
    [CssVariable("bs-gray")]
    public string? Gray { get; set; }

    /// <summary>
    /// Gray dark color. Default: #343a40
    /// </summary>
    [CssVariable("bs-gray-dark")]
    public string? GrayDark { get; set; }

    // Gray Scale
    /// <summary>
    /// Gray 100 color. Default: #f8f9fa
    /// </summary>
    [CssVariable("bs-gray-100")]
    public string? Gray100 { get; set; }

    /// <summary>
    /// Gray 200 color. Default: #e9ecef
    /// </summary>
    [CssVariable("bs-gray-200")]
    public string? Gray200 { get; set; }

    /// <summary>
    /// Gray 300 color. Default: #dee2e6
    /// </summary>
    [CssVariable("bs-gray-300")]
    public string? Gray300 { get; set; }

    /// <summary>
    /// Gray 400 color. Default: #ced4da
    /// </summary>
    [CssVariable("bs-gray-400")]
    public string? Gray400 { get; set; }

    /// <summary>
    /// Gray 500 color. Default: #adb5bd
    /// </summary>
    [CssVariable("bs-gray-500")]
    public string? Gray500 { get; set; }

    /// <summary>
    /// Gray 600 color. Default: #6c757d
    /// </summary>
    [CssVariable("bs-gray-600")]
    public string? Gray600 { get; set; }

    /// <summary>
    /// Gray 700 color. Default: #495057
    /// </summary>
    [CssVariable("bs-gray-700")]
    public string? Gray700 { get; set; }

    /// <summary>
    /// Gray 800 color. Default: #343a40
    /// </summary>
    [CssVariable("bs-gray-800")]
    public string? Gray800 { get; set; }

    /// <summary>
    /// Gray 900 color. Default: #212529
    /// </summary>
    [CssVariable("bs-gray-900")]
    public string? Gray900 { get; set; }

    // Theme Colors
    /// <summary>
    /// Primary color. Default: #0d6efd
    /// </summary>
    [CssVariable("bs-primary")]
    public string? Primary { get; set; }

    /// <summary>
    /// Secondary color. Default: #6c757d
    /// </summary>
    [CssVariable("bs-secondary")]
    public string? Secondary { get; set; }

    /// <summary>
    /// Success color. Default: #198754
    /// </summary>
    [CssVariable("bs-success")]
    public string? Success { get; set; }

    /// <summary>
    /// Info color. Default: #0dcaf0
    /// </summary>
    [CssVariable("bs-info")]
    public string? Info { get; set; }

    /// <summary>
    /// Warning color. Default: #ffc107
    /// </summary>
    [CssVariable("bs-warning")]
    public string? Warning { get; set; }

    /// <summary>
    /// Danger color. Default: #dc3545
    /// </summary>
    [CssVariable("bs-danger")]
    public string? Danger { get; set; }

    /// <summary>
    /// Light color. Default: #f8f9fa
    /// </summary>
    [CssVariable("bs-light")]
    public string? Light { get; set; }

    /// <summary>
    /// Dark color. Default: #212529
    /// </summary>
    [CssVariable("bs-dark")]
    public string? Dark { get; set; }

    // RGB Values
    /// <summary>
    /// Primary RGB values. Default: 13, 110, 253
    /// </summary>
    [CssVariable("bs-primary-rgb")]
    public string? PrimaryRgb { get; set; }

    /// <summary>
    /// Secondary RGB values. Default: 108, 117, 125
    /// </summary>
    [CssVariable("bs-secondary-rgb")]
    public string? SecondaryRgb { get; set; }

    /// <summary>
    /// Success RGB values. Default: 25, 135, 84
    /// </summary>
    [CssVariable("bs-success-rgb")]
    public string? SuccessRgb { get; set; }

    /// <summary>
    /// Info RGB values. Default: 13, 202, 240
    /// </summary>
    [CssVariable("bs-info-rgb")]
    public string? InfoRgb { get; set; }

    /// <summary>
    /// Warning RGB values. Default: 255, 193, 7
    /// </summary>
    [CssVariable("bs-warning-rgb")]
    public string? WarningRgb { get; set; }

    /// <summary>
    /// Danger RGB values. Default: 220, 53, 69
    /// </summary>
    [CssVariable("bs-danger-rgb")]
    public string? DangerRgb { get; set; }

    /// <summary>
    /// Light RGB values. Default: 248, 249, 250
    /// </summary>
    [CssVariable("bs-light-rgb")]
    public string? LightRgb { get; set; }

    /// <summary>
    /// Dark RGB values. Default: 33, 37, 41
    /// </summary>
    [CssVariable("bs-dark-rgb")]
    public string? DarkRgb { get; set; }

    /// <summary>
    /// White RGB values. Default: 255, 255, 255
    /// </summary>
    [CssVariable("bs-white-rgb")]
    public string? WhiteRgb { get; set; }

    /// <summary>
    /// Black RGB values. Default: 0, 0, 0
    /// </summary>
    [CssVariable("bs-black-rgb")]
    public string? BlackRgb { get; set; }

    // Text Emphasis Colors
    /// <summary>
    /// Primary text emphasis color. Default: #052c65
    /// </summary>
    [CssVariable("bs-primary-text-emphasis")]
    public string? PrimaryTextEmphasis { get; set; }

    /// <summary>
    /// Secondary text emphasis color. Default: #2b2f32
    /// </summary>
    [CssVariable("bs-secondary-text-emphasis")]
    public string? SecondaryTextEmphasis { get; set; }

    /// <summary>
    /// Success text emphasis color. Default: #0a3622
    /// </summary>
    [CssVariable("bs-success-text-emphasis")]
    public string? SuccessTextEmphasis { get; set; }

    /// <summary>
    /// Info text emphasis color. Default: #055160
    /// </summary>
    [CssVariable("bs-info-text-emphasis")]
    public string? InfoTextEmphasis { get; set; }

    /// <summary>
    /// Warning text emphasis color. Default: #664d03
    /// </summary>
    [CssVariable("bs-warning-text-emphasis")]
    public string? WarningTextEmphasis { get; set; }

    /// <summary>
    /// Danger text emphasis color. Default: #58151c
    /// </summary>
    [CssVariable("bs-danger-text-emphasis")]
    public string? DangerTextEmphasis { get; set; }

    /// <summary>
    /// Light text emphasis color. Default: #495057
    /// </summary>
    [CssVariable("bs-light-text-emphasis")]
    public string? LightTextEmphasis { get; set; }

    /// <summary>
    /// Dark text emphasis color. Default: #495057
    /// </summary>
    [CssVariable("bs-dark-text-emphasis")]
    public string? DarkTextEmphasis { get; set; }

    // Background Subtle Colors
    /// <summary>
    /// Primary background subtle color. Default: #cfe2ff
    /// </summary>
    [CssVariable("bs-primary-bg-subtle")]
    public string? PrimaryBgSubtle { get; set; }

    /// <summary>
    /// Secondary background subtle color. Default: #e2e3e5
    /// </summary>
    [CssVariable("bs-secondary-bg-subtle")]
    public string? SecondaryBgSubtle { get; set; }

    /// <summary>
    /// Success background subtle color. Default: #d1e7dd
    /// </summary>
    [CssVariable("bs-success-bg-subtle")]
    public string? SuccessBgSubtle { get; set; }

    /// <summary>
    /// Info background subtle color. Default: #cff4fc
    /// </summary>
    [CssVariable("bs-info-bg-subtle")]
    public string? InfoBgSubtle { get; set; }

    /// <summary>
    /// Warning background subtle color. Default: #fff3cd
    /// </summary>
    [CssVariable("bs-warning-bg-subtle")]
    public string? WarningBgSubtle { get; set; }

    /// <summary>
    /// Danger background subtle color. Default: #f8d7da
    /// </summary>
    [CssVariable("bs-danger-bg-subtle")]
    public string? DangerBgSubtle { get; set; }

    /// <summary>
    /// Light background subtle color. Default: #fcfcfd
    /// </summary>
    [CssVariable("bs-light-bg-subtle")]
    public string? LightBgSubtle { get; set; }

    /// <summary>
    /// Dark background subtle color. Default: #ced4da
    /// </summary>
    [CssVariable("bs-dark-bg-subtle")]
    public string? DarkBgSubtle { get; set; }

    // Border Subtle Colors
    /// <summary>
    /// Primary border subtle color. Default: #9ec5fe
    /// </summary>
    [CssVariable("bs-primary-border-subtle")]
    public string? PrimaryBorderSubtle { get; set; }

    /// <summary>
    /// Secondary border subtle color. Default: #c4c8cb
    /// </summary>
    [CssVariable("bs-secondary-border-subtle")]
    public string? SecondaryBorderSubtle { get; set; }

    /// <summary>
    /// Success border subtle color. Default: #a3cfbb
    /// </summary>
    [CssVariable("bs-success-border-subtle")]
    public string? SuccessBorderSubtle { get; set; }

    /// <summary>
    /// Info border subtle color. Default: #9eeaf9
    /// </summary>
    [CssVariable("bs-info-border-subtle")]
    public string? InfoBorderSubtle { get; set; }

    /// <summary>
    /// Warning border subtle color. Default: #ffe69c
    /// </summary>
    [CssVariable("bs-warning-border-subtle")]
    public string? WarningBorderSubtle { get; set; }

    /// <summary>
    /// Danger border subtle color. Default: #f1aeb5
    /// </summary>
    [CssVariable("bs-danger-border-subtle")]
    public string? DangerBorderSubtle { get; set; }

    /// <summary>
    /// Light border subtle color. Default: #e9ecef
    /// </summary>
    [CssVariable("bs-light-border-subtle")]
    public string? LightBorderSubtle { get; set; }

    /// <summary>
    /// Dark border subtle color. Default: #adb5bd
    /// </summary>
    [CssVariable("bs-dark-border-subtle")]
    public string? DarkBorderSubtle { get; set; }
}
