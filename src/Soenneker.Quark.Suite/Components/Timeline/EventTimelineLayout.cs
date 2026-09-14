using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Layout used to display events along a horizontal track.</summary>
[EnumValue]
public sealed partial class EventTimelineLayout
{
    /// <summary>Auto layout.</summary>
    public static readonly EventTimelineLayout Auto = new(0);
    /// <summary>FitAll layout.</summary>
    public static readonly EventTimelineLayout FitAll = new(1);
    /// <summary>Scroll layout.</summary>
    public static readonly EventTimelineLayout Scroll = new(2);
}
