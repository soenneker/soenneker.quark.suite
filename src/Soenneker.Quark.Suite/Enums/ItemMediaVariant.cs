using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines media style variants for <see cref="ItemMedia"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class ItemMediaVariant
{
    public static readonly ItemMediaVariant Default = new("default");
    public static readonly ItemMediaVariant Icon = new("icon");
    public static readonly ItemMediaVariant Image = new("image");
}
