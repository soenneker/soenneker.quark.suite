using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the selection behavior for <see cref="Calendar"/>.
/// </summary>
[EnumValue]
public sealed partial class CalendarSelectionMode
{
    public static readonly CalendarSelectionMode Single = new(0);
    public static readonly CalendarSelectionMode Multiple = new(1);
    public static readonly CalendarSelectionMode Range = new(2);
}
