using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar collapse behavior.
/// </summary>
[EnumValue]
public sealed partial class SidebarCollapsible
{
    public static readonly SidebarCollapsible Offcanvas = new(0);
    public static readonly SidebarCollapsible Icon = new(1);
    public static readonly SidebarCollapsible None = new(2);
}
