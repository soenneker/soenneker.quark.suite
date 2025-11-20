using System;
using System.Collections.Generic;
using Soenneker.Extensions.String;
using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap gutter utilities.
/// </summary>
public sealed class GutterBuilder : ICssBuilder
{
    private readonly List<GutterRule> _rules = new(4);

    /// <summary>
    /// Gets a value indicating whether the builder is empty (no gutter rules set).
    /// </summary>
    public bool IsEmpty => _rules.Count == 0;
    /// <summary>
    /// Gets a value indicating whether this builder generates CSS classes (always true).
    /// </summary>
    public bool IsCssClass => true;
    /// <summary>
    /// Gets a value indicating whether this builder generates inline styles (always false).
    /// </summary>
    public bool IsCssStyle => false;

    internal GutterBuilder()
    {
    }

    internal GutterBuilder(List<GutterRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // Gutter type methods
    /// <summary>
    /// Sets the horizontal (X) gutter value.
    /// </summary>
    /// <param name="value">The horizontal gutter value (0-5).</param>
    /// <returns>The builder instance for method chaining.</returns>
    public GutterBuilder X(int value)
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(Math.Clamp(value, 0, 5), GutterType.X, null));
        return new GutterBuilder(newRules);
    }

    /// <summary>
    /// Sets the vertical (Y) gutter value.
    /// </summary>
    /// <param name="value">The vertical gutter value (0-5).</param>
    /// <returns>The builder instance for method chaining.</returns>
    public GutterBuilder Y(int value)
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(Math.Clamp(value, 0, 5), GutterType.Y, null));
        return new GutterBuilder(newRules);
    }

    /// <summary>
    /// Sets both horizontal and vertical gutter values.
    /// </summary>
    /// <param name="value">The gutter value for both axes (0-5).</param>
    /// <returns>The builder instance for method chaining.</returns>
    public GutterBuilder All(int value)
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(Math.Clamp(value, 0, 5), GutterType.All, null));
        return new GutterBuilder(newRules);
    }

    /// <summary>
    /// Sets the gutter to none (0) for all axes.
    /// </summary>
    /// <returns>The builder instance for method chaining.</returns>
    public GutterBuilder None()
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(0, GutterType.All, null));
        return new GutterBuilder(newRules);
    }

    // Breakpoint chaining methods
    /// <summary>
    /// Applies the gutter on phone (small) breakpoint.
    /// </summary>
    public GutterBuilder OnPhone => ChainBp(GutterBreakpoint.Sm);
    /// <summary>
    /// Applies the gutter on tablet (medium) breakpoint.
    /// </summary>
    public GutterBuilder OnTablet => ChainBp(GutterBreakpoint.Md);
    /// <summary>
    /// Applies the gutter on laptop (large) breakpoint.
    /// </summary>
    public GutterBuilder OnLaptop => ChainBp(GutterBreakpoint.Lg);
    /// <summary>
    /// Applies the gutter on desktop (extra large) breakpoint.
    /// </summary>
    public GutterBuilder OnDesktop => ChainBp(GutterBreakpoint.Xl);
    /// <summary>
    /// Applies the gutter on widescreen (extra extra large) breakpoint.
    /// </summary>
    public GutterBuilder OnWidescreen => ChainBp(GutterBreakpoint.Xxl);

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private GutterBuilder ChainBp(GutterBreakpoint breakpoint)
    {
        var newRules = new List<GutterRule>(_rules)
        {
            // Add a rule with the breakpoint but no specific type yet
            new(1, GutterType.All, breakpoint)
        };

        return new GutterBuilder(newRules);
    }

    /// <summary>
    /// Returns the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string, or an empty string if no gutter rules are set.</returns>
    public override string ToString()
    {
        if (_rules.Count == 0)
            return string.Empty;

        // Deduplicate by (Type, Breakpoint) - last one wins
        var dedupedRules = new Dictionary<(GutterType, GutterBreakpoint?), GutterRule>();

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var key = (rule.Type, rule.Breakpoint);
            dedupedRules[key] = rule;
        }

        var classes = new List<string>();
        foreach (var rule in dedupedRules.Values)
        {
            var className = GetGutterClass(rule);
            if (!className.IsNullOrEmpty())
                classes.Add(className);
        }

        return string.Join(" ", classes);
    }

    private static string GetGutterClass(GutterRule rule)
    {
        // Map axis
        var prefix = rule.Type.Value switch
        {
            GutterType.AllValue => "g",
            GutterType.XValue => "gx",
            GutterType.YValue => "gy",
            _ => string.Empty
        };
        if (prefix.IsNullOrEmpty())
            return string.Empty;

        // Map breakpoint (no xs breakpoint in Bootstrap)
        var breakpoint = rule.Breakpoint?.Value switch
        {
            GutterBreakpoint.SmValue => "-sm",
            GutterBreakpoint.MdValue => "-md",
            GutterBreakpoint.LgValue => "-lg",
            GutterBreakpoint.XlValue => "-xl",
            GutterBreakpoint.XxlValue => "-xxl",
            _ => string.Empty // base (mobile)
        };

        return $"{prefix}{breakpoint}-{rule.Value}";
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string, or an empty string if no gutter rules are set.</returns>
    public string ToClass() => ToString();
    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// Gutters are typically handled via Bootstrap classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as gutters are handled via classes.</returns>
    public string ToStyle() => string.Empty;
}