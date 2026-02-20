using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance line height builder with fluent API for chaining line height rules.
/// </summary>
public sealed class LineHeightBuilder : ICssBuilder
{
    private readonly List<LineHeightRule> _rules = new(4);

    // Tailwind leading (line-height) utilities (for Quark Suite / shadcn)
    private const string _classLh1 = "leading-none";
    private const string _classLhSm = "leading-snug";
    private const string _classLhBase = "leading-normal";
    private const string _classLhLg = "leading-relaxed";

    internal LineHeightBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new LineHeightRule(value, breakpoint));
    }

    internal LineHeightBuilder(List<LineHeightRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the line height to 1.
    /// </summary>
    public LineHeightBuilder Is1 => Chain(ScaleType.Is1.Value);
    /// <summary>
    /// Sets the line height to small.
    /// </summary>
    public LineHeightBuilder Small => Chain(SizeType.Small.Value);
    /// <summary>
    /// Sets the line height to base.
    /// </summary>
    public LineHeightBuilder Base => Chain("base");
    /// <summary>
    /// Sets the line height to large.
    /// </summary>
    public LineHeightBuilder Large => Chain(SizeType.Large.Value);

    /// <summary>
    /// Applies the line height on phone breakpoint.
    /// </summary>
    public LineHeightBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the line height on tablet breakpoint.
    /// </summary>
    public LineHeightBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the line height on laptop breakpoint.
    /// </summary>
    public LineHeightBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the line height on desktop breakpoint.
    /// </summary>
    public LineHeightBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the line height on widescreen breakpoint.
    /// </summary>
    public LineHeightBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the line height on ultrawide breakpoint.
    /// </summary>
    public LineHeightBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LineHeightBuilder Chain(string value)
    {
        _rules.Add(new LineHeightRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LineHeightBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new LineHeightRule("base", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new LineHeightRule(last.Value, bp);
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
            var cls = rule.Value switch
            {
                ScaleType.Is1Value => _classLh1,
                "sm" => _classLhSm,
                "base" => _classLhBase,
                "lg" => _classLhLg,
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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var css = rule.Value switch
            {
                ScaleType.Is1Value => "1",
                "sm" => "1.25",
                "base" => "1.5",
                "lg" => "2",
                _ => rule.Value
            };

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("line-height: ");
            sb.Append(css);
        }
        return sb.ToString();
    }


}

 
