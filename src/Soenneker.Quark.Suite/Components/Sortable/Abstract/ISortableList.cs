using System;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents a SortableJS-backed list container.
/// </summary>
public interface ISortableList : IElement
{
    /// <summary>Reports disabled items, including unmounted ones, for dragging and keyboard reordering.</summary>
    Func<int, bool>? ItemDisabled { get; set; }

    /// <summary>Enables a measured window. Requires OnReorder to update the backing collection; returned indexes and ID arrays cover the full collection.</summary>
    bool Virtualize { get; set; }
    /// <summary>Stable sortable item IDs in full display order.</summary>
    IReadOnlyList<string> ItemKeys { get; set; }
    /// <summary>Renders item contents at a full collection index. A sortable wrapper is supplied.</summary>
    RenderFragment<int>? ItemTemplate { get; set; }

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
