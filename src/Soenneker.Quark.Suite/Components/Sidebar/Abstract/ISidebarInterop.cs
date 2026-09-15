using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for sidebar.
/// </summary>
public interface ISidebarInterop : IAsyncDisposable
{

    /// <summary>
    /// Registers mobile detection and the keyboard shortcut for the callback target.
    /// </summary>
    ValueTask InitializeSidebar<T>(DotNetObjectReference<T> componentRef, string shortcutKey, CancellationToken cancellationToken = default) where T : class;

    /// <summary>
    /// Returns the saved sidebar state, or null when no valid state is stored.
    /// </summary>
    ValueTask<bool?> GetSidebarState(string cookieKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Persists the sidebar state in a cookie.
    /// </summary>
    ValueTask SaveSidebarState(string cookieKey, bool value, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes sidebar event handlers and releases the callback target.
    /// </summary>
    ValueTask Cleanup(CancellationToken cancellationToken = default);
}
