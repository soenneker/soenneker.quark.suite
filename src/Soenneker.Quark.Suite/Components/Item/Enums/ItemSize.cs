using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines size variants for <see cref="Item"/>.
/// </summary>
[EnumValue]
public sealed partial class ItemSize
{
    public static readonly ItemSize Default = new(0);
    public static readonly ItemSize Small = new(1);
    public static readonly ItemSize ExtraSmall = new(2);
}
