using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Alignment options for <see cref="InputGroupAddon"/>.
/// </summary>
[EnumValue]
public sealed partial class InputGroupAddonAlign
{
    /// <summary>
    /// The inline start.
    /// </summary>
    public static readonly InputGroupAddonAlign InlineStart = new(0);
    /// <summary>
    /// The inline end.
    /// </summary>
    public static readonly InputGroupAddonAlign InlineEnd = new(1);
    /// <summary>
    /// The block start.
    /// </summary>
    public static readonly InputGroupAddonAlign BlockStart = new(2);
    /// <summary>
    /// The block end.
    /// </summary>
    public static readonly InputGroupAddonAlign BlockEnd = new(3);
}
