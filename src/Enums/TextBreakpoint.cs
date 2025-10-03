using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the breakpoint for responsive text utilities.
/// </summary>
[Intellenum<string>]
public sealed partial class TextBreakpoint
{
    public static readonly TextBreakpoint Sm = new("sm");
    public static readonly TextBreakpoint Md = new("md");
    public static readonly TextBreakpoint Lg = new("lg");
    public static readonly TextBreakpoint Xl = new("xl");
    public static readonly TextBreakpoint Xxl = new("xxl");
}
