using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

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
    /// Small breakpoint (≥640px).
    /// </summary>
    public static readonly GutterBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly GutterBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥1024px).
    /// </summary>
    public static readonly GutterBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1280px).
    /// </summary>
    public static readonly GutterBreakpoint Xl = new("xl");

    /// <summary>
    /// 2xl breakpoint (≥1536px).
    /// </summary>
    public static readonly GutterBreakpoint Xxl = new("2xl");
}
