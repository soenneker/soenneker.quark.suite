using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Bar placement enumeration.
/// </summary>
[Intellenum<string>]
public sealed partial class BarPlacement
{
    /// <summary>
    /// Places the bar at the top of the page.
    /// </summary>
    public static readonly BarPlacement Top = new("top");

    /// <summary>
    /// Places the bar at the bottom of the page.
    /// </summary>
    public static readonly BarPlacement Bottom = new("bottom");

    /// <summary>
    /// Places the bar in a fixed position on the page.
    /// </summary>
    public static readonly BarPlacement Fixed = new("fixed");

    /// <summary>
    /// Places the bar in a sticky position that remains visible while scrolling.
    /// </summary>
    public static readonly BarPlacement Sticky = new("sticky");
}
