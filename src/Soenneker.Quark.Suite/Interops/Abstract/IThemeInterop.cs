using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop methods for application theme management.
/// </summary>
public interface IThemeInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes and applies the current theme preference.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns><c>true</c> if dark mode is active; otherwise, <c>false</c>.</returns>
    ValueTask<bool> Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Toggles between light and dark mode.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns><c>true</c> if dark mode is active after toggle; otherwise, <c>false</c>.</returns>
    ValueTask<bool> Toggle(CancellationToken cancellationToken = default);
}
