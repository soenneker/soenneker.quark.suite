using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the event timeline component.
/// </summary>
public interface IEventTimeline
{
    /// <summary>
    /// Events to display. Blank labels are omitted and events are ordered by timestamp.
    /// </summary>
    IReadOnlyList<EventTimelineItem> Items { get; set; }

    /// <summary>
    /// Fallback accessible name when AriaLabel is not specified.
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Gets or sets aria label.
    /// </summary>
    string? AriaLabel { get; set; }

    /// <summary>
    /// Gets or sets empty text.
    /// </summary>
    string EmptyText { get; set; }

    /// <summary>
    /// Gets or sets test id.
    /// </summary>
    string? TestId { get; set; }

    /// <summary>
    /// Optional timestamp formatter. The default preserves the supplied offset.
    /// </summary>
    Func<DateTimeOffset, string>? FormatTimestamp { get; set; }

    /// <summary>
    /// Additional attributes applied to the root element.
    /// </summary>
    IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// Presets applied to the component parts.
    /// </summary>
    EventTimelineAppearance Appearance { get; set; }

    /// <summary>
    /// Initial layout. Auto fits dense timelines and scrolls shorter timelines. Changing this value resets the user-selected view.
    /// </summary>
    EventTimelineLayout Layout { get; set; }

    /// <summary>
    /// Event count above which Auto fits the track and view controls become available. Defaults to five.
    /// </summary>
    int DenseTimelineThreshold { get; set; }

    /// <summary>
    /// Whether dense timelines offer Fit all and Scroll buttons.
    /// </summary>
    bool ShowViewControls { get; set; }

    /// <summary>
    /// Optional prefix for item data-testid attributes. Each item key is appended.
    /// </summary>
    string? ItemTestIdPrefix { get; set; }
}
