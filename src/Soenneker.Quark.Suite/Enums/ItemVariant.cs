using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines visual variants for <see cref="Item"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class ItemVariant
{
    public static readonly ItemVariant Default = new("default");
    public static readonly ItemVariant Outline = new("outline");
    public static readonly ItemVariant Muted = new("muted");
}
