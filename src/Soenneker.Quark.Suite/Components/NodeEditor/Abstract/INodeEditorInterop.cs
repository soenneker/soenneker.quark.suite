using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Soenneker.Quark;

/// <summary>
/// Defines browser interactions for <see cref="NodeEditor"/>.
/// </summary>
public interface INodeEditorInterop : IAsyncDisposable
{
    /// <summary>
    /// Asynchronously releases imported JavaScript module resources.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    new ValueTask DisposeAsync();

    /// <summary>
    /// Initializes browser behavior for a node editor instance.
    /// </summary>
    /// <param name="id">The editor element identifier.</param>
    /// <param name="options">The editor interaction options.</param>
    /// <param name="callbackReference">The .NET callback reference used by browser interactions.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask Initialize(string id, NodeEditorOptions options, DotNetObjectReference<NodeEditor> callbackReference,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Refreshes browser state after editor data or selection changes.
    /// </summary>
    /// <param name="id">The editor element identifier.</param>
    /// <param name="options">The editor interaction options.</param>
    /// <param name="selectedNodeId">The selected node identifier.</param>
    /// <param name="selectedEdgeId">The selected edge identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask Refresh(string id, NodeEditorOptions options, string? selectedNodeId, string? selectedEdgeId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Changes the editor viewport scale by a relative amount.
    /// </summary>
    /// <param name="id">The editor element identifier.</param>
    /// <param name="delta">The scale delta to apply.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ZoomBy(string id, double delta, CancellationToken cancellationToken = default);

    /// <summary>
    /// Fits the rendered graph into the editor viewport.
    /// </summary>
    /// <param name="id">The editor element identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask FitView(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Restores the configured initial viewport.
    /// </summary>
    /// <param name="id">The editor element identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ResetView(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Converts browser client coordinates to graph coordinates.
    /// </summary>
    /// <param name="id">The editor element identifier.</param>
    /// <param name="clientX">The horizontal browser client coordinate.</param>
    /// <param name="clientY">The vertical browser client coordinate.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The converted graph-space point.</returns>
    ValueTask<NodeEditorGraphPoint> ClientToGraph(string id, double clientX, double clientY, CancellationToken cancellationToken = default);

    /// <summary>
    /// Releases browser resources associated with a node editor instance.
    /// </summary>
    /// <param name="id">The editor element identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask Destroy(string id, CancellationToken cancellationToken = default);
}
