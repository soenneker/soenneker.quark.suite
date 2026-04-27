using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Controls how the sidebar renders on mobile viewports.
/// </summary>
[EnumValue]
public sealed partial class SidebarMobileMode
{
    public static readonly SidebarMobileMode Auto = new(0);
    public static readonly SidebarMobileMode Hidden = new(1);
    public static readonly SidebarMobileMode Sheet = new(2);
    public static readonly SidebarMobileMode FullScreen = new(3);
}
