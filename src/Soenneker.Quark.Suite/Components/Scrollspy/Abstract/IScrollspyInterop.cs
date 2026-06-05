using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JavaScript interop for Scrollspy.
/// </summary>
public interface IScrollspyInterop
{
    /// <summary>
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="options">The options.</param>
    /// <param name="callbackReference">The callback reference.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(ElementReference element, object options, DotNetObjectReference<Scrollspy> callbackReference,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the destroy operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
