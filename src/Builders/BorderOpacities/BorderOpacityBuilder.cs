using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class BorderOpacityBuilder : ICssBuilder
{
    private readonly List<BorderOpacityRule> _rules = new(4);

    internal BorderOpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BorderOpacityRule(value, breakpoint));
    }

    internal BorderOpacityBuilder(List<BorderOpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BorderOpacityBuilder V10 => Chain(10);
    public BorderOpacityBuilder V25 => Chain(25);
    public BorderOpacityBuilder V50 => Chain(50);
    public BorderOpacityBuilder V75 => Chain(75);
    public BorderOpacityBuilder V100 => Chain(100);

    public BorderOpacityBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public BorderOpacityBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public BorderOpacityBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public BorderOpacityBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public BorderOpacityBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public BorderOpacityBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderOpacityBuilder Chain(int value)
    {
        _rules.Add(new BorderOpacityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderOpacityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BorderOpacityRule(100, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new BorderOpacityRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = GetClass(rule.Value);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
            var rule = _rules[i];
            var css = GetStyle(rule.Value);
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
            10 => "border-opacity-10",
            25 => "border-opacity-25",
            50 => "border-opacity-50",
            75 => "border-opacity-75",
            100 => "border-opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            10 => "--bs-border-opacity: 0.1",
            25 => "--bs-border-opacity: 0.25",
            50 => "--bs-border-opacity: 0.5",
            75 => "--bs-border-opacity: 0.75",
            100 => "--bs-border-opacity: 1",
            _ => $"--bs-border-opacity: {value}"
        };
    }

}


