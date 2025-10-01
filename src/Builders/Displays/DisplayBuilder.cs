using Soenneker.Quark.Enums;

using System;
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

    public DisplayBuilder None => ChainWithDisplay(DisplayKeyword.NoneValue);
    public DisplayBuilder Inline => ChainWithDisplay(DisplayKeyword.InlineValue);
    public DisplayBuilder InlineBlock => ChainWithDisplay(DisplayKeyword.InlineBlockValue);
    public DisplayBuilder Block => ChainWithDisplay(DisplayKeyword.BlockValue);
    public DisplayBuilder Flex => ChainWithDisplay(DisplayKeyword.FlexValue);
    public DisplayBuilder InlineFlex => ChainWithDisplay(DisplayKeyword.InlineFlexValue);
    public DisplayBuilder Grid => ChainWithDisplay(DisplayKeyword.GridValue);
    public DisplayBuilder InlineGrid => ChainWithDisplay(DisplayKeyword.InlineGridValue);
    public DisplayBuilder Table => ChainWithDisplay(DisplayKeyword.TableValue);
    public DisplayBuilder TableCell => ChainWithDisplay(DisplayKeyword.TableCellValue);
    public DisplayBuilder TableRow => ChainWithDisplay(DisplayKeyword.TableRowValue);
    public DisplayBuilder Inherit => ChainWithDisplay(GlobalKeyword.InheritValue);
    public DisplayBuilder Initial => ChainWithDisplay(GlobalKeyword.InitialValue);
    public DisplayBuilder Revert => ChainWithDisplay(GlobalKeyword.RevertValue);
    public DisplayBuilder RevertLayer => ChainWithDisplay(GlobalKeyword.RevertLayerValue);
    public DisplayBuilder Unset => ChainWithDisplay(GlobalKeyword.UnsetValue);

    public DisplayBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public DisplayBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public DisplayBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public DisplayBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public DisplayBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public DisplayBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new DisplayRule(last.Display, breakpoint);
        return this;
    }

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

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
            if (bp.Length != 0)
                cls = InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var val = rule.Display;
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
        return display switch
        {
            DisplayKeyword.NoneValue => "d-none",
            DisplayKeyword.InlineValue => "d-inline",
            DisplayKeyword.InlineBlockValue => "d-inline-block",
            DisplayKeyword.BlockValue => "d-block",
            DisplayKeyword.FlexValue => "d-flex",
            DisplayKeyword.InlineFlexValue => "d-inline-flex",
            DisplayKeyword.GridValue => "d-grid",
            DisplayKeyword.InlineGridValue => "d-inline-grid",
            DisplayKeyword.TableValue => "d-table",
            DisplayKeyword.TableCellValue => "d-table-cell",
            DisplayKeyword.TableRowValue => "d-table-row",
            _ => string.Empty
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex)
                    .CopyTo(dst);
                var idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan()
                    .CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex)
                    .CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan()
                .CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan()
                .CopyTo(dst[idx..]);
        });
    }
}
