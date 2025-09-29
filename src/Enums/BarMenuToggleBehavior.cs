using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Controls the interaction behavior for menu toggling within the bar.
/// </summary>
[Intellenum<string>]
public sealed partial class BarMenuToggleBehavior
{
    /// <summary>
    /// Restricts menu interaction to allow only one open menu at a time.
    /// </summary>
    public static readonly BarMenuToggleBehavior AllowSingleMenu = new("allow-single-menu");

    /// <summary>
    /// Permits multiple menus to be open simultaneously.
    /// </summary>
    public static readonly BarMenuToggleBehavior AllowMultipleMenus = new("allow-multiple-menus");
}
