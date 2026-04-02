using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents the breakpoint for responsive text utilities.
/// </summary>
[EnumValue<string>]
public sealed partial class TextBreakpoint
{
    /// <summary>
    /// Small breakpoint (≥640px).
    /// </summary>
    public static readonly TextBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly TextBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥1024px).
    /// </summary>
    public static readonly TextBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1280px).
    /// </summary>
    public static readonly TextBreakpoint Xl = new("xl");

    /// <summary>
    /// 2xl breakpoint (≥1536px).
    /// </summary>
    public static readonly TextBreakpoint Xxl = new("2xl");
}
