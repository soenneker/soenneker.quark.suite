using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Defines the details item component.
/// </summary>
public interface IDetailsItem
{
    /// <summary>
    /// Presets applied to the component parts.
    /// </summary>
    DetailsItemAppearance Appearance { get; set; }

    /// <summary>
    /// Gets or sets label.
    /// </summary>
    string Label { get; set; }

    /// <summary>
    /// Gets or sets content.
    /// </summary>
    string Content { get; set; }

    /// <summary>
    /// Content rendered inside the component.
    /// </summary>
    RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Whether plain text values offer a copy action. Disabled by default.
    /// </summary>
    bool Copyable { get; set; }

    /// <summary>
    /// Gets or sets full width.
    /// </summary>
    bool FullWidth { get; set; }

    /// <summary>
    /// Gets or sets is code.
    /// </summary>
    bool IsCode { get; set; }

    /// <summary>
    /// Gets or sets is muted.
    /// </summary>
    bool IsMuted { get; set; }

    /// <summary>
    /// Gets or sets is link.
    /// </summary>
    bool IsLink { get; set; }

    /// <summary>
    /// Gets or sets link url.
    /// </summary>
    string? LinkUrl { get; set; }
}
