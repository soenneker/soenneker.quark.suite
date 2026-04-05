using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Controls how the sidebar renders on mobile viewports.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarMobileMode
{
    public static readonly SidebarMobileMode Auto = new("auto");
    public static readonly SidebarMobileMode Hidden = new("hidden");
    public static readonly SidebarMobileMode Sheet = new("sheet");
    public static readonly SidebarMobileMode FullScreen = new("full-screen");
}
