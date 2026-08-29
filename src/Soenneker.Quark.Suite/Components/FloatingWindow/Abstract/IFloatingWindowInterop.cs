using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// Defines the floating window interop contract.
/// </summary>
public interface IFloatingWindowInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the Floating Window so it is ready for use.
    /// </summary>
    /// <param name="useCdn">Whether cdn.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Floating Window is ready for use.</returns>
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a Floating Window instance from the supplied inputs.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="options">Options to configure for the Floating Window.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the create operation is complete.</returns>
    ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates the behavior options for an existing floating window.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="options">Options to configure for the Floating Window.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the options update is complete.</returns>
    ValueTask UpdateOptions(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets callbacks.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="dotNetRef">JavaScript-invokable reference to the .NET component instance.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the callbacks has been stored.</returns>
    ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the resources held by the Floating Window.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the destroy operation is complete.</returns>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Shows floating Window for the Floating Window.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the show operation is complete.</returns>
    ValueTask Show(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Hides floating Window for the Floating Window.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the hide operation is complete.</returns>
    ValueTask Hide(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles floating Window for the Floating Window.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the toggle operation is complete.</returns>
    ValueTask Toggle(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes floating Window for the Floating Window.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the close operation is complete.</returns>
    ValueTask Close(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets position.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested (int x, int y).</returns>
    ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets position.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="x">Operand passed to the accumulator function.</param>
    /// <param name="y">Vertical coordinate to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the position has been stored.</returns>
    ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets size.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested floating Window Size.</returns>
    ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets size.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="width">Width to apply.</param>
    /// <param name="height">Height to apply.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the size has been stored.</returns>
    ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default);

    /// <summary>
    /// Brings to Front.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the bring to front operation is complete.</returns>
    ValueTask BringToFront(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets viewport size.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested floating Window Size.</returns>
    ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Centers in Viewport.
    /// </summary>
    /// <param name="id">Identifier of the Floating Window instance or registration to target.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the center in viewport operation is complete.</returns>
    ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default);
}
