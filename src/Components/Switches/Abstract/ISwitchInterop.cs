using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Interface for Switch interop operations that manages CSS loading for enhanced switch styling.
/// </summary>
public interface ISwitchInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the switch stylesheet is loaded and ready.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
