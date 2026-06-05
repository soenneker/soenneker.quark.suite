using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar collapse behavior.
/// </summary>
[EnumValue]
public sealed partial class SidebarCollapsible
{
    /// <summary>
    /// The offcanvas.
    /// </summary>
    public static readonly SidebarCollapsible Offcanvas = new(0);
    /// <summary>
    /// The icon.
    /// </summary>
    public static readonly SidebarCollapsible Icon = new(1);
    /// <summary>
    /// The none.
    /// </summary>
    public static readonly SidebarCollapsible None = new(2);
}
