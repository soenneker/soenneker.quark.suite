using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Tag input tag list direction.
/// </summary>
[EnumValue]
public sealed partial class TagInputDirection
{
    public static readonly TagInputDirection Row = new(0);
    public static readonly TagInputDirection Column = new(1);
}
