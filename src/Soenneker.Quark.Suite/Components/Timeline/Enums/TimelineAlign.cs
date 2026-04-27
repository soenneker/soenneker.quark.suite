using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class TimelineAlign
{
    public static readonly TimelineAlign Left = new("left");
    public static readonly TimelineAlign Center = new("center");
    public static readonly TimelineAlign Right = new("right");
    public static readonly TimelineAlign Alternate = new("alternate");
}
