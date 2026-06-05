using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Contains reorder metadata emitted by SortableJS.
/// </summary>
public sealed class SortableReorderEventArgs
{
    /// <summary>
    /// Gets or sets old index.
    /// </summary>
    public int OldIndex { get; set; } = -1;

    /// <summary>
    /// Gets or sets new index.
    /// </summary>
    public int NewIndex { get; set; } = -1;

    /// <summary>
    /// Gets or sets old draggable index.
    /// </summary>
    public int OldDraggableIndex { get; set; } = -1;

    /// <summary>
    /// Gets or sets new draggable index.
    /// </summary>
    public int NewDraggableIndex { get; set; } = -1;

    /// <summary>
    /// Gets or sets item id.
    /// </summary>
    public string? ItemId { get; set; }

    /// <summary>
    /// Gets or sets from list id.
    /// </summary>
    public string? FromListId { get; set; }

    /// <summary>
    /// Gets or sets to list id.
    /// </summary>
    public string? ToListId { get; set; }

    /// <summary>
    /// Item ids in the source list after the drop.
    /// </summary>
    public List<string> FromItemIds { get; set; } = [];

    /// <summary>
    /// Item ids in the target list after the drop.
    /// </summary>
    public List<string> ToItemIds { get; set; } = [];
}
