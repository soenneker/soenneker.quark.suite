using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// JavaScript interop for the docs On This Page component.
/// </summary>
public interface IOnThisPageInterop
{
    /// <summary>
    /// Scans the configured docs content root for table-of-contents headings.
    /// </summary>
    /// <param name="options">Options to configure for the On This Page.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested on This Page Toc Item[].</returns>
    ValueTask<OnThisPageTocItem[]> GetItems(object options, CancellationToken cancellationToken = default);

}
