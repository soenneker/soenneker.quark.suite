using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar collapse behavior.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarCollapsible
{
    public static readonly SidebarCollapsible Offcanvas = new("offcanvas");
    public static readonly SidebarCollapsible Icon = new("icon");
    public static readonly SidebarCollapsible None = new("none");
}
