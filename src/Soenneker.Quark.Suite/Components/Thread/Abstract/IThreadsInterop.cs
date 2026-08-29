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
    /// Initializes the Threads so it is ready for use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Threads is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes thread.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="initial">Initial for the initialize thread operation.</param>
    /// <param name="resizeBehavior">Resize Behavior for the initialize thread operation.</param>
    /// <param name="stickToBottom">Whether stick to bottom.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Threads is ready for use.</returns>
    ValueTask InitializeThread(ElementReference element, DotNetObjectReference<Thread> callbackReference, string initial, string resizeBehavior,
        bool stickToBottom, CancellationToken cancellationToken = default);

    /// <summary>
    /// Scrolls to Bottom.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="behavior">Behavior for the scroll to bottom operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the scroll to bottom operation is complete.</returns>
    ValueTask ScrollToBottom(ElementReference element, string behavior, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the resources held by the Threads.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the destroy operation is complete.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
