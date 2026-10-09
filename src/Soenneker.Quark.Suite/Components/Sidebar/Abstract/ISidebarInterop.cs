using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for sidebar.
/// </summary>
public interface ISidebarInterop : IAsyncDisposable
{
    /// <summary>
    /// Registers mobile detection and the keyboard shortcut for the callback target.
    /// </summary>
    ValueTask InitializeSidebar(DotNetObjectReference<SidebarProvider> componentRef, string shortcutKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the saved sidebar state, or null when no valid state is stored.
    /// </summary>
    ValueTask<bool?> GetSidebarState(string cookieKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Persists the sidebar state in a cookie.
    /// </summary>
    ValueTask SaveSidebarState(string cookieKey, bool value, CancellationToken cancellationToken = default);

    /// <summary>Registers pointer and keyboard resizing for a sidebar handle. Widths are in pixels.</summary>
    ValueTask RegisterResizeHandle(ElementReference handle, DotNetObjectReference<SidebarResizeHandle> componentRef, double minWidth, double maxWidth, bool rightSide, CancellationToken cancellationToken = default);

    /// <summary>Removes a handle's listeners and cancels any active resize.</summary>
    ValueTask UnregisterResizeHandle(ElementReference handle, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes sidebar event handlers and releases the callback target.
    /// </summary>
    ValueTask Cleanup(CancellationToken cancellationToken = default);
}
