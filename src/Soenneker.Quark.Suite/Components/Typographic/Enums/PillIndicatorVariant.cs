using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

[EnumValue]
public sealed partial class PillIndicatorVariant
{
    public static readonly PillIndicatorVariant Success = new(0);
    public static readonly PillIndicatorVariant Error = new(1);
    public static readonly PillIndicatorVariant Warning = new(2);
    public static readonly PillIndicatorVariant Info = new(3);
    public static readonly PillIndicatorVariant Neutral = new(4);
}
