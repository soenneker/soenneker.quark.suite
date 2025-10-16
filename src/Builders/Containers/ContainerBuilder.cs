using Soenneker.Quark.Enums;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap container utilities.
/// </summary>
public sealed class ContainerBuilder : ICssBuilder
{
    private ContainerRule? _rule;

    internal ContainerBuilder(ContainerVariant variant, ContainerBreakpoint? breakpoint = null)
    {
        _rule = new ContainerRule(variant, breakpoint ?? ContainerBreakpoint.None);
    }

    /// <summary>Gets the CSS class string for the current configuration.</summary>
    public string ToClass()
    {
        if (!_rule.HasValue)
            return string.Empty;

        return GetContainerClass(_rule.Value);
    }

    /// <summary>Gets the CSS style string for the current configuration.</summary>
    public string ToStyle() => string.Empty;

    /// <summary>Gets the string representation of the current configuration.</summary>
    public override string ToString() => ToClass();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetContainerClass(ContainerRule rule)
    {
        return rule.Variant.Value switch
        {
            ContainerVariant.DefaultValue => "container",
            ContainerVariant.FluidValue => "container-fluid",
            ContainerVariant.ResponsiveValue => rule.Breakpoint.Value switch
            {
                ContainerBreakpoint.SmValue => "container-sm",
                ContainerBreakpoint.MdValue => "container-md",
                ContainerBreakpoint.LgValue => "container-lg",
                ContainerBreakpoint.XlValue => "container-xl",
                ContainerBreakpoint.XxlValue => "container-xxl",
                _ => "container"
            },
            _ => "container"
        };
    }
}
