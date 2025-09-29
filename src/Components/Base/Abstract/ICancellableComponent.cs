using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Contract for components that expose a cancellable, resettable async work scope.
/// </summary>
public interface ICancellableComponent : IComponent
{
    /// <summary>
    /// Gets the current token for in-flight work.
    /// </summary>
    /// <remarks>
    /// Should return <see cref="CancellationToken.None"/> after disposal.
    /// </remarks>
    CancellationToken CancellationToken { get; }

    /// <summary>
    /// Cancels any in-flight work. No-op if nothing has started.
    /// </summary>
    Task Cancel();

    /// <summary>
    /// Cancels current work and swaps in a fresh token/source for new work.
    /// </summary>
    ValueTask ResetCancellation();
}
