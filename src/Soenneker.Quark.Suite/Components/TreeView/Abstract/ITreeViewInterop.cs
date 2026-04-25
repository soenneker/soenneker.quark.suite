using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Interface for tree view JavaScript interop operations.
/// </summary>
public interface ITreeViewInterop : IAsyncDisposable
{
    /// <summary>
    /// Initializes the tree view interop functionality.
    /// </summary>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A value task that represents the asynchronous initialization operation.</returns>
    ValueTask Initialize(CancellationToken cancellationToken = default);

    ValueTask InitializeTree(ElementReference tree, CancellationToken cancellationToken = default);

    ValueTask DestroyTree(ElementReference tree, CancellationToken cancellationToken = default);
}
