using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>Owns browser observation for infinite scrolling.</summary>
public interface IInfiniteScrollInterop
{
    /// <summary>Updates observation of the sentinel using its nearest vertical scroll container, or the viewport.
    /// Disabled observations are disconnected. Each enabled update checks whether more content is needed.</summary>
    ValueTask Observe(ElementReference sentinel, DotNetObjectReference<InfiniteScroll> callback, bool enabled,
        int preloadDistance, CancellationToken cancellationToken = default);
    /// <summary>Disconnects and releases the sentinel's observation.</summary>
    ValueTask Destroy(ElementReference sentinel, CancellationToken cancellationToken = default);
}
