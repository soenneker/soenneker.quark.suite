using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class TimelineSize
{
    public static readonly TimelineSize Small = new(0);
    public static readonly TimelineSize Medium = new(1);
    public static readonly TimelineSize Large = new(2);
}
