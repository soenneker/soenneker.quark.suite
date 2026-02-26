using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Badge type enumeration.
/// </summary>
[EnumValue<string>]
public sealed partial class BadgeType
{
    /// <summary>
    /// Standard badge type.
    /// </summary>
    public static readonly BadgeType Badge = new("badge");

    /// <summary>
    /// Pill-shaped badge type with rounded edges.
    /// </summary>
    public static readonly BadgeType Pill = new("pill");

    /// <summary>
    /// Dot badge type, typically used as a small indicator.
    /// </summary>
    public static readonly BadgeType Dot = new("dot");
}
