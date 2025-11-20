using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's color CSS variables
/// </summary>
public sealed class BootstrapColorsCssVariables : IBootstrapCssVariableGroup
{
    // Basic Colors
    /// <summary>
    /// Blue color. Default: #0d6efd
    /// </summary>
    public string? Blue { get; set; }

    /// <summary>
    /// Indigo color. Default: #6610f2
    /// </summary>
    public string? Indigo { get; set; }

    /// <summary>
    /// Purple color. Default: #6f42c1
    /// </summary>
    public string? Purple { get; set; }

    /// <summary>
    /// Pink color. Default: #d63384
    /// </summary>
    public string? Pink { get; set; }

    /// <summary>
    /// Red color. Default: #dc3545
    /// </summary>
    public string? Red { get; set; }

    /// <summary>
    /// Orange color. Default: #fd7e14
    /// </summary>
    public string? Orange { get; set; }

    /// <summary>
    /// Yellow color. Default: #ffc107
    /// </summary>
    public string? Yellow { get; set; }

    /// <summary>
    /// Green color. Default: #198754
    /// </summary>
    public string? Green { get; set; }

    /// <summary>
    /// Teal color. Default: #20c997
    /// </summary>
    public string? Teal { get; set; }

    /// <summary>
    /// Cyan color. Default: #0dcaf0
    /// </summary>
    public string? Cyan { get; set; }

    /// <summary>
    /// Black color. Default: #000
    /// </summary>
    public string? Black { get; set; }

    /// <summary>
    /// White color. Default: #fff
    /// </summary>
    public string? White { get; set; }

    /// <summary>
    /// Gray color. Default: #6c757d
    /// </summary>
    public string? Gray { get; set; }

    /// <summary>
    /// Gray dark color. Default: #343a40
    /// </summary>
    public string? GrayDark { get; set; }

    // Gray Scale
    /// <summary>
    /// Gray 100 color. Default: #f8f9fa
    /// </summary>
    public string? Gray100 { get; set; }

    /// <summary>
    /// Gray 200 color. Default: #e9ecef
    /// </summary>
    public string? Gray200 { get; set; }

    /// <summary>
    /// Gray 300 color. Default: #dee2e6
    /// </summary>
    public string? Gray300 { get; set; }

    /// <summary>
    /// Gray 400 color. Default: #ced4da
    /// </summary>
    public string? Gray400 { get; set; }

    /// <summary>
    /// Gray 500 color. Default: #adb5bd
    /// </summary>
    public string? Gray500 { get; set; }

    /// <summary>
    /// Gray 600 color. Default: #6c757d
    /// </summary>
    public string? Gray600 { get; set; }

    /// <summary>
    /// Gray 700 color. Default: #495057
    /// </summary>
    public string? Gray700 { get; set; }

    /// <summary>
    /// Gray 800 color. Default: #343a40
    /// </summary>
    public string? Gray800 { get; set; }

    /// <summary>
    /// Gray 900 color. Default: #212529
    /// </summary>
    public string? Gray900 { get; set; }

    // Theme Colors
    /// <summary>
    /// Primary color. Default: #0d6efd
    /// </summary>
    public string? Primary { get; set; }

    /// <summary>
    /// Secondary color. Default: #6c757d
    /// </summary>
    public string? Secondary { get; set; }

    /// <summary>
    /// Success color. Default: #198754
    /// </summary>
    public string? Success { get; set; }

    /// <summary>
    /// Info color. Default: #0dcaf0
    /// </summary>
    public string? Info { get; set; }

    /// <summary>
    /// Warning color. Default: #ffc107
    /// </summary>
    public string? Warning { get; set; }

    /// <summary>
    /// Danger color. Default: #dc3545
    /// </summary>
    public string? Danger { get; set; }

    /// <summary>
    /// Light color. Default: #f8f9fa
    /// </summary>
    public string? Light { get; set; }

    /// <summary>
    /// Dark color. Default: #212529
    /// </summary>
    public string? Dark { get; set; }

    // RGB Values
    /// <summary>
    /// Primary RGB values. Default: 13, 110, 253
    /// </summary>
    public string? PrimaryRgb { get; set; }

    /// <summary>
    /// Secondary RGB values. Default: 108, 117, 125
    /// </summary>
    public string? SecondaryRgb { get; set; }

    /// <summary>
    /// Success RGB values. Default: 25, 135, 84
    /// </summary>
    public string? SuccessRgb { get; set; }

    /// <summary>
    /// Info RGB values. Default: 13, 202, 240
    /// </summary>
    public string? InfoRgb { get; set; }

    /// <summary>
    /// Warning RGB values. Default: 255, 193, 7
    /// </summary>
    public string? WarningRgb { get; set; }

    /// <summary>
    /// Danger RGB values. Default: 220, 53, 69
    /// </summary>
    public string? DangerRgb { get; set; }

    /// <summary>
    /// Light RGB values. Default: 248, 249, 250
    /// </summary>
    public string? LightRgb { get; set; }

    /// <summary>
    /// Dark RGB values. Default: 33, 37, 41
    /// </summary>
    public string? DarkRgb { get; set; }

    /// <summary>
    /// White RGB values. Default: 255, 255, 255
    /// </summary>
    public string? WhiteRgb { get; set; }

    /// <summary>
    /// Black RGB values. Default: 0, 0, 0
    /// </summary>
    public string? BlackRgb { get; set; }

    // Text Emphasis Colors
    /// <summary>
    /// Primary text emphasis color. Default: #052c65
    /// </summary>
    public string? PrimaryTextEmphasis { get; set; }

    /// <summary>
    /// Secondary text emphasis color. Default: #2b2f32
    /// </summary>
    public string? SecondaryTextEmphasis { get; set; }

    /// <summary>
    /// Success text emphasis color. Default: #0a3622
    /// </summary>
    public string? SuccessTextEmphasis { get; set; }

    /// <summary>
    /// Info text emphasis color. Default: #055160
    /// </summary>
    public string? InfoTextEmphasis { get; set; }

    /// <summary>
    /// Warning text emphasis color. Default: #664d03
    /// </summary>
    public string? WarningTextEmphasis { get; set; }

    /// <summary>
    /// Danger text emphasis color. Default: #58151c
    /// </summary>
    public string? DangerTextEmphasis { get; set; }

    /// <summary>
    /// Light text emphasis color. Default: #495057
    /// </summary>
    public string? LightTextEmphasis { get; set; }

    /// <summary>
    /// Dark text emphasis color. Default: #495057
    /// </summary>
    public string? DarkTextEmphasis { get; set; }

    // Background Subtle Colors
    /// <summary>
    /// Primary background subtle color. Default: #cfe2ff
    /// </summary>
    public string? PrimaryBgSubtle { get; set; }

    /// <summary>
    /// Secondary background subtle color. Default: #e2e3e5
    /// </summary>
    public string? SecondaryBgSubtle { get; set; }

    /// <summary>
    /// Success background subtle color. Default: #d1e7dd
    /// </summary>
    public string? SuccessBgSubtle { get; set; }

    /// <summary>
    /// Info background subtle color. Default: #cff4fc
    /// </summary>
    public string? InfoBgSubtle { get; set; }

    /// <summary>
    /// Warning background subtle color. Default: #fff3cd
    /// </summary>
    public string? WarningBgSubtle { get; set; }

    /// <summary>
    /// Danger background subtle color. Default: #f8d7da
    /// </summary>
    public string? DangerBgSubtle { get; set; }

    /// <summary>
    /// Light background subtle color. Default: #fcfcfd
    /// </summary>
    public string? LightBgSubtle { get; set; }

    /// <summary>
    /// Dark background subtle color. Default: #ced4da
    /// </summary>
    public string? DarkBgSubtle { get; set; }

    // Border Subtle Colors
    /// <summary>
    /// Primary border subtle color. Default: #9ec5fe
    /// </summary>
    public string? PrimaryBorderSubtle { get; set; }

    /// <summary>
    /// Secondary border subtle color. Default: #c4c8cb
    /// </summary>
    public string? SecondaryBorderSubtle { get; set; }

    /// <summary>
    /// Success border subtle color. Default: #a3cfbb
    /// </summary>
    public string? SuccessBorderSubtle { get; set; }

    /// <summary>
    /// Info border subtle color. Default: #9eeaf9
    /// </summary>
    public string? InfoBorderSubtle { get; set; }

    /// <summary>
    /// Warning border subtle color. Default: #ffe69c
    /// </summary>
    public string? WarningBorderSubtle { get; set; }

    /// <summary>
    /// Danger border subtle color. Default: #f1aeb5
    /// </summary>
    public string? DangerBorderSubtle { get; set; }

    /// <summary>
    /// Light border subtle color. Default: #e9ecef
    /// </summary>
    public string? LightBorderSubtle { get; set; }

    /// <summary>
    /// Dark border subtle color. Default: #adb5bd
    /// </summary>
    public string? DarkBorderSubtle { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (Blue.HasContent())
            yield return ("--bs-blue", Blue);
        if (Indigo.HasContent())
            yield return ("--bs-indigo", Indigo);
        if (Purple.HasContent())
            yield return ("--bs-purple", Purple);
        if (Pink.HasContent())
            yield return ("--bs-pink", Pink);
        if (Red.HasContent())
            yield return ("--bs-red", Red);
        if (Orange.HasContent())
            yield return ("--bs-orange", Orange);
        if (Yellow.HasContent())
            yield return ("--bs-yellow", Yellow);
        if (Green.HasContent())
            yield return ("--bs-green", Green);
        if (Teal.HasContent())
            yield return ("--bs-teal", Teal);
        if (Cyan.HasContent())
            yield return ("--bs-cyan", Cyan);
        if (Black.HasContent())
            yield return ("--bs-black", Black);
        if (White.HasContent())
            yield return ("--bs-white", White);
        if (Gray.HasContent())
            yield return ("--bs-gray", Gray);
        if (GrayDark.HasContent())
            yield return ("--bs-gray-dark", GrayDark);
        if (Gray100.HasContent())
            yield return ("--bs-gray-100", Gray100);
        if (Gray200.HasContent())
            yield return ("--bs-gray-200", Gray200);
        if (Gray300.HasContent())
            yield return ("--bs-gray-300", Gray300);
        if (Gray400.HasContent())
            yield return ("--bs-gray-400", Gray400);
        if (Gray500.HasContent())
            yield return ("--bs-gray-500", Gray500);
        if (Gray600.HasContent())
            yield return ("--bs-gray-600", Gray600);
        if (Gray700.HasContent())
            yield return ("--bs-gray-700", Gray700);
        if (Gray800.HasContent())
            yield return ("--bs-gray-800", Gray800);
        if (Gray900.HasContent())
            yield return ("--bs-gray-900", Gray900);
        if (Primary.HasContent())
            yield return ("--bs-primary", Primary);
        if (Secondary.HasContent())
            yield return ("--bs-secondary", Secondary);
        if (Success.HasContent())
            yield return ("--bs-success", Success);
        if (Info.HasContent())
            yield return ("--bs-info", Info);
        if (Warning.HasContent())
            yield return ("--bs-warning", Warning);
        if (Danger.HasContent())
            yield return ("--bs-danger", Danger);
        if (Light.HasContent())
            yield return ("--bs-light", Light);
        if (Dark.HasContent())
            yield return ("--bs-dark", Dark);
        if (PrimaryRgb.HasContent())
            yield return ("--bs-primary-rgb", PrimaryRgb);
        if (SecondaryRgb.HasContent())
            yield return ("--bs-secondary-rgb", SecondaryRgb);
        if (SuccessRgb.HasContent())
            yield return ("--bs-success-rgb", SuccessRgb);
        if (InfoRgb.HasContent())
            yield return ("--bs-info-rgb", InfoRgb);
        if (WarningRgb.HasContent())
            yield return ("--bs-warning-rgb", WarningRgb);
        if (DangerRgb.HasContent())
            yield return ("--bs-danger-rgb", DangerRgb);
        if (LightRgb.HasContent())
            yield return ("--bs-light-rgb", LightRgb);
        if (DarkRgb.HasContent())
            yield return ("--bs-dark-rgb", DarkRgb);
        if (WhiteRgb.HasContent())
            yield return ("--bs-white-rgb", WhiteRgb);
        if (BlackRgb.HasContent())
            yield return ("--bs-black-rgb", BlackRgb);
        if (PrimaryTextEmphasis.HasContent())
            yield return ("--bs-primary-text-emphasis", PrimaryTextEmphasis);
        if (SecondaryTextEmphasis.HasContent())
            yield return ("--bs-secondary-text-emphasis", SecondaryTextEmphasis);
        if (SuccessTextEmphasis.HasContent())
            yield return ("--bs-success-text-emphasis", SuccessTextEmphasis);
        if (InfoTextEmphasis.HasContent())
            yield return ("--bs-info-text-emphasis", InfoTextEmphasis);
        if (WarningTextEmphasis.HasContent())
            yield return ("--bs-warning-text-emphasis", WarningTextEmphasis);
        if (DangerTextEmphasis.HasContent())
            yield return ("--bs-danger-text-emphasis", DangerTextEmphasis);
        if (LightTextEmphasis.HasContent())
            yield return ("--bs-light-text-emphasis", LightTextEmphasis);
        if (DarkTextEmphasis.HasContent())
            yield return ("--bs-dark-text-emphasis", DarkTextEmphasis);
        if (PrimaryBgSubtle.HasContent())
            yield return ("--bs-primary-bg-subtle", PrimaryBgSubtle);
        if (SecondaryBgSubtle.HasContent())
            yield return ("--bs-secondary-bg-subtle", SecondaryBgSubtle);
        if (SuccessBgSubtle.HasContent())
            yield return ("--bs-success-bg-subtle", SuccessBgSubtle);
        if (InfoBgSubtle.HasContent())
            yield return ("--bs-info-bg-subtle", InfoBgSubtle);
        if (WarningBgSubtle.HasContent())
            yield return ("--bs-warning-bg-subtle", WarningBgSubtle);
        if (DangerBgSubtle.HasContent())
            yield return ("--bs-danger-bg-subtle", DangerBgSubtle);
        if (LightBgSubtle.HasContent())
            yield return ("--bs-light-bg-subtle", LightBgSubtle);
        if (DarkBgSubtle.HasContent())
            yield return ("--bs-dark-bg-subtle", DarkBgSubtle);
        if (PrimaryBorderSubtle.HasContent())
            yield return ("--bs-primary-border-subtle", PrimaryBorderSubtle);
        if (SecondaryBorderSubtle.HasContent())
            yield return ("--bs-secondary-border-subtle", SecondaryBorderSubtle);
        if (SuccessBorderSubtle.HasContent())
            yield return ("--bs-success-border-subtle", SuccessBorderSubtle);
        if (InfoBorderSubtle.HasContent())
            yield return ("--bs-info-border-subtle", InfoBorderSubtle);
        if (WarningBorderSubtle.HasContent())
            yield return ("--bs-warning-border-subtle", WarningBorderSubtle);
        if (DangerBorderSubtle.HasContent())
            yield return ("--bs-danger-border-subtle", DangerBorderSubtle);
        if (LightBorderSubtle.HasContent())
            yield return ("--bs-light-border-subtle", LightBorderSubtle);
        if (DarkBorderSubtle.HasContent())
            yield return ("--bs-dark-border-subtle", DarkBorderSubtle);
    }
}
