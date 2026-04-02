using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified vertical alignment builder with fluent API for chaining vertical alignment rules.
/// </summary>
[TailwindPrefix("align-", Responsive = true)]
public sealed class VerticalAlignBuilder : ICssBuilder
{
    private readonly List<VerticalAlignRule> _rules = new(6);
    private BreakpointType? _pendingBreakpoint;

    internal VerticalAlignBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new VerticalAlignRule(value, breakpoint));
    }

    internal VerticalAlignBuilder(List<VerticalAlignRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the vertical alignment to baseline.
    /// </summary>
    public VerticalAlignBuilder Baseline => Chain(VerticalAlignKeyword.BaselineValue);

    /// <summary>
    /// Sets the vertical alignment to top.
    /// </summary>
    public VerticalAlignBuilder Top => Chain(VerticalAlignKeyword.TopValue);

    /// <summary>
    /// Sets the vertical alignment to middle.
    /// </summary>
    public VerticalAlignBuilder Middle => Chain(VerticalAlignKeyword.MiddleValue);

    /// <summary>
    /// Sets the vertical alignment to bottom.
    /// </summary>
    public VerticalAlignBuilder Bottom => Chain(VerticalAlignKeyword.BottomValue);

    /// <summary>
    /// Sets the vertical alignment to text-top.
    /// </summary>
    public VerticalAlignBuilder TextTop => Chain(VerticalAlignKeyword.TextTopValue);

    /// <summary>
    /// Sets the vertical alignment to text-bottom.
    /// </summary>
    public VerticalAlignBuilder TextBottom => Chain(VerticalAlignKeyword.TextBottomValue);

    /// <summary>
    /// Applies the vertical alignment on phone breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Applies the vertical alignment on small breakpoint (≥640px).
    /// </summary>
    public VerticalAlignBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);

    /// <summary>
    /// Applies the vertical alignment on tablet breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);

    /// <summary>
    /// Applies the vertical alignment on laptop breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);

    /// <summary>
    /// Applies the vertical alignment on desktop breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);

    /// <summary>
    /// Applies the vertical alignment on the 2xl breakpoint.
    /// </summary>
    public VerticalAlignBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VerticalAlignBuilder Chain(string value)
    {
        _rules.Add(new VerticalAlignRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VerticalAlignBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var cls = rule.Value switch
            {
                VerticalAlignKeyword.BaselineValue => "align-baseline",
                VerticalAlignKeyword.TopValue => "align-top",
                VerticalAlignKeyword.MiddleValue => "align-middle",
                VerticalAlignKeyword.BottomValue => "align-bottom",
                VerticalAlignKeyword.TextTopValue => "align-text-top",
                VerticalAlignKeyword.TextBottomValue => "align-text-bottom",
                _ => string.Empty
            };
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
    public override string ToString() => ToClass();
}