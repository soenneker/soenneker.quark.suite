using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance link offset builder with fluent API for chaining link offset rules.
/// </summary>
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

    /// <summary>
    /// Sets the link offset to 1.
    /// </summary>
    public LinkOffsetBuilder Is1 => Chain(1);
    /// <summary>
    /// Sets the link offset to 2.
    /// </summary>
    public LinkOffsetBuilder Is2 => Chain(2);
    /// <summary>
    /// Sets the link offset to 3.
    /// </summary>
    public LinkOffsetBuilder Is3 => Chain(3);

    /// <summary>
    /// Applies the link offset on phone breakpoint.
    /// </summary>
    public LinkOffsetBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the link offset on tablet breakpoint.
    /// </summary>
    public LinkOffsetBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the link offset on laptop breakpoint.
    /// </summary>
    public LinkOffsetBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the link offset on desktop breakpoint.
    /// </summary>
    public LinkOffsetBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the link offset on widescreen breakpoint.
    /// </summary>
    public LinkOffsetBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the link offset on ultrawide breakpoint.
    /// </summary>
    public LinkOffsetBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

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
            var rule = _rules[i];
            var cls = GetClass(rule.Value);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
            _ => $"text-underline-offset: {value}"
        };
    }

}


