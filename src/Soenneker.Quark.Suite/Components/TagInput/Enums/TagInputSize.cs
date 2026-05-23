using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Tag input pill size.
/// </summary>
[EnumValue]
public sealed partial class TagInputSize
{
    public static readonly TagInputSize Sm = new(0);
    public static readonly TagInputSize Md = new(1);
    public static readonly TagInputSize Lg = new(2);
}
