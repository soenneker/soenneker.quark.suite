using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified opacity builder with fluent API for chaining opacity rules.
/// </summary>
[TailwindPrefix("opacity-", Responsive = true)]
public sealed class OpacityBuilder : ICssBuilder
{
    private readonly List<OpacityRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal OpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new OpacityRule(value, breakpoint));
    }

    internal OpacityBuilder(List<OpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the opacity to 0 (fully transparent).
    /// </summary>
    public OpacityBuilder Is0 => Chain(0);

    /// <summary>
    /// Sets the opacity to 25%.
    /// </summary>
    public OpacityBuilder Is25 => Chain(25);

    /// <summary>
    /// Sets the opacity to 50%.
    /// </summary>
    public OpacityBuilder Is50 => Chain(50);

    /// <summary>
    /// Sets the opacity to 75%.
    /// </summary>
    public OpacityBuilder Is75 => Chain(75);

    /// <summary>
    /// Sets the opacity to 100% (fully opaque).
    /// </summary>
    public OpacityBuilder Is100 => Chain(100);

    /// <summary>
    /// Applies the opacity on phone breakpoint.
    /// </summary>
    public OpacityBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Applies the opacity on small breakpoint (≥640px).
    /// </summary>
    public OpacityBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);

    /// <summary>
    /// Applies the opacity on tablet breakpoint.
    /// </summary>
    public OpacityBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);

    /// <summary>
    /// Applies the opacity on laptop breakpoint.
    /// </summary>
    public OpacityBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);

    /// <summary>
    /// Applies the opacity on desktop breakpoint.
    /// </summary>
    public OpacityBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);

    /// <summary>
    /// Applies the opacity on the 2xl breakpoint.
    /// </summary>
    public OpacityBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OpacityBuilder Chain(int value)
    {
        _rules.Add(new OpacityRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OpacityBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BreakpointType? ConsumePendingBreakpoint()
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        return breakpoint;
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
            0 => "opacity-0",
            25 => "opacity-25",
            50 => "opacity-50",
            75 => "opacity-75",
            100 => "opacity-100",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(int value)
    {
        return value switch
        {
            0 => "opacity: 0",
            25 => "opacity: 0.25",
            50 => "opacity: 0.5",
            75 => "opacity: 0.75",
            100 => "opacity: 1",
            _ => $"opacity: {value}"
        };
    }
}