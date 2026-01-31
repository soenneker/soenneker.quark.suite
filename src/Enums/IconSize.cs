using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents size options for Font Awesome icons.
/// </summary>
[Intellenum<string>]
public sealed partial class IconSize
{
    /// <summary>
    /// Default size (no size class applied).
    /// </summary>
    public static readonly IconSize None = new("");

    /// <summary>
    /// Extra small size.
    /// </summary>
    public static readonly IconSize ExtraSmall = new("fa-xs");

    /// <summary>
    /// Small size.
    /// </summary>
    public static readonly IconSize Small = new("fa-sm");

    /// <summary>
    /// Large size.
    /// </summary>
    public static readonly IconSize Large = new("fa-lg");

    /// <summary>
    /// 2x size multiplier.
    /// </summary>
    public static readonly IconSize Is2X = new("fa-2x");

    /// <summary>
    /// 3x size multiplier.
    /// </summary>
    public static readonly IconSize Is3X = new("fa-3x");

    /// <summary>
    /// 4x size multiplier.
    /// </summary>
    public static readonly IconSize Is4X = new("fa-4x");

    /// <summary>
    /// 5x size multiplier.
    /// </summary>
    public static readonly IconSize Is5X = new("fa-5x");

    /// <summary>
    /// 6x size multiplier.
    /// </summary>
    public static readonly IconSize Is6X = new("fa-6x");

    /// <summary>
    /// 7x size multiplier.
    /// </summary>
    public static readonly IconSize Is7X = new("fa-7x");

    /// <summary>
    /// 8x size multiplier.
    /// </summary>
    public static readonly IconSize Is8X = new("fa-8x");

    /// <summary>
    /// 9x size multiplier.
    /// </summary>
    public static readonly IconSize Is9X = new("fa-9x");

    /// <summary>
    /// 10x size multiplier.
    /// </summary>
    public static readonly IconSize Is10X = new("fa-10x");
}
