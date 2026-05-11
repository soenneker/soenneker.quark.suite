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
    ValueTask RegisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default);

    ValueTask UnregisterTextarea(ElementReference textarea, CancellationToken cancellationToken = default);

    ValueTask OpenFileDialogById(string inputId, CancellationToken cancellationToken = default);

    ValueTask RegisterAttachmentsById(string inputId, DotNetObjectReference<PromptInputActionAddAttachments> callbackReference, bool globalDrop,
        CancellationToken cancellationToken = default);

    ValueTask UnregisterAttachmentsById(string inputId, CancellationToken cancellationToken = default);
}
