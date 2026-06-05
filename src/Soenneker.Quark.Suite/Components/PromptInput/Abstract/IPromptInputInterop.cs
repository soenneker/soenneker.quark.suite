using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop contract for prompt input browser behavior.
/// </summary>
public interface IPromptInputInterop : IAsyncDisposable
{
    /// <summary>
    /// Executes the register textarea operation.
    /// </summary>
    /// <param name="textarea">The textarea.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask RegisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the unregister textarea operation.
    /// </summary>
    /// <param name="textarea">The textarea.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UnregisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the open file dialog by id operation.
    /// </summary>
    /// <param name="inputId">The input id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask OpenFileDialogById(string inputId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the register attachments by id operation.
    /// </summary>
    /// <param name="inputId">The input id.</param>
    /// <param name="callbackReference">The callback reference.</param>
    /// <param name="globalDrop">The global drop.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask RegisterAttachmentsById(string inputId, DotNetObjectReference<PromptInputActionAddAttachments> callbackReference, bool globalDrop,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the unregister attachments by id operation.
    /// </summary>
    /// <param name="inputId">The input id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask UnregisterAttachmentsById(string inputId, CancellationToken cancellationToken = default);
}
