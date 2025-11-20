using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Bootstrap's form CSS variables
/// </summary>
public sealed class BootstrapFormsCssVariables : IBootstrapCssVariableGroup
{
    /// <summary>
    /// Form valid color. Default: var(--bs-success)
    /// </summary>
    public string? FormValidColor { get; set; }

    /// <summary>
    /// Form valid border color. Default: var(--bs-success)
    /// </summary>
    public string? FormValidBorderColor { get; set; }

    /// <summary>
    /// Form invalid color. Default: var(--bs-danger)
    /// </summary>
    public string? FormInvalidColor { get; set; }

    /// <summary>
    /// Form invalid border color. Default: var(--bs-danger)
    /// </summary>
    public string? FormInvalidBorderColor { get; set; }

    /// <summary>
    /// Form valid color alternative. Default: #75b798
    /// </summary>
    public string? FormValidColorAlt { get; set; }

    /// <summary>
    /// Form invalid color alternative. Default: #ea868f
    /// </summary>
    public string? FormInvalidColorAlt { get; set; }

    /// <summary>
    /// Form valid background color. Default: var(--bs-success-bg-subtle)
    /// </summary>
    public string? FormValidBg { get; set; }

    /// <summary>
    /// Form invalid background color. Default: var(--bs-danger-bg-subtle)
    /// </summary>
    public string? FormInvalidBg { get; set; }

    /// <summary>
    /// Form valid border color alternative. Default: var(--bs-success-border-subtle)
    /// </summary>
    public string? FormValidBorderColorAlt { get; set; }

    /// <summary>
    /// Form invalid border color alternative. Default: var(--bs-danger-border-subtle)
    /// </summary>
    public string? FormInvalidBorderColorAlt { get; set; }

    /// <summary>
    /// Form valid feedback color. Default: var(--bs-form-valid-color)
    /// </summary>
    public string? FormValidFeedbackColor { get; set; }

    /// <summary>
    /// Form invalid feedback color. Default: var(--bs-form-invalid-color)
    /// </summary>
    public string? FormInvalidFeedbackColor { get; set; }

    /// <summary>
    /// Form valid feedback background color. Default: var(--bs-form-valid-bg)
    /// </summary>
    public string? FormValidFeedbackBg { get; set; }

    /// <summary>
    /// Form invalid feedback background color. Default: var(--bs-form-invalid-bg)
    /// </summary>
    public string? FormInvalidFeedbackBg { get; set; }

    /// <summary>
    /// Form valid feedback border color. Default: var(--bs-form-valid-border-color)
    /// </summary>
    public string? FormValidFeedbackBorderColor { get; set; }

    /// <summary>
    /// Form invalid feedback border color. Default: var(--bs-form-invalid-border-color)
    /// </summary>
    public string? FormInvalidFeedbackBorderColor { get; set; }

    /// <summary>
    /// Form valid feedback border radius. Default: var(--bs-border-radius)
    /// </summary>
    public string? FormValidFeedbackBorderRadius { get; set; }

    /// <summary>
    /// Form invalid feedback border radius. Default: var(--bs-border-radius)
    /// </summary>
    public string? FormInvalidFeedbackBorderRadius { get; set; }

    /// <summary>
    /// Form valid feedback padding Y. Default: 0.25rem
    /// </summary>
    public string? FormValidFeedbackPaddingY { get; set; }

    /// <summary>
    /// Form invalid feedback padding Y. Default: 0.25rem
    /// </summary>
    public string? FormInvalidFeedbackPaddingY { get; set; }

    /// <summary>
    /// Form valid feedback padding X. Default: 0.5rem
    /// </summary>
    public string? FormValidFeedbackPaddingX { get; set; }

    /// <summary>
    /// Form invalid feedback padding X. Default: 0.5rem
    /// </summary>
    public string? FormInvalidFeedbackPaddingX { get; set; }

    /// <summary>
    /// Form valid feedback font size. Default: var(--bs-font-size-sm)
    /// </summary>
    public string? FormValidFeedbackFontSize { get; set; }

    /// <summary>
    /// Form invalid feedback font size. Default: var(--bs-font-size-sm)
    /// </summary>
    public string? FormInvalidFeedbackFontSize { get; set; }

    public string GetSelector()
    {
        return ":root";
    }

    public IEnumerable<(string CssPropertyName, string Value)> GetCssVariables()
    {
        if (FormValidColor.HasContent())
            yield return ("--bs-form-valid-color", FormValidColor);
        if (FormValidBorderColor.HasContent())
            yield return ("--bs-form-valid-border-color", FormValidBorderColor);
        if (FormInvalidColor.HasContent())
            yield return ("--bs-form-invalid-color", FormInvalidColor);
        if (FormInvalidBorderColor.HasContent())
            yield return ("--bs-form-invalid-border-color", FormInvalidBorderColor);
        if (FormValidColorAlt.HasContent())
            yield return ("--bs-form-valid-color-alt", FormValidColorAlt);
        if (FormInvalidColorAlt.HasContent())
            yield return ("--bs-form-invalid-color-alt", FormInvalidColorAlt);
        if (FormValidBg.HasContent())
            yield return ("--bs-form-valid-bg", FormValidBg);
        if (FormInvalidBg.HasContent())
            yield return ("--bs-form-invalid-bg", FormInvalidBg);
        if (FormValidBorderColorAlt.HasContent())
            yield return ("--bs-form-valid-border-color-alt", FormValidBorderColorAlt);
        if (FormInvalidBorderColorAlt.HasContent())
            yield return ("--bs-form-invalid-border-color-alt", FormInvalidBorderColorAlt);
        if (FormValidFeedbackColor.HasContent())
            yield return ("--bs-form-valid-feedback-color", FormValidFeedbackColor);
        if (FormInvalidFeedbackColor.HasContent())
            yield return ("--bs-form-invalid-feedback-color", FormInvalidFeedbackColor);
        if (FormValidFeedbackBg.HasContent())
            yield return ("--bs-form-valid-feedback-bg", FormValidFeedbackBg);
        if (FormInvalidFeedbackBg.HasContent())
            yield return ("--bs-form-invalid-feedback-bg", FormInvalidFeedbackBg);
        if (FormValidFeedbackBorderColor.HasContent())
            yield return ("--bs-form-valid-feedback-border-color", FormValidFeedbackBorderColor);
        if (FormInvalidFeedbackBorderColor.HasContent())
            yield return ("--bs-form-invalid-feedback-border-color", FormInvalidFeedbackBorderColor);
        if (FormValidFeedbackBorderRadius.HasContent())
            yield return ("--bs-form-valid-feedback-border-radius", FormValidFeedbackBorderRadius);
        if (FormInvalidFeedbackBorderRadius.HasContent())
            yield return ("--bs-form-invalid-feedback-border-radius", FormInvalidFeedbackBorderRadius);
        if (FormValidFeedbackPaddingY.HasContent())
            yield return ("--bs-form-valid-feedback-padding-y", FormValidFeedbackPaddingY);
        if (FormInvalidFeedbackPaddingY.HasContent())
            yield return ("--bs-form-invalid-feedback-padding-y", FormInvalidFeedbackPaddingY);
        if (FormValidFeedbackPaddingX.HasContent())
            yield return ("--bs-form-valid-feedback-padding-x", FormValidFeedbackPaddingX);
        if (FormInvalidFeedbackPaddingX.HasContent())
            yield return ("--bs-form-invalid-feedback-padding-x", FormInvalidFeedbackPaddingX);
        if (FormValidFeedbackFontSize.HasContent())
            yield return ("--bs-form-valid-feedback-font-size", FormValidFeedbackFontSize);
        if (FormInvalidFeedbackFontSize.HasContent())
            yield return ("--bs-form-invalid-feedback-font-size", FormInvalidFeedbackFontSize);
    }
}
