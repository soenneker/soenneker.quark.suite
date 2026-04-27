using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Alignment options for <see cref="InputGroupAddon"/>.
/// </summary>
[EnumValue]
public sealed partial class InputGroupAddonAlign
{
    public static readonly InputGroupAddonAlign InlineStart = new(0);
    public static readonly InputGroupAddonAlign InlineEnd = new(1);
    public static readonly InputGroupAddonAlign BlockStart = new(2);
    public static readonly InputGroupAddonAlign BlockEnd = new(3);
}
