namespace Soenneker.Quark;

/// <summary>
/// Styling for the parts of a FormSection component.
/// </summary>
public sealed class FormSectionAppearance
{
    /// <summary>Presets for section header preset.</summary>
    public QuarkPresetToken SectionHeaderPreset { get; init; } = CompositionPresets.Header;

    /// <summary>Presets for section description preset.</summary>
    public QuarkPresetToken SectionDescriptionPreset { get; init; } = CompositionPresets.Muted;

    /// <summary>Presets for section header actions preset.</summary>
    public QuarkPresetToken SectionHeaderActionsPreset { get; init; } = CompositionPresets.Actions;

    /// <summary>Presets for section body preset.</summary>
    public QuarkPresetToken SectionBodyPreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for section title preset.</summary>
    public QuarkPresetToken SectionTitlePreset { get; init; } = CompositionPresets.Title;

    /// <summary>Presets for section panel preset.</summary>
    public QuarkPresetToken SectionPanelPreset { get; init; } = CompositionPresets.Panel;
}
