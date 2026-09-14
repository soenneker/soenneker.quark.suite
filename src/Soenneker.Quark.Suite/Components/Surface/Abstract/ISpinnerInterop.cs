using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Loads shared animation resources for <see cref="Spinner"/> instances.
/// </summary>
public interface ISpinnerInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the spinner stylesheet is loaded. Concurrent and repeated calls share the resource loader's cached load.
    /// </summary>
    /// <param name="cancellationToken">A token that can cancel initialization.</param>
    /// <returns>A task that completes when initialization is complete.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
