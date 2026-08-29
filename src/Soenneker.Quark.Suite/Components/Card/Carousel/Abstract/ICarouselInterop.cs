using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop contract for carousel measurement behavior.
/// </summary>
public interface ICarouselInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the Carousel so it is ready for use.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="currentIndex">Current Index for the initialize operation.</param>
    /// <param name="isVertical">Whether vertical.</param>
    /// <param name="align">Align for the initialize operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Carousel is ready for use.</returns>
    ValueTask Initialize(ElementReference element, DotNetObjectReference<Carousel> callbackReference, int currentIndex, bool isVertical, string? align,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Measures offset.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="currentIndex">Current Index for the measure offset operation.</param>
    /// <param name="isVertical">Whether vertical.</param>
    /// <param name="align">Align for the measure offset operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested value.</returns>
    ValueTask<double> MeasureOffset(ElementReference element, int currentIndex, bool isVertical, string? align, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the resources held by the Carousel.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the destroy operation is complete.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
