using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Defines the supported select size variants.</summary>
[EnumValue]
public sealed partial class SelectSize
{
    /// <summary>The default size.</summary>
    public static readonly SelectSize Default = new(0);

    /// <summary>The small size.</summary>
    public static readonly SelectSize Sm = new(1);
}