using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines size variants for <see cref="Item"/>.
/// </summary>
[EnumValue]
public sealed partial class ItemSize
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly ItemSize Default = new(0);
    /// <summary>
    /// The small.
    /// </summary>
    public static readonly ItemSize Small = new(1);
    /// <summary>
    /// The extra small.
    /// </summary>
    public static readonly ItemSize ExtraSmall = new(2);
}
