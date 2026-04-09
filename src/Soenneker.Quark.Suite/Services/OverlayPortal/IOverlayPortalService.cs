using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Renders modal overlay markup at a layout-level host (Radix/shadcn-style portal) so fixed layers are not
/// clipped by ancestor overflow/transform and stack correctly with explicit z-order.
/// </summary>
public interface IOverlayPortalService
{
    /// <summary>
    /// Raised when registrations change so <see cref="QuarkOverlayPortalHost"/> can re-render.
    /// </summary>
    event Action? Changed;

    /// <summary>
    /// Registers or updates overlay content keyed by <paramref name="id"/> (stable per overlay instance).
    /// </summary>
    void Register(string id, RenderFragment fragment);

    /// <summary>
    /// Removes a registration when the overlay is closed or disposed.
    /// </summary>
    void Unregister(string id);

    /// <summary>
    /// Entries in registration order for z-stacking (later = higher).
    /// </summary>
    IReadOnlyList<OverlayPortalEntry> GetOrderedEntries();
}
