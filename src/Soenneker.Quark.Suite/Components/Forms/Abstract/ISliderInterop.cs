using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop contract for slider pointer math and drag lifecycle.
/// </summary>
public interface ISliderInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures slider JS resources are loaded and ready.
    /// </summary>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Converts a client pointer position into a slider value for the supplied track.
    /// </summary>
    ValueTask<double> GetValueFromPointer(ElementReference track, double clientX, double clientY, double min, double max, string orientation,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts document-level drag tracking for the provided slider thumb.
    /// </summary>
    ValueTask<double> StartDrag(ElementReference track, long pointerId, double clientX, double clientY, double min, double max, string orientation,
        DotNetObjectReference<Slider> callbackReference, int thumbIndex, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns whether the slider track is rendered in a right-to-left direction.
    /// </summary>
    ValueTask<bool> IsDirectionRtl(ElementReference track, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops any active drag tracking.
    /// </summary>
    ValueTask StopDrag(CancellationToken cancellationToken = default);
}
