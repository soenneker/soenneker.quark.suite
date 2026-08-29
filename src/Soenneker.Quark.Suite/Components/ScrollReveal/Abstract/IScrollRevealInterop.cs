using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// JavaScript interop for <see cref="ScrollReveal"/>.
/// </summary>
public interface IScrollRevealInterop
{
    /// <summary>
    /// Starts observing an element for viewport intersection.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="options">Options to configure for the Scroll Reveal.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after the Scroll Reveal has started.</returns>
    ValueTask Initialize(ElementReference element, object options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops observing an element.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes after the Scroll Reveal has stopped.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
