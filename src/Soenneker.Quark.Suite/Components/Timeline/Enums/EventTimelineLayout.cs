using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Layout used to display events in a horizontal or vertical track.</summary>
[EnumValue]
public sealed partial class EventTimelineLayout
{
    /// <summary>Vertical activity feed with timestamps, dots, and event content.</summary>
    public static readonly EventTimelineLayout Vertical = new(3);
    /// <summary>Auto layout.</summary>
    public static readonly EventTimelineLayout Auto = new(0);
    /// <summary>FitAll layout.</summary>
    public static readonly EventTimelineLayout FitAll = new(1);
    /// <summary>Scroll layout.</summary>
    public static readonly EventTimelineLayout Scroll = new(2);
}
