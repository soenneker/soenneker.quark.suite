using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified column order builder with fluent API for chaining column order rules.
/// </summary>
public sealed class ColumnOrderBuilder : ICssBuilder
{
    private readonly List<ColumnOrderRule> _rules = new(4);

    private const string _classOrder1 = "order-1";
    private const string _classOrder2 = "order-2";
    private const string _classOrder3 = "order-3";
    private const string _classOrder4 = "order-4";
    private const string _classOrder5 = "order-5";
    private const string _classOrder6 = "order-6";
    private const string _classOrder7 = "order-7";
    private const string _classOrder8 = "order-8";
    private const string _classOrder9 = "order-9";
    private const string _classOrder10 = "order-10";
    private const string _classOrder11 = "order-11";
    private const string _classOrder12 = "order-12";
    private const string _classOrderFirst = "order-first";
    private const string _classOrderLast = "order-last";

    internal ColumnOrderBuilder(string order, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ColumnOrderRule(order, breakpoint));
    }


    internal ColumnOrderBuilder(List<ColumnOrderRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ColumnOrderBuilder O1 => ChainWithOrder("1");
    public ColumnOrderBuilder O2 => ChainWithOrder("2");
    public ColumnOrderBuilder O3 => ChainWithOrder("3");
    public ColumnOrderBuilder O4 => ChainWithOrder("4");
    public ColumnOrderBuilder O5 => ChainWithOrder("5");
    public ColumnOrderBuilder O6 => ChainWithOrder("6");
    public ColumnOrderBuilder O7 => ChainWithOrder("7");
    public ColumnOrderBuilder O8 => ChainWithOrder("8");
    public ColumnOrderBuilder O9 => ChainWithOrder("9");
    public ColumnOrderBuilder O10 => ChainWithOrder("10");
    public ColumnOrderBuilder O11 => ChainWithOrder("11");
    public ColumnOrderBuilder O12 => ChainWithOrder("12");
    public ColumnOrderBuilder First => ChainWithOrder("first");
    public ColumnOrderBuilder Last => ChainWithOrder("last");

    public ColumnOrderBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ColumnOrderBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ColumnOrderBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ColumnOrderBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ColumnOrderBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ColumnOrderBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnOrderBuilder ChainWithOrder(string order)
    {
        _rules.Add(new ColumnOrderRule(order, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnOrderBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ColumnOrderRule("1", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ColumnOrderRule(last.Order, breakpoint);
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
            var cls = GetColumnOrderClass(rule.Order);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = InsertBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        // Column orders are typically handled via Bootstrap classes, not inline styles
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetColumnOrderClass(string order)
    {
        return order switch
        {
            "1" => _classOrder1,
            "2" => _classOrder2,
            "3" => _classOrder3,
            "4" => _classOrder4,
            "5" => _classOrder5,
            "6" => _classOrder6,
            "7" => _classOrder7,
            "8" => _classOrder8,
            "9" => _classOrder9,
            "10" => _classOrder10,
            "11" => _classOrder11,
            "12" => _classOrder12,
            "first" => _classOrderFirst,
            "last" => _classOrderLast,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpoint(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                var idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}
