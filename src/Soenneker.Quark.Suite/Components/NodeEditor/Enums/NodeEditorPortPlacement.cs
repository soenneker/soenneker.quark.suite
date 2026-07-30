using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the default position of a port around a node.
/// </summary>
[EnumValue<string>]
public sealed partial class NodeEditorPortPlacement
{
    /// <summary>
    /// Positions a port on the top edge of its node.
    /// </summary>
    public static readonly NodeEditorPortPlacement Top = new("top");

    /// <summary>
    /// Positions a port on the right edge of its node.
    /// </summary>
    public static readonly NodeEditorPortPlacement Right = new("right");

    /// <summary>
    /// Positions a port on the bottom edge of its node.
    /// </summary>
    public static readonly NodeEditorPortPlacement Bottom = new("bottom");

    /// <summary>
    /// Positions a port on the left edge of its node.
    /// </summary>
    public static readonly NodeEditorPortPlacement Left = new("left");
}
