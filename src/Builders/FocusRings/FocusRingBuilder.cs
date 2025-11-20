using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class FocusRingBuilder : ICssBuilder
{
    private readonly List<FocusRingRule> _rules = new(4);

    private const string _classPrimary = "focus-ring-primary";
    private const string _classSecondary = "focus-ring-secondary";
    private const string _classSuccess = "focus-ring-success";
    private const string _classInfo = "focus-ring-info";
    private const string _classWarning = "focus-ring-warning";
    private const string _classDanger = "focus-ring-danger";
    private const string _classLight = "focus-ring-light";
    private const string _classDark = "focus-ring-dark";

    internal FocusRingBuilder(string color, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FocusRingRule(color, breakpoint));
    }

    internal FocusRingBuilder(List<FocusRingRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public FocusRingBuilder Primary => Chain("primary");
    public FocusRingBuilder Secondary => Chain("secondary");
    public FocusRingBuilder Success => Chain("success");
    public FocusRingBuilder Info => Chain("info");
    public FocusRingBuilder Warning => Chain("warning");
    public FocusRingBuilder Danger => Chain("danger");
    public FocusRingBuilder Light => Chain("light");
    public FocusRingBuilder Dark => Chain("dark");

    public FocusRingBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public FocusRingBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public FocusRingBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public FocusRingBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public FocusRingBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public FocusRingBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FocusRingBuilder Chain(string color)
    {
        _rules.Add(new FocusRingRule(color, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FocusRingBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FocusRingRule("primary", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new FocusRingRule(last.Color, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Color switch
            {
                "primary" => _classPrimary,
                "secondary" => _classSecondary,
                "success" => _classSuccess,
                "info" => _classInfo,
                "warning" => _classWarning,
                "danger" => _classDanger,
                "light" => _classLight,
                "dark" => _classDark,
                _ => string.Empty
            };
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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cssVar = GetCssVariable(rule.Color);
            if (cssVar is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("--bs-focus-ring-color: ");
            sb.Append(cssVar);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetCssVariable(string color)
    {
        return color switch
        {
            "primary" => "var(--bs-primary-rgb)",
            "secondary" => "var(--bs-secondary-rgb)",
            "success" => "var(--bs-success-rgb)",
            "info" => "var(--bs-info-rgb)",
            "warning" => "var(--bs-warning-rgb)",
            "danger" => "var(--bs-danger-rgb)",
            "light" => "var(--bs-light-rgb)",
            "dark" => "var(--bs-dark-rgb)",
            _ => null
        };
    }

}

 






