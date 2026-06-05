using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Consent manager banner placement side.
/// </summary>
[EnumValue]
public sealed partial class ConsentManagerSide
{
    /// <summary>
    /// The left.
    /// </summary>
    public static readonly ConsentManagerSide Left = new(0);
    /// <summary>
    /// The right.
    /// </summary>
    public static readonly ConsentManagerSide Right = new(1);
    /// <summary>
    /// The center.
    /// </summary>
    public static readonly ConsentManagerSide Center = new(2);
}
