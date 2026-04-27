using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar menu button size.
/// </summary>
[EnumValue]
public sealed partial class SidebarMenuButtonSize
{
    public static readonly SidebarMenuButtonSize Default = new(0);
    public static readonly SidebarMenuButtonSize Sm = new(1);
    public static readonly SidebarMenuButtonSize Lg = new(2);
}
