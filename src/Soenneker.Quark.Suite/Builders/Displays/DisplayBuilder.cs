using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified display builder with fluent API for chaining display rules.
/// </summary>
[TailwindPrefix("block", Responsive = true)]
public sealed class DisplayBuilder : ICssBuilder
{
    private readonly List<DisplayRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal DisplayBuilder(string display, BreakpointType? breakpoint = null)
    {
        _rules.Add(new DisplayRule(display, breakpoint));
    }

    internal DisplayBuilder(List<DisplayRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

	/// <summary>
	/// Sets the display to none.
	/// </summary>
    public DisplayBuilder None => ChainWithDisplay(DisplayKeyword.NoneValue);
	/// <summary>
	/// Sets the display to inline.
	/// </summary>
    public DisplayBuilder Inline => ChainWithDisplay(DisplayKeyword.InlineValue);
	/// <summary>
	/// Sets the display to inline-block.
	/// </summary>
    public DisplayBuilder InlineBlock => ChainWithDisplay(DisplayKeyword.InlineBlockValue);
	/// <summary>
	/// Sets the display to block.
	/// </summary>
    public DisplayBuilder Block => ChainWithDisplay(DisplayKeyword.BlockValue);
	/// <summary>
	/// Sets the display to flex.
	/// </summary>
    public DisplayBuilder Flex => ChainWithDisplay(DisplayKeyword.FlexValue);
	/// <summary>
	/// Sets the display to inline-flex.
	/// </summary>
    public DisplayBuilder InlineFlex => ChainWithDisplay(DisplayKeyword.InlineFlexValue);
	/// <summary>
	/// Sets the display to grid.
	/// </summary>
    public DisplayBuilder Grid => ChainWithDisplay(DisplayKeyword.GridValue);
	/// <summary>
	/// Sets the display to inline-grid.
	/// </summary>
    public DisplayBuilder InlineGrid => ChainWithDisplay(DisplayKeyword.InlineGridValue);
	/// <summary>
	/// Sets the display to table.
	/// </summary>
    public DisplayBuilder Table => ChainWithDisplay(DisplayKeyword.TableValue);
	/// <summary>
	/// Sets the display to table-cell.
	/// </summary>
    public DisplayBuilder TableCell => ChainWithDisplay(DisplayKeyword.TableCellValue);
	/// <summary>
	/// Sets the display to table-row.
	/// </summary>
    public DisplayBuilder TableRow => ChainWithDisplay(DisplayKeyword.TableRowValue);
	/// <summary>
	/// Applies the display on phone breakpoint.
	/// </summary>
    public DisplayBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the display on small breakpoint (≥640px).
	/// </summary>
    public DisplayBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
	/// <summary>
	/// Applies the display on tablet breakpoint.
	/// </summary>
    public DisplayBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the display on laptop breakpoint.
	/// </summary>
    public DisplayBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the display on desktop breakpoint.
	/// </summary>
    public DisplayBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the display on the 2xl breakpoint.
	/// </summary>
    public DisplayBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DisplayBuilder ChainWithDisplay(string display)
    {
        _rules.Add(new DisplayRule(display, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DisplayBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BreakpointType? ConsumePendingBreakpoint()
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        return breakpoint;
    }

	/// <summary>
	/// Gets the CSS class string for the current configuration.
	/// </summary>
	/// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = GetDisplayClass(rule.Display);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

	/// <summary>
	/// Gets the CSS style string for the current configuration.
	/// </summary>
	/// <returns>The CSS style string.</returns>
    public string ToStyle()
    {
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetDisplayClass(string display)
    {
        // Tailwind/shadcn display utilities (no d- prefix)
        return display switch
        {
            DisplayKeyword.NoneValue => "hidden",
            DisplayKeyword.InlineValue => "inline",
            DisplayKeyword.InlineBlockValue => "inline-block",
            DisplayKeyword.BlockValue => "block",
            DisplayKeyword.FlexValue => "flex",
            DisplayKeyword.InlineFlexValue => "inline-flex",
            DisplayKeyword.GridValue => "grid",
            DisplayKeyword.InlineGridValue => "inline-grid",
            DisplayKeyword.TableValue => "table",
            DisplayKeyword.TableCellValue => "table-cell",
            DisplayKeyword.TableRowValue => "table-row",
            _ => string.Empty
        };
    }
    public override string ToString() => ToClass();
}