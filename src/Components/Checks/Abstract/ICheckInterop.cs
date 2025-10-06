using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Interface for Check interop operations that manages CSS loading for enhanced checkbox styling.
/// </summary>
public interface ICheckInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the check stylesheet is loaded and ready.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
