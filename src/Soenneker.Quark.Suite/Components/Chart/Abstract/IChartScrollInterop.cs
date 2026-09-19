using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for chart scrolling and radial value animations.
/// </summary>
public interface IChartScrollInterop : IAsyncDisposable
{

    /// <summary>
    /// Starts observing and animating scrolling chart data and radial slice geometry.
    /// </summary>
    ValueTask Initialize(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops chart animations and removes observers and event handlers.
    /// </summary>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
