using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the selection mode for <see cref="ToggleGroup"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class ToggleGroupType
{
    /// <summary>
    /// Only one toggle can be pressed at a time.
    /// </summary>
    public static readonly ToggleGroupType Single = new("single");

    /// <summary>
    /// Multiple toggles can be pressed.
    /// </summary>
    public static readonly ToggleGroupType Multiple = new("multiple");
}
