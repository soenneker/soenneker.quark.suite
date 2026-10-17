using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
namespace Soenneker.Quark;
/// <summary>A sortable Kanban column with optional measured card virtualization.</summary>
public interface IKanbanColumnContent : IElement
{
    /// <summary>Reports disabled items, including unmounted ones, for dragging and keyboard reordering.</summary>
    Func<int, bool>? ItemDisabled { get; set; }

    /// <summary>Enables windowed cards. Handle OnReorder using full collection indexes/IDs to update both columns after a cross-column move.</summary>
    bool Virtualize { get; set; }
    /// <summary>Stable card IDs, in column order.</summary>
    IReadOnlyList<string> ItemKeys { get; set; }
    /// <summary>Renders card content at the full column index. May contain a KanbanItem and drag handle.</summary>
    RenderFragment<int>? ItemTemplate { get; set; }
}
