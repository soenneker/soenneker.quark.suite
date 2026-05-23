using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Tag input pill variant.
/// </summary>
[EnumValue]
public sealed partial class TagInputVariant
{
    public static readonly TagInputVariant Default = new(0);
    public static readonly TagInputVariant Primary = new(1);
    public static readonly TagInputVariant Destructive = new(2);
    public static readonly TagInputVariant Secondary = new(3);
    public static readonly TagInputVariant Outline = new(4);
}
