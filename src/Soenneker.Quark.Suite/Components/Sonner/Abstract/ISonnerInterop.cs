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
    /// Initializes the Sonner so it is ready for use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Sonner is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers hotkey for the Sonner.
    /// </summary>
    /// <param name="section">Configuration section to read.</param>
    /// <param name="hotkey">hotkey to process.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the hotkey registration is complete.</returns>
    ValueTask RegisterHotkey(ElementReference section, IReadOnlyList<string>? hotkey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters hotkey for the Sonner.
    /// </summary>
    /// <param name="section">Configuration section to read.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the hotkey registration has been removed.</returns>
    ValueTask UnregisterHotkey(ElementReference section, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers swipe Handlers for the Sonner.
    /// </summary>
    /// <param name="section">Configuration section to read.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>true if registers swipe Handlers for the Sonner; otherwise, false.</returns>
    ValueTask<bool> RegisterSwipeHandlers(ElementReference section, DotNetObjectReference<Sonner> callbackReference, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters swipe Handlers for the Sonner.
    /// </summary>
    /// <param name="section">Configuration section to read.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the swipe handlers registration has been removed.</returns>
    ValueTask UnregisterSwipeHandlers(ElementReference section, CancellationToken cancellationToken = default);

    /// <summary>
    /// Measures toast Heights.
    /// </summary>
    /// <param name="section">Configuration section to read.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested dictionary.</returns>
    ValueTask<Dictionary<string, double>> MeasureToastHeights(ElementReference section, CancellationToken cancellationToken = default);
}
