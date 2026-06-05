using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents the pill indicator variant.
/// </summary>
[EnumValue]
public sealed partial class PillIndicatorVariant
{
    /// <summary>
    /// The success.
    /// </summary>
    public static readonly PillIndicatorVariant Success = new(0);
    /// <summary>
    /// The error.
    /// </summary>
    public static readonly PillIndicatorVariant Error = new(1);
    /// <summary>
    /// The warning.
    /// </summary>
    public static readonly PillIndicatorVariant Warning = new(2);
    /// <summary>
    /// The info.
    /// </summary>
    public static readonly PillIndicatorVariant Info = new(3);
    /// <summary>
    /// The neutral.
    /// </summary>
    public static readonly PillIndicatorVariant Neutral = new(4);
}
