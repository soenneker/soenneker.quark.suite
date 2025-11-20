using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents flip transformations for Font Awesome icons.
/// </summary>
[Intellenum<string>]
public sealed partial class IconFlip
{
    /// <summary>
    /// No flip transformation applied.
    /// </summary>
    public static readonly IconFlip None = new("");

    /// <summary>
    /// Flip the icon horizontally.
    /// </summary>
    public static readonly IconFlip Horizontal = new("fa-flip-horizontal");

    /// <summary>
    /// Flip the icon vertically.
    /// </summary>
    public static readonly IconFlip Vertical = new("fa-flip-vertical");

    /// <summary>
    /// Flip the icon both horizontally and vertically.
    /// </summary>
    public static readonly IconFlip Both = new("fa-flip-both");
}
