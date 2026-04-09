using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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
    /// <param name="overlayId">Stable id for this overlay instance (stack ordering, focus trap, Escape routing).</param>
    /// <param name="container">Focus scope root (typically the modal content panel).</param>
    /// <param name="trapFocus">When <c>true</c>, Tab cycles within <paramref name="container"/>.</param>
    /// <param name="lockScroll">When <c>true</c>, body scroll is locked for this overlay.</param>
    /// <param name="initialFocusSelector">Optional CSS selector for the first focused element.</param>
    /// <param name="escapeInvoker">
    /// When set, Escape is handled in capture phase on the document so only the top overlay in the stack
    /// dismisses and the event does not bubble to parent overlays (Radix-style nested modal behavior).
    /// </param>
    /// <param name="cancellationToken">Cancellation token.</param>
    ValueTask Activate(string overlayId, ElementReference container, bool trapFocus = true, bool lockScroll = true, string? initialFocusSelector = null,
        DotNetObjectReference<OverlayElement>? escapeInvoker = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deactivates focus management and optional scroll locking for an overlay.
    /// </summary>
    ValueTask Deactivate(string overlayId, bool unlockScroll = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers document bubble-phase Escape handling so open tooltips can dismiss after modal overlay capture defers (nested dismiss order).
    /// </summary>
    ValueTask RegisterTooltipEscapeAsync(DotNetObjectReference<TooltipProvider> dotNetRef, CancellationToken cancellationToken = default);

    /// <summary>
    /// Clears the tooltip Escape dismiss target (e.g. when <see cref="TooltipProvider"/> disposes).
    /// </summary>
    ValueTask UnregisterTooltipEscapeAsync(CancellationToken cancellationToken = default);
}
