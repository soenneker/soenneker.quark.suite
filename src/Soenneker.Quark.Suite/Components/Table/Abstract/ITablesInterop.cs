using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// A Blazor interop library for the Quark Table component
/// </summary>
public interface ITablesInterop : IAsyncDisposable
{
    /// <summary>
    /// Completes table initialization. Adaptive layout loads its module lazily when attached.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the initialization operation.</param>
    /// <returns>A <see cref="ValueTask"/> representing the asynchronous operation.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    /// <summary>Measures rendered content and maintains buffered column widths until detached.</summary>
    ValueTask StartAdaptiveLayout(ElementReference element, ElementReference columns, TableColumnSizingOptions options, CancellationToken cancellationToken = default);

    /// <summary>Stops observing the table and restores its original layout.</summary>
    ValueTask StopAdaptiveLayout(ElementReference element, CancellationToken cancellationToken = default);
}
