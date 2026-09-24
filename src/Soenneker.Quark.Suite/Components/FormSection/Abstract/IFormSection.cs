using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Defines the form section component.
/// </summary>
public interface IFormSection
{
    /// <summary>
    /// Presets applied to the component parts.
    /// </summary>
    FormSectionAppearance Appearance { get; set; }

    /// <summary>
    /// Gets or sets title.
    /// </summary>
    string? Title { get; set; }

    /// <summary>
    /// Gets or sets description.
    /// </summary>
    string? Description { get; set; }

    /// <summary>
    /// Gets or sets body preset.
    /// </summary>
    QuarkPresetToken? BodyPreset { get; set; }

    /// <summary>
    /// Gets or sets title preset.
    /// </summary>
    QuarkPresetToken? TitlePreset { get; set; }

    /// <summary>
    /// Gets or sets preset.
    /// </summary>
    QuarkPresetToken? Preset { get; set; }

    /// <summary>
    /// Custom action content.
    /// </summary>
    RenderFragment? Actions { get; set; }

    /// <summary>
    /// Content rendered inside the component.
    /// </summary>
    RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional attributes applied to the root element.
    /// </summary>
    IReadOnlyDictionary<string, object>? AdditionalAttributes { get; set; }
}
