using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Controls how the sidebar renders on mobile viewports.
/// </summary>
[EnumValue]
public sealed partial class SidebarMobileMode
{
    /// <summary>
    /// The auto.
    /// </summary>
    public static readonly SidebarMobileMode Auto = new(0);
    /// <summary>
    /// The hidden.
    /// </summary>
    public static readonly SidebarMobileMode Hidden = new(1);
    /// <summary>
    /// The sheet.
    /// </summary>
    public static readonly SidebarMobileMode Sheet = new(2);
    /// <summary>
    /// The full screen.
    /// </summary>
    public static readonly SidebarMobileMode FullScreen = new(3);
}
