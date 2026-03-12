namespace Soenneker.Quark;

/// <summary>
/// Represents computed sidebar state shared through cascading parameters.
/// </summary>
public sealed record SidebarContextState
{
    /// <summary>
    /// Gets whether the sidebar is open on desktop.
    /// </summary>
    public required bool Open { get; init; }

    /// <summary>
    /// Gets whether the sidebar is open on mobile.
    /// </summary>
    public required bool OpenMobile { get; init; }

    /// <summary>
    /// Gets whether the current viewport should be treated as mobile.
    /// </summary>
    public required bool IsMobile { get; init; }

    /// <summary>
    /// Gets "expanded" when open, otherwise "collapsed".
    /// </summary>
    public string State => Open ? "expanded" : "collapsed";
}
