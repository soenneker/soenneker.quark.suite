using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class TimelineIconVariant
{
    public static readonly TimelineIconVariant Solid = new("solid");
    public static readonly TimelineIconVariant Outline = new("outline");
}
