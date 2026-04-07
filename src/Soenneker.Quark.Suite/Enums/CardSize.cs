using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Card size following shadcn/ui conventions.
/// </summary>
[EnumValue<string>]
public sealed partial class CardSize
{
    public static readonly CardSize Default = new("default");
    public static readonly CardSize Sm = new("sm");
}
