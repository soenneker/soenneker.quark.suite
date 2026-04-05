using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Sidebar placement side.
/// </summary>
[EnumValue<string>]
public sealed partial class SidebarSide
{
    public static readonly SidebarSide Left = new("left");
    public static readonly SidebarSide Right = new("right");
}
