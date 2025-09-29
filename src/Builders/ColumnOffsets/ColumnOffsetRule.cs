
using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.ColumnOffsets;

/// <summary>
/// Represents a column offset rule with optional breakpoint.
/// </summary>
internal readonly struct ColumnOffsetRule
{
    public readonly string Offset;
    public readonly BreakpointType? Breakpoint;

    public ColumnOffsetRule(string offset, BreakpointType? breakpoint = null)
    {
        Offset = offset;
        Breakpoint = breakpoint;
    }

}
