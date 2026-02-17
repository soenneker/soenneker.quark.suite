using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified display builder with fluent API for chaining display rules.
/// </summary>
public sealed class DisplayBuilder : ICssBuilder
{
    private readonly List<DisplayRule> _rules = new(4);

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
	/// Sets the display to inherit.
	/// </summary>
    public DisplayBuilder Inherit => ChainWithDisplay(GlobalKeyword.InheritValue);
	/// <summary>
	/// Sets the display to initial.
	/// </summary>
    public DisplayBuilder Initial => ChainWithDisplay(GlobalKeyword.InitialValue);
	/// <summary>
	/// Sets the display to revert.
	/// </summary>
    public DisplayBuilder Revert => ChainWithDisplay(GlobalKeyword.RevertValue);
	/// <summary>
	/// Sets the display to revert-layer.
	/// </summary>
    public DisplayBuilder RevertLayer => ChainWithDisplay(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Sets the display to unset.
	/// </summary>
    public DisplayBuilder Unset => ChainWithDisplay(GlobalKeyword.UnsetValue);

	/// <summary>
	/// Applies the display on phone breakpoint.
	/// </summary>
    public DisplayBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the display on tablet breakpoint.
	/// </summary>
    public DisplayBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the display on laptop breakpoint.
	/// </summary>
    public DisplayBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the display on desktop breakpoint.
	/// </summary>
    public DisplayBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the display on widescreen breakpoint.
	/// </summary>
    public DisplayBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
	/// <summary>
	/// Applies the display on ultrawide breakpoint.
	/// </summary>
    public DisplayBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DisplayBuilder ChainWithDisplay(string display)
    {
        _rules.Add(new DisplayRule(display, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DisplayBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new DisplayRule(DisplayKeyword.BlockValue, breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        DisplayRule last = _rules[lastIdx];
        _rules[lastIdx] = new DisplayRule(last.Display, breakpoint);
        return this;
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
            DisplayRule rule = _rules[i];
            string cls = GetDisplayClass(rule.Display);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            DisplayRule rule = _rules[i];
            string val = rule.Display;
            if (val.IsNullOrEmpty())
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append("display: ");
            sb.Append(val);
        }

        return sb.ToString();
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


}
