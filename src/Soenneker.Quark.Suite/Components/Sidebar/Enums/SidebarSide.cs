using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar placement side.
/// </summary>
[EnumValue]
public sealed partial class SidebarSide
{
    /// <summary>
    /// The left.
    /// </summary>
    public static readonly SidebarSide Left = new(0);
    /// <summary>
    /// The right.
    /// </summary>
    public static readonly SidebarSide Right = new(1);
}