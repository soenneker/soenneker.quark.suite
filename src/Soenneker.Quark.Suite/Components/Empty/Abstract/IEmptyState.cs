using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Defines the empty state component.
/// </summary>
public interface IEmptyState
{
    /// <summary>
    /// Gets or sets title.
    /// </summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    /// Gets or sets kind.
    /// </summary>
    EmptyStateKind Kind { get; set; }

    /// <summary>
    /// Gets or sets media.
    /// </summary>
    RenderFragment? Media { get; set; }

    /// <summary>
    /// Custom action content.
    /// </summary>
    RenderFragment? Actions { get; set; }

    /// <summary>
    /// Content rendered inside the component.
    /// </summary>
    RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets preset.
    /// </summary>
    QuarkPresetToken? Preset { get; set; }

    /// <summary>
    /// Gets or sets title preset.
    /// </summary>
    QuarkPresetToken? TitlePreset { get; set; }

    /// <summary>
    /// Gets or sets description preset.
    /// </summary>
    QuarkPresetToken? DescriptionPreset { get; set; }

    /// <summary>
    /// Additional attributes applied to the root element.
    /// </summary>
    IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
}
