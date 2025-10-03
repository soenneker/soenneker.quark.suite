using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop methods for managing Bootstrap CSS and JavaScript resources.
/// </summary>
public interface IBootstrapInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes Bootstrap CSS and JavaScript resources.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}