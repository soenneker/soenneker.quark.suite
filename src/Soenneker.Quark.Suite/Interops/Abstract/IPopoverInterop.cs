using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for viewport-positioned popovers.
/// </summary>
public interface IPopoverInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures popover resources are loaded.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts tracking a popover against its trigger and keeps the content positioned.
    /// </summary>
    ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content, string side, string align, int sideOffset = 4,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts tracking a popover against its trigger, keeps the content positioned, and wires outside-click dismissal.
    /// </summary>
    ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content, DotNetObjectReference<PopoverOutsideCloseProxy> callbackReference,
        string side, string align, int sideOffset = 4,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops tracking the popover position.
    /// </summary>
    ValueTask StopObserving(string popoverId, CancellationToken cancellationToken = default);
}
