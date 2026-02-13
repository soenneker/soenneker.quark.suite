using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance box shadow builder with fluent API for chaining box shadow rules.
/// </summary>
public sealed class BoxShadowBuilder : ICssBuilder
{
    private readonly List<BoxShadowRule> _rules = new(4);

    // ----- Class name constants -----
    private const string _classNone = "shadow-none";
    private const string _classBase = "shadow";
    private const string _classSm = "shadow-sm";
    private const string _classLg = "shadow-lg";

    internal BoxShadowBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BoxShadowRule(value, breakpoint));
    }

    internal BoxShadowBuilder(List<BoxShadowRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the box shadow to none.
    /// </summary>
    public BoxShadowBuilder None => Chain("none");
    /// <summary>
    /// Sets the box shadow to base.
    /// </summary>
    public BoxShadowBuilder Base => Chain("base");
    /// <summary>
    /// Sets the box shadow to small.
    /// </summary>
    public BoxShadowBuilder Small => Chain(SizeType.Small.Value);
    /// <summary>
    /// Sets the box shadow to large.
    /// </summary>
    public BoxShadowBuilder Large => Chain(SizeType.Large.Value);

    /// <summary>
    /// Applies the box shadow on phone breakpoint.
    /// </summary>
    public BoxShadowBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the box shadow on tablet breakpoint.
    /// </summary>
    public BoxShadowBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the box shadow on laptop breakpoint.
    /// </summary>
    public BoxShadowBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the box shadow on desktop breakpoint.
    /// </summary>
    public BoxShadowBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the box shadow on widescreen breakpoint.
    /// </summary>
    public BoxShadowBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the box shadow on ultrawide breakpoint.
    /// </summary>
    public BoxShadowBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BoxShadowBuilder Chain(string value)
    {
        _rules.Add(new BoxShadowRule(value, null));
        return this;
    }

    /// <summary>Apply a BreakpointType to the most recent rule (or bootstrap with "base" if empty).</summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BoxShadowBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BoxShadowRule("base", bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        BoxShadowRule last = _rules[lastIdx];
        _rules[lastIdx] = new BoxShadowRule(last.Value, bp);
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
            BoxShadowRule rule = _rules[i];

            string baseClass = rule.Value switch
            {
                "none" => _classNone,
                "base" => _classBase,
                "sm" => _classSm,
                "lg" => _classLg,
                _ => string.Empty
            };

            if (baseClass.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                baseClass = BreakpointUtil.InsertBreakpointType(baseClass, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(baseClass);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// Shadow utilities are class-first; no inline style mapping.
    /// </summary>
    /// <returns>An empty string as shadow utilities are class-first.</returns>
    public string ToStyle()
    {
        // Shadow utilities are class-first; no inline style mapping
        return string.Empty;
    }


}
