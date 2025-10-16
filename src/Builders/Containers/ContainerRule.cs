using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a CSS rule for Bootstrap container utilities.
/// </summary>
public readonly struct ContainerRule
{
    public readonly ContainerVariant Variant;
    public readonly ContainerBreakpoint Breakpoint;

    public ContainerRule(ContainerVariant variant, ContainerBreakpoint breakpoint)
    {
        Variant = variant;
        Breakpoint = breakpoint;
    }
}
