using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines visual variants for <see cref="Item"/>.
/// </summary>
[EnumValue]
public sealed partial class ItemVariant
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly ItemVariant Default = new(0);
    /// <summary>
    /// The outline.
    /// </summary>
    public static readonly ItemVariant Outline = new(1);
    /// <summary>
    /// The muted.
    /// </summary>
    public static readonly ItemVariant Muted = new(2);
}
