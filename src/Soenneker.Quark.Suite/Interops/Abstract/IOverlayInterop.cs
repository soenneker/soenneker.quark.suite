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
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Overlay is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Activates focus management and optional scroll locking for an overlay.
    /// </summary>
    /// <param name="overlayId">Identifier of the overlay to target.</param>
    /// <param name="container">Element that will contain the rendered component.</param>
    /// <param name="trapFocus">Whether trap focus.</param>
    /// <param name="lockScroll">Whether lock scroll.</param>
    /// <param name="initialFocusSelector">Initial Focus Selector for the activate operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the activate operation is complete.</returns>
    ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true, string? initialFocusSelector = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Activates scroll locking for an overlay before its focus container is available.
    /// </summary>
    /// <param name="overlayId">Identifier of the overlay to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the activate scroll lock operation is complete.</returns>
    ValueTask ActivateScrollLock(string overlayId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates focus management and optional scroll locking for an overlay.
    /// </summary>
    /// <param name="overlayId">Identifier of the overlay to target.</param>
    /// <param name="unlockScroll">Whether unlock scroll.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the deactivate operation is complete.</returns>
    ValueTask Deactivate(string overlayId, bool unlockScroll = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases any remaining document-level overlay scroll locks.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the release scroll locks operation is complete.</returns>
    ValueTask ReleaseScrollLocks(CancellationToken cancellationToken = default);
}
