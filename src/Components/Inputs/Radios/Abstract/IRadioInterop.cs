using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Interface for Radio interop operations that manages CSS loading for enhanced radio button styling.
/// </summary>
public interface IRadioInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the radio stylesheet is loaded and ready.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);
}
