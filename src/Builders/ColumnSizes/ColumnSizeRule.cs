

using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents a column size rule with optional breakpoint.
/// </summary>
internal readonly struct ColumnSizeRule
{
    public readonly string Size;
    public readonly BreakpointType? Breakpoint;

    public ColumnSizeRule(string size, BreakpointType? breakpoint = null)
    {
        Size = size;
        Breakpoint = breakpoint;
    }

}
