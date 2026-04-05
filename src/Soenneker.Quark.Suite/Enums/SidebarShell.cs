using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar shell presentation.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarShell
{
    public static readonly SidebarShell Default = new("default");
    public static readonly SidebarShell Navigation = new("navigation");
}