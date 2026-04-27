using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar shell presentation.
/// </summary>
[EnumValue]
public sealed partial class SidebarShell
{
    public static readonly SidebarShell Default = new(0);
    public static readonly SidebarShell Navigation = new(1);
}