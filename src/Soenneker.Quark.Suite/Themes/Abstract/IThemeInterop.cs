using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Provides JavaScript interop methods for application theme management.
/// </summary>
public interface IThemeInterop : IAsyncDisposable
{
    /// <summary>Gets the active theme after initialization. Subscribe to <see cref="ThemeChanged"/> to refresh theme-dependent content such as images.</summary>
    bool IsDark { get; }

    /// <summary>Gets the selected mode after initialization, independently of the resolved color scheme.</summary>
    ThemeMode Mode { get; }

    /// <summary>Sets and saves the preferred mode. System mode follows subsequent operating system changes.</summary>
    /// <param name="mode">The preferred color scheme.</param>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Whether dark mode is active.</returns>
    ValueTask<bool> SetMode(ThemeMode mode, CancellationToken cancellationToken = default);

    /// <summary>Raised when the selected mode or active theme changes, including operating system changes while following the system. Components should dispatch rendering through InvokeAsync.</summary>
    event Action<bool>? ThemeChanged;

    /// <summary>
    /// Initializes and applies the saved preference, or follows the operating system when no explicit preference exists. Call after the first interactive render.
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

    /// <summary>
    /// Gets whether dark mode is selected by the saved preference, otherwise by the operating system.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns><c>true</c> if dark mode is active; otherwise, <c>false</c>.</returns>
    ValueTask<bool> GetIsDark(CancellationToken cancellationToken = default);

    /// <summary>Clears the explicit preference and resumes following the operating system, including subsequent changes.</summary>
    /// <param name="cancellationToken">A cancellation token to cancel the operation.</param>
    /// <returns>Whether dark mode is active.</returns>
    ValueTask<bool> UseSystem(CancellationToken cancellationToken = default);
}
