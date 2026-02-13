using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    /// <summary>
    /// Sets the scroll behavior to auto.
    /// </summary>
    public ScrollBehaviorBuilder Auto => ChainWithBehavior("auto");
    /// <summary>
    /// Sets the scroll behavior to smooth.
    /// </summary>
    public ScrollBehaviorBuilder Smooth => ChainWithBehavior("smooth");

    /// <summary>
    /// Applies the scroll behavior on phone breakpoint.
    /// </summary>
    public ScrollBehaviorBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the scroll behavior on tablet breakpoint.
    /// </summary>
    public ScrollBehaviorBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the scroll behavior on laptop breakpoint.
    /// </summary>
    public ScrollBehaviorBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the scroll behavior on desktop breakpoint.
    /// </summary>
    public ScrollBehaviorBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the scroll behavior on widescreen breakpoint.
    /// </summary>
    public ScrollBehaviorBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the scroll behavior on ultrawide breakpoint.
    /// </summary>
    public ScrollBehaviorBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

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

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
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

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    /// <returns>The CSS style string.</returns>
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

    /// <summary>
    /// Returns the CSS class string representation of this scroll behavior builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

