using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for modal-like overlay behavior.
/// </summary>
public interface IOverlayInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures overlay resources are loaded.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Activates focus management and optional scroll locking for an overlay.
    /// </summary>
    ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true, string? initialFocusSelector = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates focus management and optional scroll locking for an overlay.
    /// </summary>
    ValueTask Deactivate(string overlayId, bool unlockScroll = true, CancellationToken cancellationToken = default);
}
