// ReSharper disable InconsistentNaming
using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents size options for Font Awesome icons.
/// </summary>
[EnumValue<string>]
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
    public static readonly IconSize Is2x = new("fa-2x");

    /// <summary>
    /// 3x size multiplier.
    /// </summary>
    public static readonly IconSize Is3x = new("fa-3x");

    /// <summary>
    /// 4x size multiplier.
    /// </summary>
    public static readonly IconSize Is4x = new("fa-4x");

    /// <summary>
    /// 5x size multiplier.
    /// </summary>
    public static readonly IconSize Is5x = new("fa-5x");

    /// <summary>
    /// 6x size multiplier.
    /// </summary>
    public static readonly IconSize Is6x = new("fa-6x");

    /// <summary>
    /// 7x size multiplier.
    /// </summary>
    public static readonly IconSize Is7x = new("fa-7x");

    /// <summary>
    /// 8x size multiplier.
    /// </summary>
    public static readonly IconSize Is8x = new("fa-8x");

    /// <summary>
    /// 9x size multiplier.
    /// </summary>
    public static readonly IconSize Is9x = new("fa-9x");

    /// <summary>
    /// 10x size multiplier.
    /// </summary>
    public static readonly IconSize Is10x = new("fa-10x");
}
