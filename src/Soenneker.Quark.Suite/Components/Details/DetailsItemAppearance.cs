namespace Soenneker.Quark;

/// <summary>
/// Styling for the parts of a DetailsItem component.
/// </summary>
public sealed class DetailsItemAppearance
{
    /// <summary>Presets for info item preset.</summary>
    public QuarkPresetToken InfoItemPreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for info item label preset.</summary>
    public QuarkPresetToken InfoItemLabelPreset { get; init; } = CompositionPresets.Muted;

    /// <summary>Presets for detail link preset.</summary>
    public QuarkPresetToken DetailLinkPreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for info item value preset.</summary>
    public QuarkPresetToken InfoItemValuePreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for info item code value preset.</summary>
    public QuarkPresetToken InfoItemCodeValuePreset { get; init; } = CompositionPresets.Code;

    /// <summary>Presets for info item muted value preset.</summary>
    public QuarkPresetToken InfoItemMutedValuePreset { get; init; } = CompositionPresets.Muted;
}
