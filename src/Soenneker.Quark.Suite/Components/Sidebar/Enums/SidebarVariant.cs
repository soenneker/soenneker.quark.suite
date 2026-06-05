using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar visual variant.
/// </summary>
[EnumValue]
public sealed partial class SidebarVariant
{
    /// <summary>
    /// The sidebar.
    /// </summary>
    public static readonly SidebarVariant Sidebar = new(0);
    /// <summary>
    /// The floating.
    /// </summary>
    public static readonly SidebarVariant Floating = new(1);
    /// <summary>
    /// The inset.
    /// </summary>
    public static readonly SidebarVariant Inset = new(2);
}
