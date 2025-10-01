using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Contract for JS interop that manages offcanvas overlays (body lock/backdrop) with global overlay counting.
/// </summary>
public interface IOffcanvasInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the stylesheet are loaded and ready.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
