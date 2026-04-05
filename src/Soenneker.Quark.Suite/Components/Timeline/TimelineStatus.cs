using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue<string>]
public sealed partial class TimelineStatus
{
    public static readonly TimelineStatus Completed = new("completed");
    public static readonly TimelineStatus InProgress = new("inprogress");
    public static readonly TimelineStatus Pending = new("pending");
}
