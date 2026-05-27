using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides cached browser time zone detection for Quark date/time components.
/// </summary>
public interface IQuarkBrowserTimeZoneService
{
    /// <summary>
    /// Gets the browser time zone identifier.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the wait operation.</param>
    /// <returns>The detected browser time zone identifier, or <c>null</c> when unavailable.</returns>
    ValueTask<string?> GetTimeZoneId(CancellationToken cancellationToken = default);
}
