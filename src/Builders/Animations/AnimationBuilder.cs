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

    public AnimationBuilder None => ChainWithAnimation("none");
    public AnimationBuilder Spin => ChainWithAnimation("spin");
    public AnimationBuilder Ping => ChainWithAnimation("ping");
    public AnimationBuilder Pulse => ChainWithAnimation("pulse");
    public AnimationBuilder Bounce => ChainWithAnimation("bounce");

    public AnimationBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public AnimationBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public AnimationBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public AnimationBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public AnimationBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public AnimationBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new AnimationRule(last.Animation, breakpoint);
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
            var cls = GetAnimationClass(rule.Animation);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var animationValue = GetAnimationValue(rule.Animation);

            if (animationValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

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


    public override string ToString()
    {
        return ToClass();
    }
}
