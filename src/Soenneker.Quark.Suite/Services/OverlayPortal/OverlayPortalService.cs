using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Soenneker.Quark;

/// <inheritdoc cref="IOverlayPortalService"/>
public sealed class OverlayPortalService : IOverlayPortalService
{
    private readonly Dictionary<string, OverlayPortalEntry> _entries = new(StringComparer.Ordinal);
    private int _nextSortKey;

    public event Action? Changed;

    public void Register(string id, RenderFragment fragment)
    {
        if (string.IsNullOrEmpty(id))
            return;

        if (_entries.TryGetValue(id, out var existing))
        {
            existing.Fragment = fragment;
        }
        else
        {
            _entries[id] = new OverlayPortalEntry(id, fragment, _nextSortKey++);
        }

        Changed?.Invoke();
    }

    public void Unregister(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;

        if (_entries.Remove(id))
            Changed?.Invoke();
    }

    public IReadOnlyList<OverlayPortalEntry> GetOrderedEntries()
    {
        return _entries.Values.OrderBy(e => e.SortKey).ToList();
    }
}
