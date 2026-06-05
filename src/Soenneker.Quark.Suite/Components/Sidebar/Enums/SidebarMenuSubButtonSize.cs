using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar submenu button size.
/// </summary>
[EnumValue]
public sealed partial class SidebarMenuSubButtonSize
{
    /// <summary>
    /// The sm.
    /// </summary>
    public static readonly SidebarMenuSubButtonSize Sm = new(0);
    /// <summary>
    /// The md.
    /// </summary>
    public static readonly SidebarMenuSubButtonSize Md = new(1);
}
