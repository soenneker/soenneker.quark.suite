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

    public bool IsEmpty => _rules.Count == 0;
    public bool IsCssClass => true;
    public bool IsCssStyle => false;

    internal GutterBuilder() { }

    internal GutterBuilder(List<GutterRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    // Gutter type methods
    public GutterBuilder X(int value)
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(Math.Clamp(value, 0, 5), GutterType.X, null));
        return new GutterBuilder(newRules);
    }

    public GutterBuilder Y(int value)
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(Math.Clamp(value, 0, 5), GutterType.Y, null));
        return new GutterBuilder(newRules);
    }

    public GutterBuilder All(int value)
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(Math.Clamp(value, 0, 5), GutterType.All, null));
        return new GutterBuilder(newRules);
    }

    public GutterBuilder None()
    {
        var newRules = new List<GutterRule>(_rules);
        newRules.Add(new GutterRule(0, GutterType.All, null));
        return new GutterBuilder(newRules);
    }

    // Breakpoint chaining methods
    public GutterBuilder OnPhone => ChainBp(GutterBreakpoint.Sm);
    public GutterBuilder OnTablet => ChainBp(GutterBreakpoint.Md);
    public GutterBuilder OnLaptop => ChainBp(GutterBreakpoint.Lg);
    public GutterBuilder OnDesktop => ChainBp(GutterBreakpoint.Xl);
    public GutterBuilder OnWidescreen => ChainBp(GutterBreakpoint.Xxl);

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private GutterBuilder ChainBp(GutterBreakpoint breakpoint)
    {
        var newRules = new List<GutterRule>(_rules);
        // Add a rule with the breakpoint but no specific type yet
        newRules.Add(new GutterRule(1, GutterType.All, breakpoint));
        return new GutterBuilder(newRules);
    }

    public override string ToString()
    {
        if (_rules.Count == 0) return string.Empty;

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
        if (prefix.IsNullOrEmpty()) return string.Empty;

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

    public string ToClass() => ToString();
    public string ToStyle() => string.Empty;
}