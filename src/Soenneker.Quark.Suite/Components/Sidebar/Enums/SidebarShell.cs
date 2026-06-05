using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar shell presentation.
/// </summary>
[EnumValue]
public sealed partial class SidebarShell
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly SidebarShell Default = new(0);
    /// <summary>
    /// The navigation.
    /// </summary>
    public static readonly SidebarShell Navigation = new(1);
}