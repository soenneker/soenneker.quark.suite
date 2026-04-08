using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop contract for resizable panel drag tracking.
/// </summary>
public interface IResizableInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures resizable JS resources are loaded and ready.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts document-level drag tracking for the supplied handle.
    /// </summary>
    ValueTask StartDrag(ElementReference group, long pointerId, double clientX, double clientY, string orientation,
        DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops any active drag tracking.
    /// </summary>
    ValueTask StopDrag(CancellationToken cancellationToken = default);
}
