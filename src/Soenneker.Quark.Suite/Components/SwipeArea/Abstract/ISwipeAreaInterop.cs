using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace Soenneker.Quark;

/// <summary>Owns SwipeArea browser listeners.</summary>
public interface ISwipeAreaInterop
{
    /// <summary>Registers or updates gesture detection. Changed options cancel any in-progress gesture.</summary>
    ValueTask Configure(ElementReference element, DotNetObjectReference<SwipeArea> callback, SwipeAreaInteropOptions options,
        CancellationToken cancellationToken = default);
    /// <summary>Removes listeners and cancels any pending gesture.</summary>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
