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
    ValueTask Initialize(CancellationToken cancellationToken = default);

    ValueTask InitializeThread(ElementReference element, DotNetObjectReference<Thread> callbackReference, string initial, string resizeBehavior,
        bool stickToBottom, CancellationToken cancellationToken = default);

    ValueTask ScrollToBottom(ElementReference element, string behavior, CancellationToken cancellationToken = default);

    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
