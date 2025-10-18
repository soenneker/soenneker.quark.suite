using System.Threading.Tasks;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a navigation bar component that can be horizontal (navbar) or vertical (sidebar).
/// </summary>
public interface IBar : ICancellableElement
{
    /// <summary>
    /// Gets or sets the mode of the bar (horizontal or vertical).
    /// </summary>
    BarMode Mode { get; set; }

    /// <summary>
    /// Gets or sets the placement of the bar (top, bottom, fixed, sticky).
    /// </summary>
    BarPlacement Placement { get; set; }

    /// <summary>
    /// Gets or sets the alignment of items within the bar.
    /// </summary>
    Alignment Alignment { get; set; }

    /// <summary>
    /// Gets or sets whether the bar should expand at the specified breakpoint.
    /// </summary>
    bool Expand { get; set; }

    /// <summary>
    /// Gets or sets whether the bar should use dark theme styling.
    /// </summary>
    bool Dark { get; set; }

    /// <summary>
    /// Gets or sets whether the bar background should be transparent.
    /// </summary>
    bool Transparent { get; set; }

    /// <summary>
    /// Gets or sets whether the bar is visible.
    /// </summary>
    bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the collapse behavior when the bar is hidden.
    /// </summary>
    BarCollapseMode CollapseMode { get; set; }

    /// <summary>
    /// Gets or sets the behavior when multiple menus are toggled.
    /// </summary>
    BarMenuToggleBehavior MenuToggleBehavior { get; set; }

    /// <summary>
    /// Gets or sets the breakpoint at which the navbar will expand.
    /// </summary>
    BreakpointType? Breakpoint { get; set; }

    /// <summary>
    /// Gets or sets the breakpoint at which the navigation will collapse.
    /// </summary>
    BreakpointType? NavigationBreakpoint { get; set; }

    /// <summary>
    /// Toggles the sidebar visibility or collapsed state.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Toggle();
}

