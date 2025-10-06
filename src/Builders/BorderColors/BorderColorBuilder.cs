using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance border color builder.
/// Produces Bootstrap border color utility classes.
/// </summary>
public sealed class BorderColorBuilder : ICssBuilder
{
    private readonly List<BorderColorRule> _rules = new(4);

    internal BorderColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BorderColorRule(value, breakpoint));
    }

    internal BorderColorBuilder(List<BorderColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }
    
    public BorderColorBuilder Primary => ChainValue("primary");
    public BorderColorBuilder Secondary => ChainValue("secondary");
    public BorderColorBuilder Success => ChainValue("success");
    public BorderColorBuilder Danger => ChainValue("danger");
    public BorderColorBuilder Warning => ChainValue("warning");
    public BorderColorBuilder Info => ChainValue("info");
    public BorderColorBuilder Light => ChainValue("light");
    public BorderColorBuilder Dark => ChainValue("dark");
    public BorderColorBuilder Muted => ChainValue("muted");
    public BorderColorBuilder White => ChainValue("white");
    public BorderColorBuilder Black => ChainValue("black");
    public BorderColorBuilder Body => ChainValue("body");
    public BorderColorBuilder BodySecondary => ChainValue("body-secondary");
    public BorderColorBuilder BodyTertiary => ChainValue("body-tertiary");
    public BorderColorBuilder BodyEmphasis => ChainValue("body-emphasis");
    public BorderColorBuilder BodyHighlight => ChainValue("body-highlight");
    public BorderColorBuilder BodyMuted => ChainValue("body-muted");
    public BorderColorBuilder BodyReset => ChainValue("body-reset");
    public BorderColorBuilder BodyInverse => ChainValue("body-inverse");
    public BorderColorBuilder BodyInverseSecondary => ChainValue("body-inverse-secondary");
    public BorderColorBuilder BodyInverseTertiary => ChainValue("body-inverse-tertiary");
    public BorderColorBuilder BodyInverseEmphasis => ChainValue("body-inverse-emphasis");
    public BorderColorBuilder BodyInverseHighlight => ChainValue("body-inverse-highlight");
    public BorderColorBuilder BodyInverseMuted => ChainValue("body-inverse-muted");
    public BorderColorBuilder BodyInverseReset => ChainValue("body-inverse-reset");

    // Opacity variants
    public BorderColorBuilder Opacity75 => ChainValue("75");
    public BorderColorBuilder Opacity50 => ChainValue("50");
    public BorderColorBuilder Opacity25 => ChainValue("25");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderColorBuilder ChainValue(string value)
    {
        _rules.Add(new BorderColorRule(value, null));
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
    /// Returns the string representation of the border color builder.
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
    private static string GetClass(BorderColorRule rule)
    {
        return rule.Value switch
        {
            "primary" => "border-primary",
            "secondary" => "border-secondary",
            "success" => "border-success",
            "danger" => "border-danger",
            "warning" => "border-warning",
            "info" => "border-info",
            "light" => "border-light",
            "dark" => "border-dark",
            "muted" => "border-muted",
            "white" => "border-white",
            "black" => "border-black",
            "body" => "border-body",
            "body-secondary" => "border-body-secondary",
            "body-tertiary" => "border-body-tertiary",
            "body-emphasis" => "border-body-emphasis",
            "body-highlight" => "border-body-highlight",
            "body-muted" => "border-body-muted",
            "body-reset" => "border-body-reset",
            "body-inverse" => "border-body-inverse",
            "body-inverse-secondary" => "border-body-inverse-secondary",
            "body-inverse-tertiary" => "border-body-inverse-tertiary",
            "body-inverse-emphasis" => "border-body-inverse-emphasis",
            "body-inverse-highlight" => "border-body-inverse-highlight",
            "body-inverse-muted" => "border-body-inverse-muted",
            "body-inverse-reset" => "border-body-inverse-reset",
            "75" => "-75",
            "50" => "-50",
            "25" => "-25",
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(BorderColorRule rule)
    {
        // Border colors should always generate classes, not styles
        return null;
    }

}
