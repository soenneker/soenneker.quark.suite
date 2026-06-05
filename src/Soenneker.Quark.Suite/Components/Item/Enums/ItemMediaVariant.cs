using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines media style variants for <see cref="ItemMedia"/>.
/// </summary>
[EnumValue]
public sealed partial class ItemMediaVariant
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly ItemMediaVariant Default = new(0);
    /// <summary>
    /// The icon.
    /// </summary>
    public static readonly ItemMediaVariant Icon = new(1);
    /// <summary>
    /// The image.
    /// </summary>
    public static readonly ItemMediaVariant Image = new(2);
}
