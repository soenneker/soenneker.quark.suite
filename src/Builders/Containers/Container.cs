using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap container types.
/// </summary>
public readonly struct ContainerUtility
{
    public readonly ContainerType Type;
    public readonly ContainerBreakpoint Breakpoint;

    public ContainerUtility(ContainerType type, ContainerBreakpoint? breakpoint = null)
    {
        Type = type;
        Breakpoint = breakpoint ?? ContainerBreakpoint.None;
    }

    public static ContainerUtility Default => new(ContainerType.Default);
    public static ContainerUtility Fluid => new(ContainerType.Fluid);
    public static ContainerUtility Small => new(ContainerType.Responsive, ContainerBreakpoint.Sm);
    public static ContainerUtility Medium => new(ContainerType.Responsive, ContainerBreakpoint.Md);
    public static ContainerUtility Large => new(ContainerType.Responsive, ContainerBreakpoint.Lg);
    public static ContainerUtility ExtraLarge => new(ContainerType.Responsive, ContainerBreakpoint.Xl);
    public static ContainerUtility ExtraExtraLarge => new(ContainerType.Responsive, ContainerBreakpoint.Xxl);
}
