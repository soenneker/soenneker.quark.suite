using System;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text background combination builder.
/// Produces Bootstrap text-bg utility classes that set both text and background colors.
/// </summary>
public sealed class TextBackgroundBuilder : ICssBuilder
{
    private readonly List<TextBackgroundRule> _rules = new(4);

    internal TextBackgroundBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextBackgroundRule(value, breakpoint));
    }

    internal TextBackgroundBuilder(List<TextBackgroundRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }
    
    public TextBackgroundBuilder Primary => ChainValue("primary");
    public TextBackgroundBuilder Secondary => ChainValue("secondary");
    public TextBackgroundBuilder Success => ChainValue("success");
    public TextBackgroundBuilder Danger => ChainValue("danger");
    public TextBackgroundBuilder Warning => ChainValue("warning");
    public TextBackgroundBuilder Info => ChainValue("info");
    public TextBackgroundBuilder Light => ChainValue("light");
    public TextBackgroundBuilder Dark => ChainValue("dark");
    public TextBackgroundBuilder Body => ChainValue("body");
    public TextBackgroundBuilder BodySecondary => ChainValue("body-secondary");
    public TextBackgroundBuilder BodyTertiary => ChainValue("body-tertiary");

    public TextBackgroundBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextBackgroundBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextBackgroundBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextBackgroundBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextBackgroundBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextBackgroundBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextBackgroundBuilder ChainValue(string value)
    {
        _rules.Add(new TextBackgroundRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextBackgroundBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextBackgroundRule("primary", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TextBackgroundRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = GetClass(rule);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        // Text-bg classes should always use Bootstrap classes, not inline styles
        return string.Empty;
    }

    /// <summary>
    /// Returns the string representation of the text background builder.
    /// </summary>
    public override string ToString()
    {
        return ToClass();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(TextBackgroundRule rule)
    {
        return rule.Value switch
        {
            "primary" => "text-bg-primary",
            "secondary" => "text-bg-secondary",
            "success" => "text-bg-success",
            "danger" => "text-bg-danger",
            "warning" => "text-bg-warning",
            "info" => "text-bg-info",
            "light" => "text-bg-light",
            "dark" => "text-bg-dark",
            "body" => "text-bg-body",
            "body-secondary" => "text-bg-body-secondary",
            "body-tertiary" => "text-bg-body-tertiary",
            _ => string.Empty
        };
    }

}

