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
    /// Initializes the Scrollspy so it is ready for use.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="options">Options to configure for the Scrollspy.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Scrollspy is ready for use.</returns>
    ValueTask Initialize(ElementReference element, object options, DotNetObjectReference<Scrollspy> callbackReference,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the resources held by the Scrollspy.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the destroy operation is complete.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
