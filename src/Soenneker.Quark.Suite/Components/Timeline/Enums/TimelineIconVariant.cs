using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class TimelineIconVariant
{
    public static readonly TimelineIconVariant Solid = new(0);
    public static readonly TimelineIconVariant Outline = new(1);
}
