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
    ValueTask Initialize(ElementReference element, object options, DotNetObjectReference<Scrollspy> callbackReference,
        CancellationToken cancellationToken = default);

    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
