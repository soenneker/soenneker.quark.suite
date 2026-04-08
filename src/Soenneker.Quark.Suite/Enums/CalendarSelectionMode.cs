using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the selection behavior for <see cref="Calendar"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class CalendarSelectionMode
{
    public static readonly CalendarSelectionMode Single = new("single");
    public static readonly CalendarSelectionMode Multiple = new("multiple");
    public static readonly CalendarSelectionMode Range = new("range");
}
