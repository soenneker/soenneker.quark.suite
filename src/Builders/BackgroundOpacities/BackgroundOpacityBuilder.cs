using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class BackgroundOpacityBuilder : ICssBuilder
{
    private readonly List<BackgroundOpacityRule> _rules = new(4);

    internal BackgroundOpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackgroundOpacityRule(value, breakpoint));
    }

    internal BackgroundOpacityBuilder(List<BackgroundOpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BackgroundOpacityBuilder V10 => Chain(10);
    public BackgroundOpacityBuilder V25 => Chain(25);
    public BackgroundOpacityBuilder V50 => Chain(50);
    public BackgroundOpacityBuilder V75 => Chain(75);
    public BackgroundOpacityBuilder V100 => Chain(100);

    public BackgroundOpacityBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public BackgroundOpacityBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public BackgroundOpacityBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public BackgroundOpacityBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public BackgroundOpacityBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public BackgroundOpacityBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundOpacityBuilder Chain(int value)
    {
        _rules.Add(new BackgroundOpacityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundOpacityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BackgroundOpacityRule(100, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new BackgroundOpacityRule(last.Value, bp);
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
            10 => "bg-opacity-10",
            25 => "bg-opacity-25",
            50 => "bg-opacity-50",
            75 => "bg-opacity-75",
            100 => "bg-opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            10 => "--bs-bg-opacity: 0.1",
            25 => "--bs-bg-opacity: 0.25",
            50 => "--bs-bg-opacity: 0.5",
            75 => "--bs-bg-opacity: 0.75",
            100 => "--bs-bg-opacity: 1",
            _ => $"--bs-bg-opacity: {value}"
        };
    }

}


