using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance border opacity builder.
/// Produces Bootstrap utility classes when possible, otherwise falls back to inline style.
/// </summary>
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

    /// <summary>
    /// Sets the border opacity to 10 (10% opacity).
    /// </summary>
    public BorderOpacityBuilder V10 => Chain(10);
    /// <summary>
    /// Sets the border opacity to 25 (25% opacity).
    /// </summary>
    public BorderOpacityBuilder V25 => Chain(25);
    /// <summary>
    /// Sets the border opacity to 50 (50% opacity).
    /// </summary>
    public BorderOpacityBuilder V50 => Chain(50);
    /// <summary>
    /// Sets the border opacity to 75 (75% opacity).
    /// </summary>
    public BorderOpacityBuilder V75 => Chain(75);
    /// <summary>
    /// Sets the border opacity to 100 (fully opaque).
    /// </summary>
    public BorderOpacityBuilder V100 => Chain(100);

    /// <summary>
    /// Applies the border opacity on phone breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the border opacity on tablet breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the border opacity on laptop breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the border opacity on desktop breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the border opacity on widescreen breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the border opacity on ultrawide breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

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

        int lastIdx = _rules.Count - 1;
        BorderOpacityRule last = _rules[lastIdx];
        _rules[lastIdx] = new BorderOpacityRule(last.Value, bp);
        return this;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            BorderOpacityRule rule = _rules[i];
            string cls = GetClass(rule.Value);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            BorderOpacityRule rule = _rules[i];
            string? css = GetStyle(rule.Value);
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


