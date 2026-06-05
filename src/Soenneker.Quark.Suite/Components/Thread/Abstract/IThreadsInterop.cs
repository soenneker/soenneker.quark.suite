using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop contract for Thread scroll behavior.
/// </summary>
public interface IThreadsInterop : IAsyncDisposable
{
    /// <summary>
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes thread.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="callbackReference">The callback reference.</param>
    /// <param name="initial">The initial.</param>
    /// <param name="resizeBehavior">The resize behavior.</param>
    /// <param name="stickToBottom">The stick to bottom.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask InitializeThread(ElementReference element, DotNetObjectReference<Thread> callbackReference, string initial, string resizeBehavior,
        bool stickToBottom, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the scroll to bottom operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="behavior">The behavior.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask ScrollToBottom(ElementReference element, string behavior, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the destroy operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
