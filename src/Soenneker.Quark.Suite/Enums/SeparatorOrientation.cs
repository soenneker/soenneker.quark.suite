using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>shadcn Separator orientation.</summary>
[EnumValue<string>]
public sealed partial class SeparatorOrientation
{
    public static readonly SeparatorOrientation Horizontal = new("horizontal");
    public static readonly SeparatorOrientation Vertical = new("vertical");
}
