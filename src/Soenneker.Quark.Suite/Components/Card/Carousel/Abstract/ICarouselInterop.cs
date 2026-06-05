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
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="callbackReference">The callback reference.</param>
    /// <param name="currentIndex">The current index.</param>
    /// <param name="isVertical">The is vertical.</param>
    /// <param name="align">The align.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(ElementReference element, DotNetObjectReference<Carousel> callbackReference, int currentIndex, bool isVertical, string? align,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the measure offset operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="currentIndex">The current index.</param>
    /// <param name="isVertical">The is vertical.</param>
    /// <param name="align">The align.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<double> MeasureOffset(ElementReference element, int currentIndex, bool isVertical, string? align, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the destroy operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
