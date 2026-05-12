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
    ValueTask Initialize(ElementReference element, DotNetObjectReference<Carousel> callbackReference, int currentIndex, bool isVertical, string? align,
        CancellationToken cancellationToken = default);

    ValueTask<double> MeasureOffset(ElementReference element, int currentIndex, bool isVertical, string? align, CancellationToken cancellationToken = default);

    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
