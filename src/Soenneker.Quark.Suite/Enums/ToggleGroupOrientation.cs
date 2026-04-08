using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the layout direction for <see cref="ToggleGroup"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class ToggleGroupOrientation
{
    /// <summary>
    /// Items are laid out in a row.
    /// </summary>
    public static readonly ToggleGroupOrientation Horizontal = new("horizontal");

    /// <summary>
    /// Items are stacked in a column.
    /// </summary>
    public static readonly ToggleGroupOrientation Vertical = new("vertical");
}
