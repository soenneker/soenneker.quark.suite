using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>A measured, variable-height window over stable item keys. Data loading and retention belong to the caller.</summary>
public interface IVirtualItems : IElement
{
    /// <summary>Stable keys of disabled sortable items, including unmounted rows.</summary>
    IReadOnlyList<string> DisabledKeys { get; set; }
    /// <summary>Adds sortable row metadata for a SortableList owner.</summary>
    bool Sortable { get; set; }
    /// <summary>Logical list ID for sortable integration.</summary>
    string? ListId { get; set; }
    /// <summary>Enables windowed rendering. When false, renders all items.</summary>
    bool Enabled { get; set; }
    /// <summary>Stable, unique keys in display order. Replace the collection when data changes.</summary>
    IReadOnlyList<string> ItemKeys { get; set; }
    /// <summary>Renders an item by its full collection index; in table mode, renders cells inside the supplied row.</summary>
    RenderFragment<int>? ItemTemplate { get; set; }
    /// <summary>Uses the nearest scrolling ancestor instead of creating a scroll surface.</summary>
    bool ParentScroll { get; set; }
    /// <summary>Renders a tbody with tr spacers and rows. Requires a constrained scrolling ancestor.</summary>
    bool TableBody { get; set; }
    /// <summary>Number of columns spanned by table spacers.</summary>
    int VirtualColumnCount { get; set; }
    /// <summary>Follows appended items until the reader scrolls away from the bottom.</summary>
    bool FollowEnd { get; set; }
    /// <summary>Whether more items can be prepended.</summary>
    bool HasBefore { get; set; }
    /// <summary>Whether more items can be appended.</summary>
    bool HasAfter { get; set; }
    /// <summary>Loads an earlier page when scrolling near the beginning.</summary>
    EventCallback LoadBefore { get; set; }
    /// <summary>Loads a later page when scrolling near the end.</summary>
    EventCallback LoadAfter { get; set; }
    /// <summary>Reports whether the reader is following the end.</summary>
    EventCallback<bool> AtEndChanged { get; set; }
    /// <summary>Scrolls an index into view and optionally focuses a matching control once it mounts.</summary>
    ValueTask ScrollToIndex(int index, string? focusSelector = null);
    /// <summary>Resumes following the end of the collection.</summary>
    ValueTask JumpToEnd();
    /// <summary>Receives a measured window with an optional separately pinned focused row.</summary>
    Task SetVirtualWindow(int start, int count, double top, double bottom, bool paused, int pinned, double before, double after);
    /// <summary>Receives the measured window from browser interop.</summary>
    Task SetWindow(int start, int count, double top, double bottom, bool paused);
    /// <summary>Invokes the earlier-page loader, serializing requests.</summary>
    Task RequestOlder();
    /// <summary>Invokes the later-page loader, serializing requests.</summary>
    Task RequestAfter();
}
