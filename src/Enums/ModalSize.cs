using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents modal dialog size options.
/// </summary>
[EnumValue<string>]
public sealed partial class ModalSize
{
    /// <summary>
    /// Default modal size (500px).
    /// </summary>
    public static readonly ModalSize Default = new("");

    /// <summary>
    /// Small modal size (300px).
    /// </summary>
    public static readonly ModalSize Small = new("sm");

    /// <summary>
    /// Large modal size (800px).
    /// </summary>
    public static readonly ModalSize Large = new("lg");

    /// <summary>
    /// Extra large modal size (1140px).
    /// </summary>
    public static readonly ModalSize ExtraLarge = new("xl");

    /// <summary>
    /// Fullscreen modal.
    /// </summary>
    public static readonly ModalSize Fullscreen = new("fullscreen");

    /// <summary>
    /// Fullscreen below small breakpoint.
    /// </summary>
    public static readonly ModalSize FullscreenSmDown = new("fullscreen-sm-down");

    /// <summary>
    /// Fullscreen below medium breakpoint.
    /// </summary>
    public static readonly ModalSize FullscreenMdDown = new("fullscreen-md-down");

    /// <summary>
    /// Fullscreen below large breakpoint.
    /// </summary>
    public static readonly ModalSize FullscreenLgDown = new("fullscreen-lg-down");

    /// <summary>
    /// Fullscreen below extra large breakpoint.
    /// </summary>
    public static readonly ModalSize FullscreenXlDown = new("fullscreen-xl-down");

    /// <summary>
    /// Fullscreen below extra extra large breakpoint.
    /// </summary>
    public static readonly ModalSize FullscreenXxlDown = new("fullscreen-xxl-down");
}

