using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the breakpoint for responsive gutters.
/// </summary>
[EnumValue<string>]
public sealed partial class GutterBreakpoint
{
    /// <summary>
    /// No breakpoint specified.
    /// </summary>
    public static readonly GutterBreakpoint None = new("");

    /// <summary>
    /// Small breakpoint (≥576px).
    /// </summary>
    public static readonly GutterBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly GutterBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥992px).
    /// </summary>
    public static readonly GutterBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1200px).
    /// </summary>
    public static readonly GutterBreakpoint Xl = new("xl");

    /// <summary>
    /// Extra extra large breakpoint (≥1400px).
    /// </summary>
    public static readonly GutterBreakpoint Xxl = new("xxl");
}
