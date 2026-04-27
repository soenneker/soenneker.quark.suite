using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines media style variants for <see cref="ItemMedia"/>.
/// </summary>
[EnumValue]
public sealed partial class ItemMediaVariant
{
    public static readonly ItemMediaVariant Default = new(0);
    public static readonly ItemMediaVariant Icon = new(1);
    public static readonly ItemMediaVariant Image = new(2);
}
