using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified vertical alignment builder with fluent API for chaining vertical alignment rules.
/// </summary>
public sealed class VerticalAlignBuilder : ICssBuilder
{
    private readonly List<VerticalAlignRule> _rules = new(6);

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
    /// Sets the vertical alignment to inherit.
    /// </summary>
    public VerticalAlignBuilder Inherit => Chain(GlobalKeyword.InheritValue);

    /// <summary>
    /// Sets the vertical alignment to initial.
    /// </summary>
    public VerticalAlignBuilder Initial => Chain(GlobalKeyword.InitialValue);

    /// <summary>
    /// Sets the vertical alignment to revert.
    /// </summary>
    public VerticalAlignBuilder Revert => Chain(GlobalKeyword.RevertValue);

    /// <summary>
    /// Sets the vertical alignment to revert-layer.
    /// </summary>
    public VerticalAlignBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);

    /// <summary>
    /// Sets the vertical alignment to unset.
    /// </summary>
    public VerticalAlignBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Applies the vertical alignment on phone breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnPhone => ChainBp(BreakpointType.Base);

    /// <summary>
    /// Applies the vertical alignment on tablet breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnTablet => ChainBp(BreakpointType.Md);

    /// <summary>
    /// Applies the vertical alignment on laptop breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnLaptop => ChainBp(BreakpointType.Lg);

    /// <summary>
    /// Applies the vertical alignment on desktop breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnDesktop => ChainBp(BreakpointType.Xl);

    /// <summary>
    /// Applies the vertical alignment on widescreen breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);

    /// <summary>
    /// Applies the vertical alignment on ultrawide breakpoint.
    /// </summary>
    public VerticalAlignBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VerticalAlignBuilder Chain(string value)
    {
        _rules.Add(new VerticalAlignRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VerticalAlignBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new VerticalAlignRule(VerticalAlignKeyword.BaselineValue, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        VerticalAlignRule last = _rules[lastIdx];
        _rules[lastIdx] = new VerticalAlignRule(last.Value, bp);
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
            VerticalAlignRule rule = _rules[i];
            string cls = rule.Value switch
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
            VerticalAlignRule rule = _rules[i];
            string val = rule.Value;
            if (string.IsNullOrEmpty(val))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("vertical-align: ");
            sb.Append(val);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}

 
