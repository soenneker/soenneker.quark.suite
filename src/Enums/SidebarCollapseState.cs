using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Defines the collapse state of the sidebar component.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarCollapseState
{
    /// <summary>
    /// Sidebar is fully expanded and visible.
    /// </summary>
    public static readonly SidebarCollapseState Expanded = new("expanded");

    /// <summary>
    /// Sidebar is collapsed to a minimal width with only icons visible.
    /// </summary>
    public static readonly SidebarCollapseState Collapsed = new("collapsed");

    /// <summary>
    /// Sidebar is completely hidden from view.
    /// </summary>
    public static readonly SidebarCollapseState Hidden = new("hidden");
}
