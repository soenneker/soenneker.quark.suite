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
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="useCdn">The use cdn.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(bool useCdn = true, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the create operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="options">The options.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Create(string id, FloatingWindowOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets callbacks.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="dotNetRef">The dot net ref.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SetCallbacks(string id, DotNetObjectReference<FloatingWindow> dotNetRef, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the destroy operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the show operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Show(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the hide operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Hide(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the toggle operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Toggle(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the close operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Close(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets position.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<(int x, int y)> GetPosition(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets position.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SetPosition(string id, int x, int y, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets size.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<FloatingWindowSize> GetSize(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Sets size.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask SetSize(string id, int width, int height, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the bring to front operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask BringToFront(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets viewport size.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<FloatingWindowSize> GetViewportSize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the center in viewport operation.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask CenterInViewport(string id, CancellationToken cancellationToken = default);
}
