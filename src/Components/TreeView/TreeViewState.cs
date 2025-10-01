using Soenneker.Quark.Enums;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the state of a TreeView component.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public sealed class TreeViewState<TNode>
{
    /// <summary>
    /// Gets or sets the currently selected node in single selection mode.
    /// </summary>
    public TNode? SelectedNode { get; set; }

    /// <summary>
    /// Gets or sets the collection of selected nodes in multiple selection mode.
    /// </summary>
    public IList<TNode> SelectedNodes { get; set; } = new List<TNode>();

    /// <summary>
    /// Gets or sets the selection mode for the TreeView.
    /// </summary>
    public TreeViewSelectionMode SelectionMode { get; set; } = TreeViewSelectionMode.Single;
}
