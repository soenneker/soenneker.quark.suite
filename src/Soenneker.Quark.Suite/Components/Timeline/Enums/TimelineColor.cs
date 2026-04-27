using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class TimelineColor
{
    public static readonly TimelineColor Primary = new(0);
    public static readonly TimelineColor Secondary = new(1);
    public static readonly TimelineColor Muted = new(2);
    public static readonly TimelineColor Accent = new(3);
    public static readonly TimelineColor Destructive = new(4);
}
