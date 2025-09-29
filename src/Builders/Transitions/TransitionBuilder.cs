using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified transition builder with fluent API for chaining transition rules.
/// </summary>
public sealed class TransitionBuilder : ICssBuilder
{
    private readonly List<TransitionRule> _rules = new(4);

    private const string _classTransitionNone = "transition-none";
    private const string _classTransitionAll = "transition-all";
    private const string _classTransitionColors = "transition-colors";
    private const string _classTransitionOpacity = "transition-opacity";
    private const string _classTransitionShadow = "transition-shadow";
    private const string _classTransitionTransform = "transition-transform";

    internal TransitionBuilder(string transition, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TransitionRule(transition, breakpoint));
    }

    internal TransitionBuilder(List<TransitionRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TransitionBuilder None => ChainWithTransition("none");
    public TransitionBuilder All => ChainWithTransition("all");
    public TransitionBuilder Colors => ChainWithTransition("colors");
    public TransitionBuilder Opacity => ChainWithTransition("opacity");
    public TransitionBuilder Shadow => ChainWithTransition("shadow");
    public TransitionBuilder Transform => ChainWithTransition("transform");

    public TransitionBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public TransitionBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public TransitionBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public TransitionBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public TransitionBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public TransitionBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TransitionBuilder ChainWithTransition(string transition)
    {
        _rules.Add(new TransitionRule(transition, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TransitionBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TransitionRule("none", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TransitionRule(last.Transition, breakpoint);
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
            var cls = GetTransitionClass(rule.Transition);
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
            var transitionValue = GetTransitionValue(rule.Transition);

            if (transitionValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("transition: ");
            sb.Append(transitionValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetTransitionClass(string transition)
    {
        return transition switch
        {
            "none" => _classTransitionNone,
            "all" => _classTransitionAll,
            "colors" => _classTransitionColors,
            "opacity" => _classTransitionOpacity,
            "shadow" => _classTransitionShadow,
            "transform" => _classTransitionTransform,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetTransitionValue(string transition)
    {
        return transition switch
        {
            "none" => "none",
            "all" => "all 150ms ease-in-out",
            "colors" => "color 150ms ease-in-out, background-color 150ms ease-in-out, border-color 150ms ease-in-out",
            "opacity" => "opacity 150ms ease-in-out",
            "shadow" => "box-shadow 150ms ease-in-out",
            "transform" => "transform 150ms ease-in-out",
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

    public override string ToString()
    {
        return ToClass();
    }
}

