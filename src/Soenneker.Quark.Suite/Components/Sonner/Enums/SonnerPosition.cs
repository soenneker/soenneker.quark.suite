using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents the sonner position.
/// </summary>
[EnumValue]
public sealed partial class SonnerPosition
{
    /// <summary>
    /// The top left.
    /// </summary>
    public static readonly SonnerPosition TopLeft = new(0);
    /// <summary>
    /// The top center.
    /// </summary>
    public static readonly SonnerPosition TopCenter = new(1);
    /// <summary>
    /// The top right.
    /// </summary>
    public static readonly SonnerPosition TopRight = new(2);
    /// <summary>
    /// The bottom left.
    /// </summary>
    public static readonly SonnerPosition BottomLeft = new(3);
    /// <summary>
    /// The bottom center.
    /// </summary>
    public static readonly SonnerPosition BottomCenter = new(4);
    /// <summary>
    /// The bottom right.
    /// </summary>
    public static readonly SonnerPosition BottomRight = new(5);
}
