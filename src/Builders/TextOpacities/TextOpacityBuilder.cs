using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text opacity builder.
/// Produces Bootstrap utility classes when possible, otherwise falls back to inline style.
/// </summary>
public sealed class TextOpacityBuilder : ICssBuilder
{
    private readonly List<TextOpacityRule> _rules = new(4);

    internal TextOpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextOpacityRule(value, breakpoint));
    }

    internal TextOpacityBuilder(List<TextOpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the text opacity to 25 (25% opacity).
    /// </summary>
    public TextOpacityBuilder V25 => Chain(25);
    /// <summary>
    /// Sets the text opacity to 50 (50% opacity).
    /// </summary>
    public TextOpacityBuilder V50 => Chain(50);
    /// <summary>
    /// Sets the text opacity to 75 (75% opacity).
    /// </summary>
    public TextOpacityBuilder V75 => Chain(75);
    /// <summary>
    /// Sets the text opacity to 100 (fully opaque).
    /// </summary>
    public TextOpacityBuilder V100 => Chain(100);

    /// <summary>
    /// Applies the text opacity on phone breakpoint.
    /// </summary>
    public TextOpacityBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text opacity on tablet breakpoint.
    /// </summary>
    public TextOpacityBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text opacity on laptop breakpoint.
    /// </summary>
    public TextOpacityBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text opacity on desktop breakpoint.
    /// </summary>
    public TextOpacityBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text opacity on widescreen breakpoint.
    /// </summary>
    public TextOpacityBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the text opacity on ultrawide breakpoint.
    /// </summary>
    public TextOpacityBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOpacityBuilder Chain(int value)
    {
        _rules.Add(new TextOpacityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextOpacityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextOpacityRule(100, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextOpacityRule last = _rules[lastIdx];
        _rules[lastIdx] = new TextOpacityRule(last.Value, bp);
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
            TextOpacityRule rule = _rules[i];
            string cls = GetClass(rule.Value);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            TextOpacityRule rule = _rules[i];
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
            25 => "text-opacity-25",
            50 => "text-opacity-50",
            75 => "text-opacity-75",
            100 => "text-opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            25 => "--bs-text-opacity: 0.25",
            50 => "--bs-text-opacity: 0.5",
            75 => "--bs-text-opacity: 0.75",
            100 => "--bs-text-opacity: 1",
            _ => $"--bs-text-opacity: {value}"
        };
    }

}


