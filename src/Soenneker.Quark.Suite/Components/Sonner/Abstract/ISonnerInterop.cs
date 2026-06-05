using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop for Sonner measurement and client-side host behavior helpers.
/// </summary>
public interface ISonnerInterop : IAsyncDisposable
{
    /// <summary>
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the register hotkey operation.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="hotkey">The hotkey.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask RegisterHotkey(ElementReference section, IReadOnlyList<string>? hotkey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the unregister hotkey operation.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UnregisterHotkey(ElementReference section, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the register swipe handlers operation.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="callbackReference">The callback reference.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<bool> RegisterSwipeHandlers(ElementReference section, DotNetObjectReference<Sonner> callbackReference, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the unregister swipe handlers operation.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UnregisterSwipeHandlers(ElementReference section, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the measure toast heights operation.
    /// </summary>
    /// <param name="section">The section.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<Dictionary<string, double>> MeasureToastHeights(ElementReference section, CancellationToken cancellationToken = default);
}
