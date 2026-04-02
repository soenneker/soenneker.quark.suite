using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance border opacity builder.
/// Produces Tailwind utility classes When possible, otherwise falls back to inline style.
/// </summary>
[TailwindPrefix("border-opacity-", Responsive = true)]
public sealed class BorderOpacityBuilder : ICssBuilder
{
    private readonly List<BorderOpacityRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

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
    public BorderOpacityBuilder OnBase => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the border opacity on small breakpoint (≥640px).
    /// </summary>
    public BorderOpacityBuilder OnSm => ChainBp(BreakpointType.Sm);
    /// <summary>
    /// Applies the border opacity on tablet breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnMd => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the border opacity on laptop breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnLg => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the border opacity on desktop breakpoint.
    /// </summary>
    public BorderOpacityBuilder OnXl => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the border opacity on the 2xl breakpoint.
    /// </summary>
    public BorderOpacityBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderOpacityBuilder Chain(int value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new BorderOpacityRule(value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderOpacityBuilder ChainBp(BreakpointType bp)
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
        return string.Empty;
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

    public override string ToString() => ToClass();
}