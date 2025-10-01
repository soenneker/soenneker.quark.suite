using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified column size builder with fluent API for chaining column size rules.
/// </summary>
public sealed class ColumnSizeBuilder : ICssBuilder
{
    private readonly List<ColumnSizeRule> _rules = new(4);

    private const string _classCol1 = "col-1";
    private const string _classCol2 = "col-2";
    private const string _classCol3 = "col-3";
    private const string _classCol4 = "col-4";
    private const string _classCol5 = "col-5";
    private const string _classCol6 = "col-6";
    private const string _classCol7 = "col-7";
    private const string _classCol8 = "col-8";
    private const string _classCol9 = "col-9";
    private const string _classCol10 = "col-10";
    private const string _classCol11 = "col-11";
    private const string _classCol12 = "col-12";
    private const string _classColAuto = "col-auto";

    internal ColumnSizeBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ColumnSizeRule(size, breakpoint));
    }


    internal ColumnSizeBuilder(List<ColumnSizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ColumnSizeBuilder Is1 => ChainWithSize("1");
    public ColumnSizeBuilder Is2 => ChainWithSize("2");
    public ColumnSizeBuilder Is3 => ChainWithSize("3");
    public ColumnSizeBuilder Is4 => ChainWithSize("4");
    public ColumnSizeBuilder Is5 => ChainWithSize("5");
    public ColumnSizeBuilder Is6 => ChainWithSize("6");
    public ColumnSizeBuilder Is7 => ChainWithSize("7");
    public ColumnSizeBuilder Is8 => ChainWithSize("8");
    public ColumnSizeBuilder Is9 => ChainWithSize("9");
    public ColumnSizeBuilder Is10 => ChainWithSize("10");
    public ColumnSizeBuilder Is11 => ChainWithSize("11");
    public ColumnSizeBuilder Is12 => ChainWithSize("12");
    public ColumnSizeBuilder Auto => ChainWithSize("auto");

    public ColumnSizeBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ColumnSizeBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ColumnSizeBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ColumnSizeBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ColumnSizeBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ColumnSizeBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnSizeBuilder ChainWithSize(string size)
    {
        _rules.Add(new ColumnSizeRule(size, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnSizeBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ColumnSizeRule("12", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ColumnSizeRule(last.Size, breakpoint);
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
            var cls = GetColumnSizeClass(rule.Size);
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
        // Column sizes are typically handled via Bootstrap classes, not inline styles
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetColumnSizeClass(string size)
    {
        return size switch
        {
            "1" => _classCol1,
            "2" => _classCol2,
            "3" => _classCol3,
            "4" => _classCol4,
            "5" => _classCol5,
            "6" => _classCol6,
            "7" => _classCol7,
            "8" => _classCol8,
            "9" => _classCol9,
            "10" => _classCol10,
            "11" => _classCol11,
            "12" => _classCol12,
            "auto" => _classColAuto,
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
