using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the breakpoint for responsive gutters.
/// </summary>
[Intellenum<string>]
public sealed partial class GutterBreakpoint
{
    public static readonly GutterBreakpoint None = new("");
    public static readonly GutterBreakpoint Sm = new("sm");
    public static readonly GutterBreakpoint Md = new("md");
    public static readonly GutterBreakpoint Lg = new("lg");
    public static readonly GutterBreakpoint Xl = new("xl");
    public static readonly GutterBreakpoint Xxl = new("xxl");
}
