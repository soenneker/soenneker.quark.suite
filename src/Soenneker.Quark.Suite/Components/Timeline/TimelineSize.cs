using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class TimelineSize
{
    public static readonly TimelineSize Small = new("small");
    public static readonly TimelineSize Medium = new("medium");
    public static readonly TimelineSize Large = new("large");
}
