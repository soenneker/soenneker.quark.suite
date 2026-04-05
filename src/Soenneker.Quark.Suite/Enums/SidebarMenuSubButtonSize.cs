using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar submenu button size.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarMenuSubButtonSize
{
    public static readonly SidebarMenuSubButtonSize Sm = new("sm");
    public static readonly SidebarMenuSubButtonSize Md = new("md");
}
