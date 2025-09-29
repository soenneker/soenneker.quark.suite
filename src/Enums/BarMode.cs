using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Defines the layout orientation and behavior of the bar component.
/// </summary>
[Intellenum<string>]
public sealed partial class BarMode
{
    /// <summary>
    /// Standard horizontal bar layout with traditional dropdown behavior.
    /// </summary>
    public static readonly BarMode Horizontal = new("horizontal");

    /// <summary>
    /// Vertical sidebar layout with pop-out style dropdown menus.
    /// </summary>
    public static readonly BarMode VerticalPopout = new("vertical-popout");

    /// <summary>
    /// Vertical sidebar layout with inline expanding dropdown menus.
    /// </summary>
    public static readonly BarMode VerticalInline = new("vertical-inline");

    /// <summary>
    /// Compact vertical sidebar with minimal width and pop-out menus.
    /// </summary>
    public static readonly BarMode VerticalSmall = new("vertical-small");
}
