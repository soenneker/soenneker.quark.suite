using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>shadcn Separator orientation.</summary>
[EnumValue]
public sealed partial class SeparatorOrientation
{
    /// <summary>
    /// The horizontal.
    /// </summary>
    public static readonly SeparatorOrientation Horizontal = new(0);
    /// <summary>
    /// The vertical.
    /// </summary>
    public static readonly SeparatorOrientation Vertical = new(1);
}
