using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines visual variants for <see cref="Item"/>.
/// </summary>
[EnumValue]
public sealed partial class ItemVariant
{
    public static readonly ItemVariant Default = new(0);
    public static readonly ItemVariant Outline = new(1);
    public static readonly ItemVariant Muted = new(2);
}
