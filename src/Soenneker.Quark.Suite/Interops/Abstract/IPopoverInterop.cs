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
        string positionMode = "popper",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts tracking a popover against its trigger, keeps the content positioned, and wires outside-click dismissal.
    /// </summary>
    ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content, DotNetObjectReference<PopoverOutsideCloseProxy> callbackReference,
        string side, string align, int sideOffset = 4,
        string positionMode = "popper",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Same as <see cref="ObservePosition(string, ElementReference, ElementReference, DotNetObjectReference{PopoverOutsideCloseProxy}, string, string, int, string, CancellationToken)"/> but for select content dismissal.
    /// </summary>
    ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content, DotNetObjectReference<SelectOutsideCloseProxy> callbackReference,
        string side, string align, int sideOffset = 4,
        string positionMode = "popper",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Same as <see cref="ObservePosition(string, ElementReference, ElementReference, DotNetObjectReference{PopoverOutsideCloseProxy}, string, string, int, string, CancellationToken)"/> but for hover card content (immediate-dismiss outside close).
    /// </summary>
    ValueTask ObservePosition(string popoverId, ElementReference trigger, ElementReference content, DotNetObjectReference<HoverCardOutsideCloseProxy> callbackReference,
        string side, string align, int sideOffset = 4,
        string positionMode = "popper",
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Re-runs positioning for an active observer (e.g. after scrolling a select item into view).
    /// </summary>
    ValueTask NudgePosition(string popoverId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops tracking the popover position.
    /// </summary>
    ValueTask StopObserving(string popoverId, CancellationToken cancellationToken = default);
}
