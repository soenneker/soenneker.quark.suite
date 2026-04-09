using Microsoft.AspNetCore.Components;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// JavaScript interop for Radix-like select viewport behavior (scroll buttons, scroll-into-view).
/// </summary>
public interface ISelectInterop : IAsyncDisposable
{
    /// <summary>
    /// Wires scroll-up / scroll-down button visibility to viewport scroll state.
    /// </summary>
    ValueTask AttachScrollButtonsAsync(string attachmentId, ElementReference viewport, ElementReference? scrollUp, ElementReference? scrollDown,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes scroll listeners for the given attachment id.
    /// </summary>
    ValueTask DetachScrollButtonsAsync(string attachmentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Scrolls an element into view inside a scrollable viewport (Radix aligns highlighted items).
    /// </summary>
    ValueTask ScrollIntoViewNearestAsync(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adjusts <paramref name="viewport"/> <c>scrollTop</c> by <paramref name="deltaY"/> (used by Radix scroll buttons).
    /// </summary>
    ValueTask ScrollViewportByAsync(ElementReference viewport, double deltaY, CancellationToken cancellationToken = default);
}
