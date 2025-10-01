using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Represents the state of a node in a TreeView.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public sealed class TreeViewNodeState<TNode>
{
    /// <summary>
    /// Initializes a new instance of the TreeViewNodeState class.
    /// </summary>
    /// <param name="node">The node data.</param>
    /// <param name="hasChildren">Whether the node has children.</param>
    /// <param name="expanded">Whether the node is expanded.</param>
    /// <param name="disabled">Whether the node is disabled.</param>
    public TreeViewNodeState(TNode node, bool hasChildren, bool expanded, bool disabled)
    {
        Key = System.Guid.NewGuid().ToString();
        Node = node;
        HasChildren = hasChildren;
        Expanded = expanded;
        Disabled = disabled;
    }

    /// <summary>
    /// Gets the unique key for this node state.
    /// </summary>
    internal string Key { get; }

    /// <summary>
    /// Gets the node data.
    /// </summary>
    public TNode Node { get; }

    /// <summary>
    /// Gets or sets whether the node is expanded.
    /// </summary>
    public bool Expanded { get; set; }

    /// <summary>
    /// Gets or sets whether the node is disabled.
    /// </summary>
    public bool Disabled { get; set; }

    /// <summary>
    /// Gets whether the node has children.
    /// </summary>
    public bool HasChildren { get; }

    /// <summary>
    /// Gets the collection of child node states.
    /// </summary>
    public List<TreeViewNodeState<TNode>> Children { get; } = new();
}
