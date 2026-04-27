using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar menu button visual variant.
/// </summary>
[EnumValue]
public sealed partial class SidebarMenuButtonVariant
{
    public static readonly SidebarMenuButtonVariant Default = new(0);
    public static readonly SidebarMenuButtonVariant Outline = new(1);
    public static readonly SidebarMenuButtonVariant Navigation = new(2);
}
