using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar menu button visual variant.
/// </summary>
[EnumValue]
public sealed partial class SidebarMenuButtonVariant
{
    /// <summary>
    /// The default.
    /// </summary>
    public static readonly SidebarMenuButtonVariant Default = new(0);
    /// <summary>
    /// The outline.
    /// </summary>
    public static readonly SidebarMenuButtonVariant Outline = new(1);
    /// <summary>
    /// The navigation.
    /// </summary>
    public static readonly SidebarMenuButtonVariant Navigation = new(2);
}
