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
    ValueTask Initialize(CancellationToken cancellationToken = default);

    ValueTask InitializeList(ElementReference element, bool disabled, bool sort, int animation, bool forceFallback, string itemSelector, string? handleSelector,
        string? filterSelector, string? group, DotNetObjectReference<SortableList> callbackReference, CancellationToken cancellationToken = default);

    ValueTask Destroy(ElementReference element, CancellationToken cancellationToken = default);
}
