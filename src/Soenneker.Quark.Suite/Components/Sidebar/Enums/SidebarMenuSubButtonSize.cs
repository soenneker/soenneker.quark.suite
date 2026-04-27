using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar submenu button size.
/// </summary>
[EnumValue]
public sealed partial class SidebarMenuSubButtonSize
{
    public static readonly SidebarMenuSubButtonSize Sm = new(0);
    public static readonly SidebarMenuSubButtonSize Md = new(1);
}
