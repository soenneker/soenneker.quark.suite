using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents dialog size options.
/// </summary>
[EnumValue<string>]
public sealed partial class DialogSize
{
    /// <summary>
    /// Default dialog size (500px).
    /// </summary>
    public static readonly DialogSize Default = new("");

    /// <summary>
    /// Small dialog size (300px).
    /// </summary>
    public static readonly DialogSize Small = new("sm");

    /// <summary>
    /// Large dialog size (800px).
    /// </summary>
    public static readonly DialogSize Large = new("lg");

    /// <summary>
    /// Extra large dialog size (1140px).
    /// </summary>
    public static readonly DialogSize ExtraLarge = new("xl");

    /// <summary>
    /// Fullscreen dialog.
    /// </summary>
    public static readonly DialogSize Fullscreen = new("fullscreen");

    /// <summary>
    /// Fullscreen below small breakpoint.
    /// </summary>
    public static readonly DialogSize FullscreenSmDown = new("fullscreen-sm-down");

    /// <summary>
    /// Fullscreen below medium breakpoint.
    /// </summary>
    public static readonly DialogSize FullscreenMdDown = new("fullscreen-md-down");

    /// <summary>
    /// Fullscreen below large breakpoint.
    /// </summary>
    public static readonly DialogSize FullscreenLgDown = new("fullscreen-lg-down");

    /// <summary>
    /// Fullscreen below extra large breakpoint.
    /// </summary>
    public static readonly DialogSize FullscreenXlDown = new("fullscreen-xl-down");

    /// <summary>
    /// Fullscreen below extra extra large breakpoint.
    /// </summary>
    public static readonly DialogSize FullscreenXxlDown = new("fullscreen-xxl-down");
}

