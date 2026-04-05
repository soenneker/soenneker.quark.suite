using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar menu button size.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarMenuButtonSize
{
    public static readonly SidebarMenuButtonSize Default = new("default");
    public static readonly SidebarMenuButtonSize Sm = new("sm");
    public static readonly SidebarMenuButtonSize Lg = new("lg");
}
