using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Defines the collapse state of the sidebar component.
/// </summary>
[EnumValue]
public sealed partial class SidebarCollapseState
{
    /// <summary>
    /// Sidebar is fully expanded and visible.
    /// </summary>
    public static readonly SidebarCollapseState Expanded = new(0);

    /// <summary>
    /// Sidebar is collapsed to a minimal width with only icons visible.
    /// </summary>
    public static readonly SidebarCollapseState Collapsed = new(1);

    /// <summary>
    /// Sidebar is completely hidden from view.
    /// </summary>
    public static readonly SidebarCollapseState Hidden = new(2);
}
