using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance background opacity builder.
/// Produces Tailwind utility classes When possible, otherwise falls back to inline style.
/// </summary>
[TailwindPrefix("bg-opacity-", Responsive = true)]
public sealed class BackgroundOpacityBuilder : ICssBuilder
{
    private readonly List<BackgroundOpacityRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal BackgroundOpacityBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackgroundOpacityRule(value, breakpoint));
    }

    internal BackgroundOpacityBuilder(List<BackgroundOpacityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the background opacity to 10 (10% opacity).
    /// </summary>
    public BackgroundOpacityBuilder V10 => Chain(10);
    /// <summary>
    /// Sets the background opacity to 25 (25% opacity).
    /// </summary>
    public BackgroundOpacityBuilder V25 => Chain(25);
    /// <summary>
    /// Sets the background opacity to 50 (50% opacity).
    /// </summary>
    public BackgroundOpacityBuilder V50 => Chain(50);
    /// <summary>
    /// Sets the background opacity to 75 (75% opacity).
    /// </summary>
    public BackgroundOpacityBuilder V75 => Chain(75);
    /// <summary>
    /// Sets the background opacity to 100 (fully opaque).
    /// </summary>
    public BackgroundOpacityBuilder V100 => Chain(100);

    /// <summary>
    /// Applies the background opacity on phone breakpoint.
    /// </summary>
    public BackgroundOpacityBuilder OnBase => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the background opacity on small breakpoint (≥640px).
    /// </summary>
    public BackgroundOpacityBuilder OnSm => ChainBp(BreakpointType.Sm);
    /// <summary>
    /// Applies the background opacity on tablet breakpoint.
    /// </summary>
    public BackgroundOpacityBuilder OnMd => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the background opacity on laptop breakpoint.
    /// </summary>
    public BackgroundOpacityBuilder OnLg => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the background opacity on desktop breakpoint.
    /// </summary>
    public BackgroundOpacityBuilder OnXl => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the background opacity on the 2xl breakpoint.
    /// </summary>
    public BackgroundOpacityBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundOpacityBuilder Chain(int value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new BackgroundOpacityRule(value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundOpacityBuilder ChainBp(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
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
            var rule = _rules[i];
            var cls = GetClass(rule.Value);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

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
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(int value)
    {
        return value switch
        {
            10 => "bg-opacity-10",
            25 => "bg-opacity-25",
            50 => "bg-opacity-50",
            75 => "bg-opacity-75",
            100 => "bg-opacity-100",
            _ => string.Empty
        };
    }

    public override string ToString() => ToClass();
}