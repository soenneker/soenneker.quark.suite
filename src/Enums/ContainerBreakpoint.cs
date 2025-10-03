using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents the breakpoint for responsive containers.
/// </summary>
[Intellenum<string>]
public sealed partial class ContainerBreakpoint
{
    public static readonly ContainerBreakpoint None = new("");
    public static readonly ContainerBreakpoint Sm = new("sm");
    public static readonly ContainerBreakpoint Md = new("md");
    public static readonly ContainerBreakpoint Lg = new("lg");
    public static readonly ContainerBreakpoint Xl = new("xl");
    public static readonly ContainerBreakpoint Xxl = new("xxl");
}
