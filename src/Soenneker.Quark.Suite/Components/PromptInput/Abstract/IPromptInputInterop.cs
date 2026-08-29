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
    /// Registers textarea.
    /// </summary>
    /// <param name="textarea">Textarea for the register textarea operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the textarea registration is complete.</returns>
    ValueTask RegisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters textarea.
    /// </summary>
    /// <param name="textarea">Textarea for the unregister textarea operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the textarea registration has been removed.</returns>
    ValueTask UnregisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens file Dialog By ID for the Prompt Input.
    /// </summary>
    /// <param name="inputId">input ID to read or transform.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the open file dialog by id operation is complete.</returns>
    ValueTask OpenFileDialogById(string inputId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers attachments By ID.
    /// </summary>
    /// <param name="inputId">input ID to read or transform.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="globalDrop">Whether global drop.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the attachments by id registration is complete.</returns>
    ValueTask RegisterAttachmentsById(string inputId, DotNetObjectReference<PromptInputActionAddAttachments> callbackReference, bool globalDrop,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters attachments By ID for the Prompt Input.
    /// </summary>
    /// <param name="inputId">input ID to read or transform.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the attachments by id registration has been removed.</returns>
    ValueTask UnregisterAttachmentsById(string inputId, CancellationToken cancellationToken = default);
}
