using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop methods for managing bar state and behavior.
/// </summary>
public interface IBarInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes horizontal bar (topbar) functionality.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask InitializeHorizontalBar(CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes vertical functionality.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask InitializeVerticalBar(CancellationToken cancellationToken = default);
}
