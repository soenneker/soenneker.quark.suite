using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class LinkOffsetBuilder : ICssBuilder
{
    private readonly List<LinkOffsetRule> _rules = new(4);

    internal LinkOffsetBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new LinkOffsetRule(value, breakpoint));
    }

    internal LinkOffsetBuilder(List<LinkOffsetRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public LinkOffsetBuilder Is1 => Chain(1);
    public LinkOffsetBuilder Is2 => Chain(2);
    public LinkOffsetBuilder Is3 => Chain(3);

    public LinkOffsetBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public LinkOffsetBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public LinkOffsetBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public LinkOffsetBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public LinkOffsetBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public LinkOffsetBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkOffsetBuilder Chain(int value)
    {
        _rules.Add(new LinkOffsetRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkOffsetBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new LinkOffsetRule(1, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new LinkOffsetRule(last.Value, bp);
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
            1 => "link-offset-1",
            2 => "link-offset-2",
            3 => "link-offset-3",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            1 => "text-underline-offset: 0.125em",
            2 => "text-underline-offset: 0.25em",
            3 => "text-underline-offset: 0.375em",
            _ => null
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

