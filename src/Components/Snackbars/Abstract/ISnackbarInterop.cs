using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop methods for loading CSS resources for Snackbar components.
/// </summary>
public interface ISnackbarInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures all required CSS resources for Snackbar components are loaded.
    /// This includes loading from embedded static files.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes internal state and JavaScript module references.
    /// </summary>
    new ValueTask DisposeAsync();
}
