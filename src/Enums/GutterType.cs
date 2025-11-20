using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the type of gutter spacing.
/// </summary>
[Intellenum<string>]
public sealed partial class GutterType
{
    /// <summary>
    /// Applies gutter spacing to all sides.
    /// </summary>
    public static readonly GutterType All = new("all");

    /// <summary>
    /// Applies gutter spacing horizontally (left and right).
    /// </summary>
    public static readonly GutterType X = new("x");

    /// <summary>
    /// Applies gutter spacing vertically (top and bottom).
    /// </summary>
    public static readonly GutterType Y = new("y");
}
