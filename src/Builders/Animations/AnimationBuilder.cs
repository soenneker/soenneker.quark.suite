using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified animation builder with fluent API for chaining animation rules.
/// </summary>
public sealed class AnimationBuilder : ICssBuilder
{
    private readonly List<AnimationRule> _rules = new(4);

    private const string _classAnimationNone = "animate-none";
    private const string _classAnimationSpin = "animate-spin";
    private const string _classAnimationPing = "animate-ping";
    private const string _classAnimationPulse = "animate-pulse";
    private const string _classAnimationBounce = "animate-bounce";

    internal AnimationBuilder(string animation, BreakpointType? breakpoint = null)
    {
        _rules.Add(new AnimationRule(animation, breakpoint));
    }

    internal AnimationBuilder(List<AnimationRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the animation to none.
    /// </summary>
    public AnimationBuilder None => ChainWithAnimation("none");

    /// <summary>
    /// Sets the animation to spin.
    /// </summary>
    public AnimationBuilder Spin => ChainWithAnimation("spin");

    /// <summary>
    /// Sets the animation to ping.
    /// </summary>
    public AnimationBuilder Ping => ChainWithAnimation("ping");

    /// <summary>
    /// Sets the animation to pulse.
    /// </summary>
    public AnimationBuilder Pulse => ChainWithAnimation("pulse");

    /// <summary>
    /// Sets the animation to bounce.
    /// </summary>
    public AnimationBuilder Bounce => ChainWithAnimation("bounce");

    /// <summary>
    /// Applies the animation on phone breakpoint.
    /// </summary>
    public AnimationBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Applies the animation on tablet breakpoint.
    /// </summary>
    public AnimationBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);

    /// <summary>
    /// Applies the animation on laptop breakpoint.
    /// </summary>
    public AnimationBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);

    /// <summary>
    /// Applies the animation on desktop breakpoint.
    /// </summary>
    public AnimationBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);

    /// <summary>
    /// Applies the animation on widescreen breakpoint.
    /// </summary>
    public AnimationBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);

    /// <summary>
    /// Applies the animation on ultrawide breakpoint.
    /// </summary>
    public AnimationBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AnimationBuilder ChainWithAnimation(string animation)
    {
        _rules.Add(new AnimationRule(animation, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AnimationBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new AnimationRule("none", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        AnimationRule last = _rules[lastIdx];
        _rules[lastIdx] = new AnimationRule(last.Animation, breakpoint);
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
            AnimationRule rule = _rules[i];
            string cls = GetAnimationClass(rule.Animation);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

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
            AnimationRule rule = _rules[i];
            string? animationValue = GetAnimationValue(rule.Animation);

            if (animationValue is null)
                continue;

            if (!first)
                sb.Append("; ");
            else
                first = false;

            sb.Append("animation: ");
            sb.Append(animationValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetAnimationClass(string animation)
    {
        return animation switch
        {
            "none" => _classAnimationNone,
            "spin" => _classAnimationSpin,
            "ping" => _classAnimationPing,
            "pulse" => _classAnimationPulse,
            "bounce" => _classAnimationBounce,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetAnimationValue(string animation)
    {
        return animation switch
        {
            "none" => "none",
            "spin" => "spin 1s linear infinite",
            "ping" => "ping 1s cubic-bezier(0, 0, 0.2, 1) infinite",
            "pulse" => "pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite",
            "bounce" => "bounce 1s infinite",
            _ => null
        };
    }

    /// <summary>
    /// Returns the CSS class string representation of this animation builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}