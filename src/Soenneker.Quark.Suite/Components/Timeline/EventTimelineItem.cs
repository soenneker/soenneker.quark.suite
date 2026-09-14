using System;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>A timestamped event with presentation data independent of application models.</summary>
public sealed record EventTimelineItem
{
    /// <summary>Optional stable identifier for the event.</summary>
    public string Key { get; init; } = string.Empty;
    /// <summary>Visible event label.</summary>
    public string Label { get; init; } = string.Empty;
    /// <summary>Event timestamp including its offset.</summary>
    public DateTimeOffset When { get; init; }
    /// <summary>Event marker icon.</summary>
    public LucideIcon Icon { get; init; } = LucideIcon.Circle;
    /// <summary>Semantic tone, independent of the label text.</summary>
    public SemanticTone Tone { get; init; } = SemanticTone.Neutral;
}
