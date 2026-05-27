using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop for browser time zone detection.
/// </summary>
public interface IQuarkBrowserTimeZoneInterop : IAsyncDisposable
{
    /// <summary>
    /// Gets the browser IANA time zone identifier.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>The browser time zone identifier, or <c>null</c> when unavailable.</returns>
    ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default);
}
