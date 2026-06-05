using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Tag input pill size.
/// </summary>
[EnumValue]
public sealed partial class TagInputSize
{
    /// <summary>
    /// The sm.
    /// </summary>
    public static readonly TagInputSize Sm = new(0);
    /// <summary>
    /// The md.
    /// </summary>
    public static readonly TagInputSize Md = new(1);
    /// <summary>
    /// The lg.
    /// </summary>
    public static readonly TagInputSize Lg = new(2);
}
