using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class LinkOpacityBuilder : ICssBuilder
{
    private readonly List<LinkOpacityRule> _rules = new(4);

    internal LinkOpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new LinkOpacityRule(value, breakpoint));
    }

    internal LinkOpacityBuilder(List<LinkOpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public LinkOpacityBuilder V10 => Chain(10);
    public LinkOpacityBuilder V25 => Chain(25);
    public LinkOpacityBuilder V50 => Chain(50);
    public LinkOpacityBuilder V75 => Chain(75);
    public LinkOpacityBuilder V100 => Chain(100);

    public LinkOpacityBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public LinkOpacityBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public LinkOpacityBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public LinkOpacityBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public LinkOpacityBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public LinkOpacityBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkOpacityBuilder Chain(int value)
    {
        _rules.Add(new LinkOpacityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkOpacityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new LinkOpacityRule(100, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new LinkOpacityRule(last.Value, bp);
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
            10 => "link-opacity-10",
            25 => "link-opacity-25",
            50 => "link-opacity-50",
            75 => "link-opacity-75",
            100 => "link-opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            10 => "--bs-link-opacity: 0.1",
            25 => "--bs-link-opacity: 0.25",
            50 => "--bs-link-opacity: 0.5",
            75 => "--bs-link-opacity: 0.75",
            100 => "--bs-link-opacity: 1",
            _ => $"--bs-link-opacity: {value}"
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


