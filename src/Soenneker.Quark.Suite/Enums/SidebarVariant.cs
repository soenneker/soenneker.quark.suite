using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar visual variant.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarVariant
{
    public static readonly SidebarVariant Sidebar = new("sidebar");
    public static readonly SidebarVariant Floating = new("floating");
    public static readonly SidebarVariant Inset = new("inset");
}
