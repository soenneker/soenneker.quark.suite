using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar placement side.
/// </summary>
[EnumValue]
public sealed partial class SidebarSide
{
    public static readonly SidebarSide Left = new(0);
    public static readonly SidebarSide Right = new(1);
}