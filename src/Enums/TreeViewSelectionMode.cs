using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Defines the selection mode for TreeView nodes.
/// </summary>
[Intellenum<string>]
public sealed partial class TreeViewSelectionMode
{
    /// <summary>
    /// Single selection mode - only one node can be selected at a time.
    /// </summary>
    public static readonly TreeViewSelectionMode Single = new("Single");

    /// <summary>
    /// Multiple selection mode - multiple nodes can be selected at a time.
    /// </summary>
    public static readonly TreeViewSelectionMode Multiple = new("Multiple");
}
