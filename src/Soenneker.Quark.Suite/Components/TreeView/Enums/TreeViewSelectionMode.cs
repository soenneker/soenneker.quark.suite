using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the selection mode for TreeView nodes.
/// </summary>
[EnumValue]
public sealed partial class TreeViewSelectionMode
{
    /// <summary>
    /// Single selection mode - only one node can be selected at a time.
    /// </summary>
    public static readonly TreeViewSelectionMode Single = new(0);

    /// <summary>
    /// Multiple selection mode - multiple nodes can be selected at a time.
    /// </summary>
    public static readonly TreeViewSelectionMode Multiple = new(1);
}
