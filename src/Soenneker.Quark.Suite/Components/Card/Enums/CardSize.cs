using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Card size following shadcn/ui conventions.
/// </summary>
[EnumValue<string>]
public sealed partial class CardSize
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly CardSize Default = new("default");
    /// <summary>
    /// The sm.
    /// </summary>
    public static readonly CardSize Sm = new("sm");
}
