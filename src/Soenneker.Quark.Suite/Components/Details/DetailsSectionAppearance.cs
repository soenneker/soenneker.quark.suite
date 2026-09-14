namespace Soenneker.Quark;

/// <summary>
/// Styling for the parts of a DetailsSection component.
/// </summary>
public sealed class DetailsSectionAppearance
{
    /// <summary>Presets for info section title preset.</summary>
    public QuarkPresetToken InfoSectionTitlePreset { get; init; } = CompositionPresets.Title;

    /// <summary>Presets for info section grid preset.</summary>
    public QuarkPresetToken InfoSectionGridPreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for flat info section preset.</summary>
    public QuarkPresetToken FlatInfoSectionPreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for info section preset.</summary>
    public QuarkPresetToken InfoSectionPreset { get; init; } = CompositionPresets.Panel;
}
