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
    ValueTask Initialize(ElementReference element, object options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops observing an element.
    /// </summary>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
