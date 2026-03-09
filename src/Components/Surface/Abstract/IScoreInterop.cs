using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Interface for Score interop operations that manages CSS loading.
/// </summary>
public interface IScoreInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the score stylesheet is loaded and ready.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
