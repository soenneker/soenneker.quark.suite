using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the breakpoint for responsive column auto.
/// </summary>
[Intellenum<string>]
public sealed partial class ColumnAutoBreakpoint
{
    public static readonly ColumnAutoBreakpoint None = new("");
    public static readonly ColumnAutoBreakpoint Sm = new("sm");
    public static readonly ColumnAutoBreakpoint Md = new("md");
    public static readonly ColumnAutoBreakpoint Lg = new("lg");
    public static readonly ColumnAutoBreakpoint Xl = new("xl");
    public static readonly ColumnAutoBreakpoint Xxl = new("xxl");
}
