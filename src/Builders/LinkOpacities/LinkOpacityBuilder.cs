using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance link opacity builder with fluent API for chaining link opacity rules.
/// </summary>
public sealed class LinkOpacityBuilder : ICssBuilder
{
    private readonly List<LinkOpacityRule> _rules = new(4);

    internal LinkOpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new LinkOpacityRule(value, breakpoint));
    }

    internal LinkOpacityBuilder(List<LinkOpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the link opacity to 10%.
    /// </summary>
    public LinkOpacityBuilder V10 => Chain(10);
    /// <summary>
    /// Sets the link opacity to 25%.
    /// </summary>
    public LinkOpacityBuilder V25 => Chain(25);
    /// <summary>
    /// Sets the link opacity to 50%.
    /// </summary>
    public LinkOpacityBuilder V50 => Chain(50);
    /// <summary>
    /// Sets the link opacity to 75%.
    /// </summary>
    public LinkOpacityBuilder V75 => Chain(75);
    /// <summary>
    /// Sets the link opacity to 100%.
    /// </summary>
    public LinkOpacityBuilder V100 => Chain(100);

    /// <summary>
    /// Applies the link opacity on phone breakpoint.
    /// </summary>
    public LinkOpacityBuilder OnPhone => ChainBp(BreakpointType.Phone);
    /// <summary>
    /// Applies the link opacity on tablet breakpoint.
    /// </summary>
    public LinkOpacityBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    /// <summary>
    /// Applies the link opacity on laptop breakpoint.
    /// </summary>
    public LinkOpacityBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    /// <summary>
    /// Applies the link opacity on desktop breakpoint.
    /// </summary>
    public LinkOpacityBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    /// <summary>
    /// Applies the link opacity on widescreen breakpoint.
    /// </summary>
    public LinkOpacityBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the link opacity on ultrawide breakpoint.
    /// </summary>
    public LinkOpacityBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkOpacityBuilder Chain(int value)
    {
        _rules.Add(new LinkOpacityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinkOpacityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new LinkOpacityRule(100, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new LinkOpacityRule(last.Value, bp);
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
            10 => "link-opacity-10",
            25 => "link-opacity-25",
            50 => "link-opacity-50",
            75 => "link-opacity-75",
            100 => "link-opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            10 => "--bs-link-opacity: 0.1",
            25 => "--bs-link-opacity: 0.25",
            50 => "--bs-link-opacity: 0.5",
            75 => "--bs-link-opacity: 0.75",
            100 => "--bs-link-opacity: 1",
            _ => $"--bs-link-opacity: {value}"
        };
    }

}


