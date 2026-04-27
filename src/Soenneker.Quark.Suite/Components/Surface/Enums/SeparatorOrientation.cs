using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>shadcn Separator orientation.</summary>
[EnumValue]
public sealed partial class SeparatorOrientation
{
    public static readonly SeparatorOrientation Horizontal = new(0);
    public static readonly SeparatorOrientation Vertical = new(1);
}
