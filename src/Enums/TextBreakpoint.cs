using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the breakpoint for responsive text utilities.
/// </summary>
[Intellenum<string>]
public sealed partial class TextBreakpoint
{
    /// <summary>
    /// Small breakpoint (≥576px).
    /// </summary>
    public static readonly TextBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly TextBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥992px).
    /// </summary>
    public static readonly TextBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1200px).
    /// </summary>
    public static readonly TextBreakpoint Xl = new("xl");

    /// <summary>
    /// Extra extra large breakpoint (≥1400px).
    /// </summary>
    public static readonly TextBreakpoint Xxl = new("xxl");
}
