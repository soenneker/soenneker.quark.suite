using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar menu button size.
/// </summary>
[EnumValue]
public sealed partial class SidebarMenuButtonSize
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly SidebarMenuButtonSize Default = new(0);
    /// <summary>
    /// The sm.
    /// </summary>
    public static readonly SidebarMenuButtonSize Sm = new(1);
    /// <summary>
    /// The lg.
    /// </summary>
    public static readonly SidebarMenuButtonSize Lg = new(2);
}
