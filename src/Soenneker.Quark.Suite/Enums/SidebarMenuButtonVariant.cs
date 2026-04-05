using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar menu button visual variant.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarMenuButtonVariant
{
    public static readonly SidebarMenuButtonVariant Default = new("default");
    public static readonly SidebarMenuButtonVariant Outline = new("outline");
    public static readonly SidebarMenuButtonVariant Navigation = new("navigation");
}
