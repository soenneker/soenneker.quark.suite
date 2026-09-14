namespace Soenneker.Quark;

/// <summary>
/// Styling for the parts of a PageHeader component.
/// </summary>
public sealed class PageHeaderAppearance
{
    /// <summary>Presets for page header copy preset.</summary>
    public QuarkPresetToken PageHeaderCopyPreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for page header eyebrow preset.</summary>
    public QuarkPresetToken PageHeaderEyebrowPreset { get; init; } = CompositionPresets.Muted;

    /// <summary>Presets for page header title preset.</summary>
    public QuarkPresetToken PageHeaderTitlePreset { get; init; } = CompositionPresets.Title;

    /// <summary>Presets for page header description preset.</summary>
    public QuarkPresetToken PageHeaderDescriptionPreset { get; init; } = CompositionPresets.Muted;

    /// <summary>Presets for page header actions preset.</summary>
    public QuarkPresetToken PageHeaderActionsPreset { get; init; } = CompositionPresets.Actions;

    /// <summary>Presets for page header inline actions preset.</summary>
    public QuarkPresetToken PageHeaderInlineActionsPreset { get; init; } = CompositionPresets.Actions;

    /// <summary>Presets for icon action button preset.</summary>
    public QuarkPresetToken IconActionButtonPreset { get; init; } = CompositionPresets.Actions;

    /// <summary>Presets for page header actions menu preset.</summary>
    public QuarkPresetToken PageHeaderActionsMenuPreset { get; init; } = CompositionPresets.Actions;

    /// <summary>Presets for page header root preset.</summary>
    public QuarkPresetToken PageHeaderRootPreset { get; init; } = CompositionPresets.Header;

    /// <summary>Presets for page header actions only root preset.</summary>
    public QuarkPresetToken PageHeaderActionsOnlyRootPreset { get; init; } = CompositionPresets.Actions;
}
