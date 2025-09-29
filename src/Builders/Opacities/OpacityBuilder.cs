using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class OpacityBuilder : ICssBuilder
{
    private readonly List<OpacityRule> _rules = new(4);

    internal OpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new OpacityRule(value, breakpoint));
    }

    internal OpacityBuilder(List<OpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public OpacityBuilder V0 => Chain(0);
    public OpacityBuilder V25 => Chain(25);
    public OpacityBuilder V50 => Chain(50);
    public OpacityBuilder V75 => Chain(75);
    public OpacityBuilder V100 => Chain(100);

    public OpacityBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public OpacityBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public OpacityBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public OpacityBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public OpacityBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public OpacityBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OpacityBuilder Chain(int value)
    {
        _rules.Add(new OpacityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OpacityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new OpacityRule(100, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        OpacityRule last = _rules[lastIdx];
        _rules[lastIdx] = new OpacityRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            OpacityRule rule = _rules[i];
            string cls = GetClass(rule.Value);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            OpacityRule rule = _rules[i];
            string? css = GetStyle(rule.Value);
            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(int value)
    {
        return value switch
        {
            0 => "opacity-0",
            25 => "opacity-25",
            50 => "opacity-50",
            75 => "opacity-75",
            100 => "opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            0 => "opacity: 0",
            25 => "opacity: 0.25",
            50 => "opacity: 0.5",
            75 => "opacity: 0.75",
            100 => "opacity: 1",
            _ => null
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        int dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            int len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                int idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            int idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}

 
