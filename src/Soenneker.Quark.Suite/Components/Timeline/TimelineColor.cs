using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class TimelineColor
{
    public static readonly TimelineColor Primary = new("primary");
    public static readonly TimelineColor Secondary = new("secondary");
    public static readonly TimelineColor Muted = new("muted");
    public static readonly TimelineColor Accent = new("accent");
    public static readonly TimelineColor Destructive = new("destructive");
}
