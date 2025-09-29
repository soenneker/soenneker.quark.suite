using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified scroll behavior builder with fluent API for chaining scroll behavior rules.
/// </summary>
public sealed class ScrollBehaviorBuilder : ICssBuilder
{
    private readonly List<ScrollBehaviorRule> _rules = new(4);

    private const string _classScrollAuto = "scroll-auto";
    private const string _classScrollSmooth = "scroll-smooth";

    internal ScrollBehaviorBuilder(string behavior, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollBehaviorRule(behavior, breakpoint));
    }

    internal ScrollBehaviorBuilder(List<ScrollBehaviorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ScrollBehaviorBuilder Auto => ChainWithBehavior("auto");
    public ScrollBehaviorBuilder Smooth => ChainWithBehavior("smooth");

    public ScrollBehaviorBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public ScrollBehaviorBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public ScrollBehaviorBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public ScrollBehaviorBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public ScrollBehaviorBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public ScrollBehaviorBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollBehaviorBuilder ChainWithBehavior(string behavior)
    {
        _rules.Add(new ScrollBehaviorRule(behavior, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollBehaviorBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ScrollBehaviorRule("auto", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        ScrollBehaviorRule last = _rules[lastIdx];
        _rules[lastIdx] = new ScrollBehaviorRule(last.Behavior, breakpoint);
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
            ScrollBehaviorRule rule = _rules[i];
            string cls = GetScrollBehaviorClass(rule.Behavior);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
            ScrollBehaviorRule rule = _rules[i];
            string? behaviorValue = GetScrollBehaviorValue(rule.Behavior);

            if (behaviorValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("scroll-behavior: ");
            sb.Append(behaviorValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetScrollBehaviorClass(string behavior)
    {
        return behavior switch
        {
            "auto" => _classScrollAuto,
            "smooth" => _classScrollSmooth,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetScrollBehaviorValue(string behavior)
    {
        return behavior switch
        {
            "auto" => "auto",
            "smooth" => "smooth",
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

    public override string ToString()
    {
        return ToClass();
    }
}

