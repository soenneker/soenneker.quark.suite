using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the visual variant for <see cref="Toggle"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class ToggleVariant
{
    /// <summary>
    /// Transparent background until hovered/pressed.
    /// </summary>
    public static readonly ToggleVariant Default = new("default");

    /// <summary>
    /// Outlined toggle with border.
    /// </summary>
    public static readonly ToggleVariant Outline = new("outline");
}
