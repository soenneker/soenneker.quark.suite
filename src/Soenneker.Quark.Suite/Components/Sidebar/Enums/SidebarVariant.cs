using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar visual variant.
/// </summary>
[EnumValue]
public sealed partial class SidebarVariant
{
    public static readonly SidebarVariant Sidebar = new(0);
    public static readonly SidebarVariant Floating = new(1);
    public static readonly SidebarVariant Inset = new(2);
}
