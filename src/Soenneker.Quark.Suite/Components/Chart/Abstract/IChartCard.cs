using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Soenneker.Lucide.Enums.Icons;

namespace Soenneker.Quark;

/// <summary>
/// Defines the chart card component.
/// </summary>
public interface IChartCard
{
    /// <summary>
    /// Presets applied to the component parts.
    /// </summary>
    ChartCardAppearance Appearance { get; set; }

    /// <summary>
    /// Gets or sets error title.
    /// </summary>
    string ErrorTitle { get; set; }

    /// <summary>
    /// Gets or sets loading content.
    /// </summary>
    RenderFragment? LoadingContent { get; set; }

    /// <summary>
    /// Gets or sets error content.
    /// </summary>
    RenderFragment? ErrorContent { get; set; }

    /// <summary>
    /// Gets or sets empty content.
    /// </summary>
    RenderFragment? EmptyContent { get; set; }

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets subtitle.
    /// </summary>
    string? Subtitle { get; set; }

    /// <summary>
    /// Gets or sets metric value.
    /// </summary>
    string? MetricValue { get; set; }

    /// <summary>
    /// Gets or sets preset.
    /// </summary>
    QuarkPresetToken? Preset { get; set; }

    /// <summary>
    /// Whether the loading view replaces content. Loading takes precedence over error and empty views.
    /// </summary>
    bool IsLoading { get; set; }

    /// <summary>
    /// A nonempty message displays the error view when loading is complete.
    /// </summary>
    string? ErrorMessage { get; set; }

    /// <summary>
    /// Whether the empty view replaces content when neither loading nor an error applies.
    /// </summary>
    bool IsEmpty { get; set; }

    /// <summary>
    /// Gets or sets empty title.
    /// </summary>
    string EmptyTitle { get; set; }

    /// <summary>
    /// Gets or sets empty message.
    /// </summary>
    string? EmptyMessage { get; set; }

    /// <summary>
    /// Gets or sets header content.
    /// </summary>
    RenderFragment? HeaderContent { get; set; }

    /// <summary>
    /// Content rendered inside the component.
    /// </summary>
    RenderFragment? ChildContent { get; set; }
}
