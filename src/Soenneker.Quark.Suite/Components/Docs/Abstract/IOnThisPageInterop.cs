using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JavaScript interop for the docs On This Page component.
/// </summary>
public interface IOnThisPageInterop
{
    /// <summary>
    /// Scans the configured docs content root for table-of-contents headings.
    /// </summary>
    ValueTask<OnThisPageTocItem[]> GetItems(object options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates active heading tracking and returns the observer id.
    /// </summary>
    ValueTask<string> CreateActiveObserver(object options, DotNetObjectReference<OnThisPage> callbackReference,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes an active heading observer by id.
    /// </summary>
    ValueTask DisposeObserver(string observerId, CancellationToken cancellationToken = default);
}
