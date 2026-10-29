using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>Defines the supported switch size variants.</summary>
[EnumValue]
public sealed partial class SwitchSize
{
    /// <summary>The default size.</summary>
    public static readonly SwitchSize Default = new(0);

    /// <summary>The small size.</summary>
    public static readonly SwitchSize Sm = new(1);
}