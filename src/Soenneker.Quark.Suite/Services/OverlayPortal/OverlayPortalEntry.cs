using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// One portaled overlay layer (e.g. dialog chrome) with a stable sort key for stacking.
/// </summary>
public sealed class OverlayPortalEntry
{
    public OverlayPortalEntry(string id, RenderFragment fragment, int sortKey)
    {
        Id = id;
        Fragment = fragment;
        SortKey = sortKey;
    }

    public string Id { get; }

    public RenderFragment Fragment { get; set; }

    public int SortKey { get; }
}
