using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Contains reorder metadata emitted by SortableJS.
/// </summary>
public sealed class SortableReorderEventArgs
{
    public int OldIndex { get; set; } = -1;

    public int NewIndex { get; set; } = -1;

    public int OldDraggableIndex { get; set; } = -1;

    public int NewDraggableIndex { get; set; } = -1;

    public string? ItemId { get; set; }

    public string? FromListId { get; set; }

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
