using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines size options for <see cref="Toggle"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class ToggleSize
{
    /// <summary>
    /// Default size.
    /// </summary>
    public static readonly ToggleSize Default = new("default");

    /// <summary>
    /// Small size.
    /// </summary>
    public static readonly ToggleSize Sm = new("sm");

    /// <summary>
    /// Large size.
    /// </summary>
    public static readonly ToggleSize Lg = new("lg");

    /// <summary>
    /// Converts a toggle size to the corresponding CSS value.
    /// </summary>
    /// <param name="size">Toggle size to convert.</param>
    /// <returns>A CSS value containing the combined contributors.</returns>
    public static implicit operator CssValue<ToggleSizeBuilder>(ToggleSize size) => size.Value;
}
