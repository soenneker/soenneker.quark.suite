using Soenneker.Gen.EnumValues;

namespace Soenneker.Quark;

/// <summary>
/// Represents the breakpoint for responsive containers.
/// </summary>
[EnumValue<string>]
public sealed partial class ContainerBreakpoint
{
    /// <summary>
    /// No breakpoint specified.
    /// </summary>
    public static readonly ContainerBreakpoint None = new("");

    /// <summary>
    /// Small breakpoint (≥640px).
    /// </summary>
    public static readonly ContainerBreakpoint Sm = new("sm");

    /// <summary>
    /// Medium breakpoint (≥768px).
    /// </summary>
    public static readonly ContainerBreakpoint Md = new("md");

    /// <summary>
    /// Large breakpoint (≥1024px).
    /// </summary>
    public static readonly ContainerBreakpoint Lg = new("lg");

    /// <summary>
    /// Extra large breakpoint (≥1280px).
    /// </summary>
    public static readonly ContainerBreakpoint Xl = new("xl");

    /// <summary>
    /// 2xl breakpoint (≥1536px).
    /// </summary>
    public static readonly ContainerBreakpoint Xxl = new("2xl");
}
