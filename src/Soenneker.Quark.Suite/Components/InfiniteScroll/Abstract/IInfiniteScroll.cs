using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>Loads additional content as the end of a list approaches the visible area.</summary>
public interface IInfiniteScroll : IElement
{
    /// <summary>Whether another page is available.</summary>
    bool HasMore { get; set; }
    /// <summary>Whether the caller is currently fetching content.</summary>
    bool Loading { get; set; }
    /// <summary>Pauses automatic loading, for example after a request fails.</summary>
    bool Disabled { get; set; }
    /// <summary>Distance in pixels below the scroll viewport at which loading starts. Defaults to 300.</summary>
    int PreloadDistance { get; set; }
    /// <summary>Fetches and appends the next page. Complete the task after updating content and HasMore.
    /// Handle recoverable errors in the callback and set Disabled until the user retries.</summary>
    EventCallback LoadMore { get; set; }
    /// <summary>Optional content displayed while loading.</summary>
    RenderFragment? LoadingContent { get; set; }

    /// <summary>Requests the next page if loading is enabled and no request is in progress.
    /// Invoked by browser observation; callers normally use LoadMore instead.</summary>
    Task RequestMore();
}
