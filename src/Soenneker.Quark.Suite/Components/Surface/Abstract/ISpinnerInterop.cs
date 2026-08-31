using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Loads resources required by <see cref="Spinner"/> instances.
/// </summary>
public interface ISpinnerInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the shared spinner stylesheet is loaded.
    /// </summary>
    /// <param name="cancellationToken">A token that can cancel initialization.</param>
    /// <returns>A task that completes when the stylesheet is ready.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
