using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines size variants for <see cref="Item"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class ItemSize
{
    public static readonly ItemSize Default = new("default");
    public static readonly ItemSize Small = new("small");
    public static readonly ItemSize ExtraSmall = new("xs");
}
