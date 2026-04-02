using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents the breakpoint for responsive column auto.
/// </summary>
[EnumValue<string>]
public sealed partial class ColumnAutoBreakpoint
{
    /// <summary>
    /// No breakpoint specified.
    /// </summary>
    public static readonly ColumnAutoBreakpoint None = new("");

    /// <summary>
    /// Small breakpoint (≥640px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥1024px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1280px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Xl = new("xl");

    /// <summary>
    /// 2xl breakpoint (≥1536px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Xxl = new("2xl");
}
