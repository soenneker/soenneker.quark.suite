using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark.Enums;

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
    /// Small breakpoint (≥576px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥992px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1200px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Xl = new("xl");

    /// <summary>
    /// Extra extra large breakpoint (≥1400px).
    /// </summary>
    public static readonly ColumnAutoBreakpoint Xxl = new("xxl");
}
