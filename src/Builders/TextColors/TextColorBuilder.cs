using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text color builder.
/// Produces Bootstrap text color utility classes.
/// </summary>
public sealed class TextColorBuilder : ICssBuilder
{
    private readonly List<TextColorRule> _rules = new(4);

    internal TextColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextColorRule(value, breakpoint));
    }

    internal TextColorBuilder(List<TextColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }
    
    public TextColorBuilder Primary => ChainValue("primary");
    public TextColorBuilder Secondary => ChainValue("secondary");
    public TextColorBuilder Success => ChainValue("success");
    public TextColorBuilder Danger => ChainValue("danger");
    public TextColorBuilder Warning => ChainValue("warning");
    public TextColorBuilder Info => ChainValue("info");
    public TextColorBuilder Light => ChainValue("light");
    public TextColorBuilder Dark => ChainValue("dark");
    public TextColorBuilder Muted => ChainValue("muted");
    public TextColorBuilder White => ChainValue("white");
    public TextColorBuilder Black => ChainValue("black");
    public TextColorBuilder Body => ChainValue("body");
    public TextColorBuilder BodySecondary => ChainValue("body-secondary");
    public TextColorBuilder BodyTertiary => ChainValue("body-tertiary");
    public TextColorBuilder BodyEmphasis => ChainValue("body-emphasis");
    public TextColorBuilder BodyHighlight => ChainValue("body-highlight");
    public TextColorBuilder BodyMuted => ChainValue("body-muted");
    public TextColorBuilder BodyReset => ChainValue("body-reset");
    public TextColorBuilder BodyInverse => ChainValue("body-inverse");
    public TextColorBuilder BodyInverseSecondary => ChainValue("body-inverse-secondary");
    public TextColorBuilder BodyInverseTertiary => ChainValue("body-inverse-tertiary");
    public TextColorBuilder BodyInverseEmphasis => ChainValue("body-inverse-emphasis");
    public TextColorBuilder BodyInverseHighlight => ChainValue("body-inverse-highlight");
    public TextColorBuilder BodyInverseMuted => ChainValue("body-inverse-muted");
    public TextColorBuilder BodyInverseReset => ChainValue("body-inverse-reset");

    // Emphasis variants
    public TextColorBuilder PrimaryEmphasis => ChainValue("primary-emphasis");
    public TextColorBuilder SecondaryEmphasis => ChainValue("secondary-emphasis");
    public TextColorBuilder SuccessEmphasis => ChainValue("success-emphasis");
    public TextColorBuilder DangerEmphasis => ChainValue("danger-emphasis");
    public TextColorBuilder WarningEmphasis => ChainValue("warning-emphasis");
    public TextColorBuilder InfoEmphasis => ChainValue("info-emphasis");
    public TextColorBuilder LightEmphasis => ChainValue("light-emphasis");
    public TextColorBuilder DarkEmphasis => ChainValue("dark-emphasis");

    // Opacity variants
    public TextColorBuilder Opacity75 => ChainValue("75");
    public TextColorBuilder Opacity50 => ChainValue("50");
    public TextColorBuilder Opacity25 => ChainValue("25");

    public TextColorBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public TextColorBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public TextColorBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public TextColorBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public TextColorBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public TextColorBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextColorBuilder ChainValue(string value)
    {
        _rules.Add(new TextColorRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextColorBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextColorRule("primary", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TextColorRule(last.Value, bp);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var css = GetStyle(rule);
            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns the string representation of the text color builder.
    /// Chooses between class and style based on whether any rules generate CSS classes.
    /// </summary>
    public override string ToString()
    {
        if (_rules.Count == 0)
            return string.Empty;

        // First try to generate classes
        var classResult = ToClass();
        if (classResult.HasContent())
            return classResult;

        // Fall back to styles if no classes were generated
        return ToStyle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(TextColorRule rule)
    {
        return rule.Value switch
        {
            "primary" => "text-primary",
            "secondary" => "text-secondary",
            "success" => "text-success",
            "danger" => "text-danger",
            "warning" => "text-warning",
            "info" => "text-info",
            "light" => "text-light",
            "dark" => "text-dark",
            "muted" => "text-muted",
            "white" => "text-white",
            "black" => "text-black",
            "body" => "text-body",
            "body-secondary" => "text-body-secondary",
            "body-tertiary" => "text-body-tertiary",
            "body-emphasis" => "text-body-emphasis",
            "body-highlight" => "text-body-highlight",
            "body-muted" => "text-body-muted",
            "body-reset" => "text-body-reset",
            "body-inverse" => "text-body-inverse",
            "body-inverse-secondary" => "text-body-inverse-secondary",
            "body-inverse-tertiary" => "text-body-inverse-tertiary",
            "body-inverse-emphasis" => "text-body-inverse-emphasis",
            "body-inverse-highlight" => "text-body-inverse-highlight",
            "body-inverse-muted" => "text-body-inverse-muted",
            "body-inverse-reset" => "text-body-inverse-reset",
            "primary-emphasis" => "text-primary-emphasis",
            "secondary-emphasis" => "text-secondary-emphasis",
            "success-emphasis" => "text-success-emphasis",
            "danger-emphasis" => "text-danger-emphasis",
            "warning-emphasis" => "text-warning-emphasis",
            "info-emphasis" => "text-info-emphasis",
            "light-emphasis" => "text-light-emphasis",
            "dark-emphasis" => "text-dark-emphasis",
            "75" => "-75",
            "50" => "-50",
            "25" => "-25",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(TextColorRule rule)
    {
        // Text colors should always generate classes, not styles
        return null;
    }

}
