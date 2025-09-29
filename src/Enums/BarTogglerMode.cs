using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Determines the visual style and positioning of the bar toggle control.
/// </summary>
[Intellenum<string>]
public sealed partial class BarTogglerMode
{
    /// <summary>
    /// Standard inline toggle button integrated within the bar layout.
    /// </summary>
    public static readonly BarTogglerMode Normal = new("normal");

    /// <summary>
    /// Floating popout style toggle that appears outside the bar boundaries.
    /// </summary>
    public static readonly BarTogglerMode Popout = new("popout");
}
