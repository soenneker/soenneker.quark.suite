using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Tag input pill variant.
/// </summary>
[EnumValue]
public sealed partial class TagInputVariant
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly TagInputVariant Default = new(0);
    /// <summary>
    /// The primary.
    /// </summary>
    public static readonly TagInputVariant Primary = new(1);
    /// <summary>
    /// The destructive.
    /// </summary>
    public static readonly TagInputVariant Destructive = new(2);
    /// <summary>
    /// The secondary.
    /// </summary>
    public static readonly TagInputVariant Secondary = new(3);
    /// <summary>
    /// The outline.
    /// </summary>
    public static readonly TagInputVariant Outline = new(4);
}
