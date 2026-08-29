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
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Resizable is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers a handle element for synchronous browser-side drag activation.
    /// </summary>
    /// <param name="handle">Handle for the register handle operation.</param>
    /// <param name="group">Group to target.</param>
    /// <param name="orientation">Layout orientation to apply.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="handleIndex">Handle Index for the register handle operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the handle registration is complete.</returns>
    ValueTask RegisterHandle(ElementReference handle, ElementReference group, string orientation,
        DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, CancellationToken cancellationToken = default);

    /// <summary>
    /// Unregisters a previously-registered handle element.
    /// </summary>
    /// <param name="handle">Handle for the unregister handle operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the handle registration has been removed.</returns>
    ValueTask UnregisterHandle(ElementReference handle, CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts document-level drag tracking for the supplied handle.
    /// </summary>
    /// <param name="group">Group to target.</param>
    /// <param name="pointerId">Identifier of the pointer to target.</param>
    /// <param name="clientX">client X used to communicate with the external service.</param>
    /// <param name="clientY">client Y used to communicate with the external service.</param>
    /// <param name="orientation">Layout orientation to apply.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="handleIndex">Handle Index for the start drag operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after the Resizable has started.</returns>
    ValueTask StartDrag(ElementReference group, long pointerId, double clientX, double clientY, string orientation,
        DotNetObjectReference<ResizablePanelGroup> callbackReference, int handleIndex, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops any active drag tracking.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after the Resizable has stopped.</returns>
    ValueTask StopDrag(CancellationToken cancellationToken = default);
}
