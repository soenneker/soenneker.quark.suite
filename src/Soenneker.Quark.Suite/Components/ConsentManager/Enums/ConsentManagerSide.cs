using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Consent manager banner placement side.
/// </summary>
[EnumValue]
public sealed partial class ConsentManagerSide
{
    public static readonly ConsentManagerSide Left = new(0);
    public static readonly ConsentManagerSide Right = new(1);
    public static readonly ConsentManagerSide Center = new(2);
}
