using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    /// <summary>
    /// Sets the transition to none.
    /// </summary>
    public TransitionBuilder None => ChainWithTransition("none");
    /// <summary>
    /// Sets the transition to all.
    /// </summary>
    public TransitionBuilder All => ChainWithTransition("all");
    /// <summary>
    /// Sets the transition to colors.
    /// </summary>
    public TransitionBuilder Colors => ChainWithTransition("colors");
    /// <summary>
    /// Sets the transition to opacity.
    /// </summary>
    public TransitionBuilder Opacity => ChainWithTransition("opacity");
    /// <summary>
    /// Sets the transition to shadow.
    /// </summary>
    public TransitionBuilder Shadow => ChainWithTransition("shadow");
    /// <summary>
    /// Sets the transition to transform.
    /// </summary>
    public TransitionBuilder Transform => ChainWithTransition("transform");

    /// <summary>
    /// Applies the transition on phone breakpoint.
    /// </summary>
    public TransitionBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the transition on tablet breakpoint.
    /// </summary>
    public TransitionBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the transition on laptop breakpoint.
    /// </summary>
    public TransitionBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the transition on desktop breakpoint.
    /// </summary>
    public TransitionBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the transition on widescreen breakpoint.
    /// </summary>
    public TransitionBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the transition on ultrawide breakpoint.
    /// </summary>
    public TransitionBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

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

        int lastIdx = _rules.Count - 1;
        TransitionRule last = _rules[lastIdx];
        _rules[lastIdx] = new TransitionRule(last.Transition, breakpoint);
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
            TransitionRule rule = _rules[i];
            string cls = GetTransitionClass(rule.Transition);
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
            TransitionRule rule = _rules[i];
            string? transitionValue = GetTransitionValue(rule.Transition);

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

    /// <summary>
    /// Returns the CSS class string representation of this transition builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

