namespace Soenneker.Quark;

/// <summary>
/// Styling for the parts of a ChartCard component.
/// </summary>
public sealed class ChartCardAppearance
{
    /// <summary>Presets for chart card header preset.</summary>
    public QuarkPresetToken ChartCardHeaderPreset { get; init; } = CompositionPresets.Header;

    /// <summary>Presets for chart card title preset.</summary>
    public QuarkPresetToken ChartCardTitlePreset { get; init; } = CompositionPresets.Title;

    /// <summary>Presets for chart card value preset.</summary>
    public QuarkPresetToken ChartCardValuePreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for chart card subtitle preset.</summary>
    public QuarkPresetToken ChartCardSubtitlePreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for chart card actions preset.</summary>
    public QuarkPresetToken ChartCardActionsPreset { get; init; } = CompositionPresets.Actions;

    /// <summary>Presets for empty state preset.</summary>
    public QuarkPresetToken EmptyStatePreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for chart body preset.</summary>
    public QuarkPresetToken ChartBodyPreset { get; init; } = CompositionPresets.Stack;

    /// <summary>Presets for chart card preset.</summary>
    public QuarkPresetToken ChartCardPreset { get; init; } = CompositionPresets.Panel;
}
