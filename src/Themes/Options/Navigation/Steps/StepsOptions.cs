using System.Collections.Generic;
using Soenneker.Extensions.String;

namespace Soenneker.Quark;

/// <summary>
/// Configuration options for steps component styling.
/// </summary>
public sealed class StepsOptions : ComponentOptions
{
    /// <summary>
    /// Initializes a new instance of the StepsOptions class.
    /// </summary>
    public StepsOptions()
    {
        Selector = ".q-steps";
    }

    /// <summary>
    /// Gets or sets the connector color between steps.
    /// </summary>
    public string? ConnectorColor { get; set; }
    /// <summary>
    /// Gets or sets the background color for active step markers.
    /// </summary>
    public string? MarkerActiveBg { get; set; }
    /// <summary>
    /// Gets or sets the border color for active step markers.
    /// </summary>
    public string? MarkerActiveBorder { get; set; }
    /// <summary>
    /// Gets or sets the text color for active step markers.
    /// </summary>
    public string? MarkerActiveColor { get; set; }
    /// <summary>
    /// Gets or sets the text color for active steps.
    /// </summary>
    public string? TextActive { get; set; }
    /// <summary>
    /// Gets or sets the success state color.
    /// </summary>
    public string? Success { get; set; }
    /// <summary>
    /// Gets or sets the background color for disabled steps.
    /// </summary>
    public string? DisabledBg { get; set; }
    /// <summary>
    /// Gets or sets the text color for disabled steps.
    /// </summary>
    public string? DisabledColor { get; set; }
    /// <summary>
    /// Gets or sets the background color for step content.
    /// </summary>
    public string? ContentBg { get; set; }
    /// <summary>
    /// Gets or sets the border color for step content.
    /// </summary>
    public string? ContentBorder { get; set; }
    /// <summary>
    /// Gets or sets the border radius for step content.
    /// </summary>
    public string? ContentRadius { get; set; }
    /// <summary>
    /// Gets or sets the box shadow for step content.
    /// </summary>
    public string? ContentShadow { get; set; }
    /// <summary>
    /// Gets or sets the focus outline style.
    /// </summary>
    public string? FocusOutline { get; set; }
    /// <summary>
    /// Gets or sets the text color for success step markers.
    /// </summary>
    public string? MarkerSuccessColor { get; set; }

    /// <summary>
    /// Gets or sets the styling options for active steps.
    /// </summary>
    public StepStateOptions? Active { get; set; }

    /// <summary>
    /// Gets or sets the styling options for non-active steps.
    /// </summary>
    public StepStateOptions? NonActive { get; set; }

    internal override IEnumerable<ComponentCssRule> GetCssRules()
    {
        var buffer = new List<ComponentCssRule>(32);

        IEnumerable<ComponentCssRule> baseRules = base.GetCssRules();
        buffer.AddRange(baseRules);

        if (Active != null)
        {
            CollectStepStateRules(buffer, ".steps a.active", Active);
        }
        else
        {
            CollectLegacyActiveRules(buffer);
        }

        if (NonActive != null)
        {
            CollectStepStateRules(buffer, ".steps a:not(.active)", NonActive);
        }

        CollectOtherRules(buffer);

        return buffer;
    }

    private void CollectLegacyActiveRules(List<ComponentCssRule> buffer)
    {
        if (MarkerActiveBg.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.active .step-marker", $"background-color: {MarkerActiveBg} !important"));
        }

        if (MarkerActiveBorder.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.active .step-marker", $"border-color: {MarkerActiveBorder} !important"));
        }

        if (MarkerActiveColor.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.active .step-marker", $"color: {MarkerActiveColor} !important"));
        }

        if (TextActive.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.active", $"color: {TextActive} !important"));
            buffer.Add(new ComponentCssRule(".steps a.active .step-description, .steps a.active .step-title", $"color: {TextActive} !important"));
        }
    }

    private void CollectOtherRules(List<ComponentCssRule> buffer)
    {
        if (ConnectorColor.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps li.step-item:not(:last-of-type)::after", $"background-color: {ConnectorColor} !important"));
        }

        if (Success.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.text-success", $"background-color: color-mix(in srgb, {Success} 10%, transparent) !important"));
            buffer.Add(new ComponentCssRule(".steps a.text-success", $"border-color: color-mix(in srgb, {Success} 35%, transparent) !important"));
            buffer.Add(new ComponentCssRule(".steps a.text-success", $"color: {Success} !important"));
            buffer.Add(new ComponentCssRule(".steps a.text-success .step-marker", $"background-color: {Success} !important"));
            buffer.Add(new ComponentCssRule(".steps a.text-success .step-marker", $"border-color: {Success} !important"));
        }

        if (MarkerSuccessColor.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.text-success .step-marker", $"color: {MarkerSuccessColor} !important"));
        }

        if (DisabledBg.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.disabled", $"background-color: {DisabledBg} !important"));
        }

        if (DisabledColor.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a.disabled", $"color: {DisabledColor} !important"));
        }

        if (ContentBg.HasContent())
        {
            buffer.Add(new ComponentCssRule(".tab-content", $"background-color: {ContentBg} !important"));
        }

        if (ContentBorder.HasContent())
        {
            buffer.Add(new ComponentCssRule(".tab-content", $"border-color: {ContentBorder} !important"));
        }

        if (ContentRadius.HasContent())
        {
            buffer.Add(new ComponentCssRule(".tab-content", $"border-radius: {ContentRadius} !important"));
        }

        if (ContentShadow.HasContent())
        {
            buffer.Add(new ComponentCssRule(".tab-content", $"box-shadow: {ContentShadow} !important"));
        }

        if (FocusOutline.HasContent())
        {
            buffer.Add(new ComponentCssRule(".steps a:focus", $"outline: 2px solid {FocusOutline} !important"));
        }
    }

    private static void CollectStepStateRules(List<ComponentCssRule> buffer, string selector, StepStateOptions options)
    {
        if (options.MarkerBackgroundColor.HasContent())
        {
            buffer.Add(new ComponentCssRule($"{selector} .step-marker", $"background-color: {options.MarkerBackgroundColor} !important"));
        }

        if (options.MarkerBorderColor.HasContent())
        {
            buffer.Add(new ComponentCssRule($"{selector} .step-marker", $"border-color: {options.MarkerBorderColor} !important"));
        }

        if (options.MarkerTextColor.HasContent())
        {
            buffer.Add(new ComponentCssRule($"{selector} .step-marker", $"color: {options.MarkerTextColor} !important"));
        }

        if (options.TextColor.HasContent())
        {
            buffer.Add(new ComponentCssRule(selector, $"color: {options.TextColor} !important"));
            buffer.Add(new ComponentCssRule($"{selector} .step-description, {selector} .step-title", $"color: {options.TextColor} !important"));
        }

        if (options.BackgroundColor.HasContent())
        {
            buffer.Add(new ComponentCssRule(selector, $"background-color: {options.BackgroundColor} !important"));
        }
    }
}

