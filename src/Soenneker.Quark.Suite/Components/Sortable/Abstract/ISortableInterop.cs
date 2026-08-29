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
    /// Initializes the Sortable so it is ready for use.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Sortable is ready for use.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>
    /// Initializes list.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="disabled">Whether disabled.</param>
    /// <param name="sort">Whether sort.</param>
    /// <param name="animation">Animation for the initialize list operation.</param>
    /// <param name="forceFallback">Whether force fallback.</param>
    /// <param name="itemSelector">item Selector to inspect or update.</param>
    /// <param name="handleSelector">Handle Selector for the initialize list operation.</param>
    /// <param name="filterSelector">Filter Selector for the initialize list operation.</param>
    /// <param name="group">Group to target.</param>
    /// <param name="notifyOnReorder">Whether notify on reorder.</param>
    /// <param name="callbackReference">callback Reference to invoke when the operation runs.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the Sortable is ready for use.</returns>
    ValueTask InitializeList(ElementReference element, bool disabled, bool sort, int animation, bool forceFallback, string itemSelector, string? handleSelector,
        string? filterSelector, string? group, bool notifyOnReorder, DotNetObjectReference<SortableList> callbackReference, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases the resources held by the Sortable.
    /// </summary>
    /// <param name="element">DOM element to inspect or update.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the destroy operation is complete.</returns>
    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
