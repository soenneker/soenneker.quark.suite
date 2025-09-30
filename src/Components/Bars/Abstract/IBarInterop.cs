using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop methods for managing sidebar state and behavior.
/// </summary>
public interface IBarInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures all required CSS resources for Sidebar components are loaded.
    /// This includes loading from embedded static files.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
