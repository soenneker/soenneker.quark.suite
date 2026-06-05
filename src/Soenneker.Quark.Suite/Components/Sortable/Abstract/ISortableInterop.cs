using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// JS interop contract for SortableJS-backed list interactions.
/// </summary>
public interface ISortableInterop : IAsyncDisposable
{
    /// <summary>
    /// Executes the initialize operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes list.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="disabled">The disabled.</param>
    /// <param name="sort">The sort.</param>
    /// <param name="animation">The animation.</param>
    /// <param name="forceFallback">The force fallback.</param>
    /// <param name="itemSelector">The item selector.</param>
    /// <param name="handleSelector">The handle selector.</param>
    /// <param name="filterSelector">The filter selector.</param>
    /// <param name="group">The group.</param>
    /// <param name="notifyOnReorder">The notify on reorder.</param>
    /// <param name="callbackReference">The callback reference.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask InitializeList(ElementReference element, bool disabled, bool sort, int animation, bool forceFallback, string itemSelector, string? handleSelector,
        string? filterSelector, string? group, bool notifyOnReorder, DotNetObjectReference<SortableList> callbackReference, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the destroy operation.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
