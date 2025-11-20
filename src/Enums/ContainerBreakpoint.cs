using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the breakpoint for responsive containers.
/// </summary>
[Intellenum<string>]
public sealed partial class ContainerBreakpoint
{
    /// <summary>
    /// No breakpoint specified.
    /// </summary>
    public static readonly ContainerBreakpoint None = new("");

    /// <summary>
    /// Small breakpoint (≥576px).
    /// </summary>
    public static readonly ContainerBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly ContainerBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥992px).
    /// </summary>
    public static readonly ContainerBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1200px).
    /// </summary>
    public static readonly ContainerBreakpoint Xl = new("xl");

    /// <summary>
    /// Extra extra large breakpoint (≥1400px).
    /// </summary>
    public static readonly ContainerBreakpoint Xxl = new("xxl");
}
