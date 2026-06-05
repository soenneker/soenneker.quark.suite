using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the selection behavior for <see cref="Calendar"/>.
/// </summary>
[EnumValue]
public sealed partial class CalendarSelectionMode
{
    /// <summary>
    /// The single.
    /// </summary>
    public static readonly CalendarSelectionMode Single = new(0);
    /// <summary>
    /// The multiple.
    /// </summary>
    public static readonly CalendarSelectionMode Multiple = new(1);
    /// <summary>
    /// The range.
    /// </summary>
    public static readonly CalendarSelectionMode Range = new(2);
}
