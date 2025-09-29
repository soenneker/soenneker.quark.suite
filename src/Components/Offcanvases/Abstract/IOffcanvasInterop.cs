using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Quark;

/// <summary>
/// Contract for JS interop that manages offcanvas overlays (body lock/backdrop) with global overlay counting.
/// </summary>
public interface IOffcanvasInterop : IAsyncDisposable
{
    /// <summary>
    /// Ensures the JS module and stylesheet are loaded and ready.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel initialization.</param>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Registers an offcanvas root by element <paramref name="id"/>.
    /// Enables auto-cleanup via a <c>MutationObserver</c> when the element is removed.
    /// </summary>
    /// <param name="id">Unique DOM id for this offcanvas instance.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask Create(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opens the offcanvas (increments global overlay count and adds the body class when first opened).
    /// </summary>
    /// <param name="id">The unique DOM id for this offcanvas instance.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask Open(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Closes the offcanvas (decrements global overlay count and removes the body class when count reaches zero).
    /// </summary>
    /// <param name="id">The unique DOM id for this offcanvas instance.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask Close(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Disposes the observer/entry for this offcanvas instance (idempotent).
    /// </summary>
    /// <param name="id">The unique DOM id for this offcanvas instance.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);
}
