using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Alignment options for <see cref="InputGroupAddon"/>.
/// </summary>
[EnumValue<string>]
public sealed partial class InputGroupAddonAlign
{
    public static readonly InputGroupAddonAlign InlineStart = new("inline-start");
    public static readonly InputGroupAddonAlign InlineEnd = new("inline-end");
    public static readonly InputGroupAddonAlign BlockStart = new("block-start");
    public static readonly InputGroupAddonAlign BlockEnd = new("block-end");
}
