using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance focus ring builder with fluent API for chaining focus ring rules.
/// </summary>
public sealed class FocusRingBuilder : ICssBuilder
{
    private readonly List<FocusRingRule> _rules = new(4);


    internal FocusRingBuilder(string color, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FocusRingRule(color, breakpoint));
    }

    internal FocusRingBuilder(List<FocusRingRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the focus ring color to primary.
    /// </summary>
    public FocusRingBuilder Ring => Chain("ring");
    public FocusRingBuilder Primary => Chain("primary");
    /// <summary>
    /// Sets the focus ring color to secondary.
    /// </summary>
    public FocusRingBuilder Secondary => Chain("secondary");
    /// <summary>
    /// Sets the focus ring color to success.
    /// </summary>
    public FocusRingBuilder Success => Chain("success");
    /// <summary>
    /// Sets the focus ring color to info.
    /// </summary>
    public FocusRingBuilder Info => Chain("info");
    /// <summary>
    /// Sets the focus ring color to warning.
    /// </summary>
    public FocusRingBuilder Warning => Chain("warning");
    /// <summary>
    /// Sets the focus ring color to danger.
    /// </summary>
    public FocusRingBuilder Danger => Chain("danger");
    public FocusRingBuilder Destructive => Chain("destructive");
    public FocusRingBuilder Muted => Chain("muted");
    public FocusRingBuilder Accent => Chain("accent");
    /// <summary>
    /// Sets the focus ring color to light.
    /// </summary>
    public FocusRingBuilder Light => Chain("light");
    /// <summary>
    /// Sets the focus ring color to dark.
    /// </summary>
    public FocusRingBuilder Dark => Chain("dark");

    /// <summary>
    /// Applies the focus ring on phone breakpoint.
    /// </summary>
    public FocusRingBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the focus ring on tablet breakpoint.
    /// </summary>
    public FocusRingBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the focus ring on laptop breakpoint.
    /// </summary>
    public FocusRingBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the focus ring on desktop breakpoint.
    /// </summary>
    public FocusRingBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the focus ring on widescreen breakpoint.
    /// </summary>
    public FocusRingBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the focus ring on ultrawide breakpoint.
    /// </summary>
    public FocusRingBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

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
            var cls = rule.Color switch
            {
                "ring" => "focus-visible:ring-ring",
                "primary" => "focus-visible:ring-primary",
                "secondary" => "focus-visible:ring-secondary",
                "destructive" => "focus-visible:ring-destructive",
                "muted" => "focus-visible:ring-muted",
                "accent" => "focus-visible:ring-accent",
                "success" => "focus-visible:ring-success",
                "info" => "focus-visible:ring-info",
                "warning" => "focus-visible:ring-warning",
                "danger" => "focus-visible:ring-destructive",
                "light" => "focus-visible:ring-white",
                "dark" => "focus-visible:ring-black",
                _ when rule.Color.StartsWith("focus-visible:ring-") => rule.Color,
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
    /// Focus rings are class-based in shadcn/Tailwind.
    /// </summary>
    public string ToStyle() => string.Empty;

}

 







