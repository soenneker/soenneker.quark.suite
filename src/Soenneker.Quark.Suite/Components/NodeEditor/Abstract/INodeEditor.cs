using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Soenneker.Quark;

/// <summary>
/// Represents an interactive editor for positioned nodes and directed connections.
/// </summary>
public interface INodeEditor : IElement
{
    /// <summary>
    /// Gets or sets the positioned nodes rendered by the editor. Node identifiers must be unique.
    /// </summary>
    IReadOnlyList<NodeEditorNodeModel> Nodes { get; set; }

    /// <summary>
    /// Gets or sets the directed connections rendered between node ports. Edge identifiers must be unique.
    /// </summary>
    IReadOnlyList<NodeEditorEdgeModel> Edges { get; set; }

    /// <summary>
    /// Gets or sets empty outgoing branches rendered as compact add-step actions.
    /// </summary>
    IReadOnlyList<NodeEditorAddHandleModel> AddHandles { get; set; }

    /// <summary>
    /// Gets or sets the consumer-defined content rendered inside each positioned node.
    /// </summary>
    RenderFragment<NodeEditorNodeModel> NodeTemplate { get; set; }

    /// <summary>
    /// Gets or sets content rendered above the graph viewport, such as hints or status indicators.
    /// </summary>
    RenderFragment? OverlayContent { get; set; }

    /// <summary>
    /// Gets or sets viewport and interaction behavior.
    /// </summary>
    NodeEditorOptions Options { get; set; }

    /// <summary>
    /// Gets or sets the selected node identifier. Use <c>@bind-SelectedNodeId</c> for two-way binding.
    /// </summary>
    string? SelectedNodeId { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when <see cref="SelectedNodeId"/> changes.
    /// </summary>
    EventCallback<string?> SelectedNodeIdChanged { get; set; }

    /// <summary>
    /// Gets or sets every selected node identifier. Use <c>@bind-SelectedNodeIds</c> to observe marquee and modifier-key selection.
    /// <see cref="SelectedNodeId"/> remains the primary selected node for compatibility.
    /// </summary>
    IReadOnlyList<string> SelectedNodeIds { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when <see cref="SelectedNodeIds"/> changes.
    /// </summary>
    EventCallback<IReadOnlyList<string>> SelectedNodeIdsChanged { get; set; }

    /// <summary>
    /// Gets or sets the selected edge identifier. Use <c>@bind-SelectedEdgeId</c> for two-way binding.
    /// </summary>
    string? SelectedEdgeId { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when <see cref="SelectedEdgeId"/> changes.
    /// </summary>
    EventCallback<string?> SelectedEdgeIdChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback raised after a pointer or keyboard node move completes.
    /// The corresponding model position is updated before this callback is invoked.
    /// </summary>
    EventCallback<NodeEditorNodePositionChangedEventArgs> OnNodePositionChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when a user completes a source-to-target connection gesture.
    /// Add an edge to <see cref="Edges"/> to accept and display the requested connection.
    /// </summary>
    EventCallback<NodeEditorConnectionRequest> OnConnectionRequested { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when a user moves an existing edge endpoint.
    /// Update the corresponding consumer-owned edge to accept the request.
    /// </summary>
    EventCallback<NodeEditorConnectionChangeRequest> OnConnectionChangeRequested { get; set; }

    /// <summary>
    /// Gets or sets an optional asynchronous validator for new connections and endpoint changes.
    /// Port capacities and disabled states are checked before this delegate is invoked.
    /// </summary>
    Func<NodeEditorConnectionValidationRequest, ValueTask<NodeEditorConnectionValidationResult>>? ConnectionValidator { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when an empty branch add handle is activated.
    /// </summary>
    EventCallback<NodeEditorAddRequest> OnAddRequested { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when a <see cref="NodeEditorPaletteItem"/> is dropped onto the graph.
    /// </summary>
    EventCallback<NodeEditorDropRequest> OnNodeDropRequested { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when deletion is requested for the current selection.
    /// The editor does not remove consumer-owned models automatically.
    /// </summary>
    EventCallback<NodeEditorDeleteRequest> OnDeleteRequested { get; set; }

    /// <summary>
    /// Gets or sets whether the consumer currently has an undo snapshot.
    /// </summary>
    bool CanUndo { get; set; }

    /// <summary>
    /// Gets or sets whether the consumer currently has a redo snapshot.
    /// </summary>
    bool CanRedo { get; set; }

    /// <summary>
    /// Gets or sets the controlled callback raised by the toolbar or the platform undo shortcut.
    /// </summary>
    EventCallback OnUndoRequested { get; set; }

    /// <summary>
    /// Gets or sets the controlled callback raised by the toolbar or the platform redo shortcut.
    /// </summary>
    EventCallback OnRedoRequested { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when duplication is requested for the selected node.
    /// </summary>
    EventCallback<NodeEditorDuplicateRequest> OnDuplicateRequested { get; set; }

    /// <summary>
    /// Gets or sets the callback raised after an inline edge-label edit is committed.
    /// The edge model is updated before this callback is invoked.
    /// </summary>
    EventCallback<NodeEditorEdgeLabelChangedEventArgs> OnEdgeLabelChanged { get; set; }

    /// <summary>
    /// Gets or sets the callback raised after viewport pan or zoom settles.
    /// </summary>
    EventCallback<NodeEditorViewportChangedEventArgs> OnViewportChanged { get; set; }

    /// <summary>
    /// Gets or sets whether the built-in viewport toolbar is displayed.
    /// </summary>
    bool ShowControls { get; set; }

    /// <summary>
    /// Gets or sets whether graph mutations schedule a debounced autosave.
    /// </summary>
    bool AutoSave { get; set; }

    /// <summary>
    /// Gets or sets the debounce delay, in milliseconds, before an autosave begins.
    /// </summary>
    int AutoSaveDelay { get; set; }

    /// <summary>
    /// Gets or sets the callback that persists the consumer-owned workflow state.
    /// The callback should read the current controlled node, edge, and application-specific data.
    /// </summary>
    Func<CancellationToken, ValueTask>? OnAutoSave { get; set; }

    /// <summary>
    /// Gets or sets the callback raised when the autosave state changes.
    /// </summary>
    EventCallback<AutoSaveState> AutoSaveStateChanged { get; set; }

    /// <summary>
    /// Gets the current autosave state.
    /// </summary>
    AutoSaveState AutoSaveState { get; }

    /// <summary>
    /// Gets whether at least one autosave has completed successfully.
    /// </summary>
    bool HasAutoSaved { get; }

    /// <summary>
    /// Notifies the editor that consumer-owned workflow data changed outside a built-in graph interaction.
    /// </summary>
    /// <returns>A task representing the autosave scheduling operation.</returns>
    Task NotifyChanged();

    /// <summary>
    /// Immediately persists the current workflow state when autosave is configured.
    /// </summary>
    /// <returns>A task representing the save operation.</returns>
    Task SaveNow();

    /// <summary>
    /// Immediately persists a pending debounced autosave, if one exists.
    /// </summary>
    /// <returns>A task representing the flush operation.</returns>
    Task FlushAutoSave();

    /// <summary>
    /// Requests an undo operation from the controlled host.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Undo();

    /// <summary>
    /// Requests a redo operation from the controlled host.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task Redo();

    /// <summary>
    /// Requests duplication of the selected node.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task DuplicateSelectedNode();

    /// <summary>
    /// Increases the viewport scale by one control step.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ZoomIn();

    /// <summary>
    /// Decreases the viewport scale by one control step.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ZoomOut();

    /// <summary>
    /// Fits all currently rendered nodes into the available viewport.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask FitView();

    /// <summary>
    /// Restores the configured initial viewport translation and scale.
    /// </summary>
    /// <returns>A value task representing the asynchronous operation.</returns>
    ValueTask ResetView();

    /// <summary>
    /// Converts browser client coordinates to graph coordinates using the current pan and zoom.
    /// </summary>
    /// <param name="clientX">The horizontal browser client coordinate.</param>
    /// <param name="clientY">The vertical browser client coordinate.</param>
    /// <returns>The converted graph-space point.</returns>
    ValueTask<NodeEditorGraphPoint> ClientToGraph(double clientX, double clientY);
}
