using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// JavaScript interop for <see cref="ComparisonChart"/>.
/// </summary>
public interface IComparisonChartInterop
{
    /// <summary>
    /// Starts the staged reveal when a chart row enters the viewport.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after observation has started.</returns>
    ValueTask Initialize(ElementReference element, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops observing a chart row and removes its reveal state.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after observation has stopped.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
