using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents a SortableJS-backed list container.
/// </summary>
public interface ISortableList : IElement
{
    /// <summary>
    /// Gets or sets a value indicating whether disabled.
    /// </summary>
    bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether sort.
    /// </summary>
    bool Sort { get; set; }

    /// <summary>
    /// Gets or sets animation ms.
    /// </summary>
    int AnimationMs { get; set; }

    /// <summary>
    /// Gets or sets group.
    /// </summary>
    string? Group { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether handle only.
    /// </summary>
    bool HandleOnly { get; set; }

    /// <summary>
    /// Gets or sets item selector.
    /// </summary>
    string ItemSelector { get; set; }

    /// <summary>
    /// Gets or sets handle selector.
    /// </summary>
    string? HandleSelector { get; set; }

    /// <summary>
    /// Gets or sets filter selector.
    /// </summary>
    string? FilterSelector { get; set; }

    /// <summary>
    /// Gets or sets on reorder.
    /// </summary>
    EventCallback<SortableReorderEventArgs> OnReorder { get; set; }
}
