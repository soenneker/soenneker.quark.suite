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

}
