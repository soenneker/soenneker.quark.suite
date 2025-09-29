

using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a column order rule with optional breakpoint.
/// </summary>
internal readonly struct ColumnOrderRule
{
    public readonly string Order;
    public readonly BreakpointType? Breakpoint;

    public ColumnOrderRule(string order, BreakpointType? breakpoint = null)
    {
        Order = order;
        Breakpoint = breakpoint;
    }

}
