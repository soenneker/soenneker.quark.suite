using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Specifies the collapse behavior when the bar is minimized.
/// </summary>
[Intellenum<string>]
public sealed partial class BarCollapseMode
{
    /// <summary>
    /// Completely hides the bar when collapsed, showing no content.
    /// </summary>
    public static readonly BarCollapseMode Hide = new("hide");

    /// <summary>
    /// Reduces the bar to a compact icon-only version when collapsed.
    /// </summary>
    public static readonly BarCollapseMode Small = new("small");
}
