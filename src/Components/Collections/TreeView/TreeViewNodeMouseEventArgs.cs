using Microsoft.AspNetCore.Components.Web;

namespace Soenneker.Quark;

/// <summary>
/// Provides data for TreeView node mouse events.
/// </summary>
/// <typeparam name="TNode">The type of nodes in the tree.</typeparam>
public readonly struct TreeViewNodeMouseEventArgs<TNode>
{
    /// <summary>
    /// Initializes a new instance of the TreeViewNodeMouseEventArgs class.
    /// </summary>
    /// <param name="node">The node that triggered the event.</param>
    /// <param name="mouseEventArgs">The mouse event arguments.</param>
    public TreeViewNodeMouseEventArgs(TNode node, MouseEventArgs mouseEventArgs)
    {
        Node = node;
        MouseEventArgs = mouseEventArgs;
    }

    /// <summary>
    /// Gets the node that triggered the event.
    /// </summary>
    public TNode Node { get; }

    /// <summary>
    /// Gets the mouse event arguments.
    /// </summary>
    public MouseEventArgs MouseEventArgs { get; }
}
