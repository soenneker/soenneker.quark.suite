using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Specifies the collapse behavior When the bar is minimized.
/// </summary>
[Intellenum<string>]
public sealed partial class BarCollapseMode
{
    /// <summary>
    /// Completely hides the bar When collapsed, showing no content.
    /// </summary>
    public static readonly BarCollapseMode Hide = new("hide");

    /// <summary>
    /// Reduces the bar to a compact icon-only version When collapsed.
    /// </summary>
    public static readonly BarCollapseMode Small = new("small");
}
