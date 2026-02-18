using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance border color builder.
/// Produces border color utility classes.
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
    
    /// <summary>
    /// Sets the border color to primary.
    /// </summary>
    public BorderColorBuilder Primary => ChainValue("primary");

    /// <summary>
    /// Sets the border color to secondary.
    /// </summary>
    public BorderColorBuilder Secondary => ChainValue("secondary");

    /// <summary>
    /// Sets the border color to success.
    /// </summary>
    public BorderColorBuilder Success => ChainValue("success");

    /// <summary>
    /// Sets the border color to danger.
    /// </summary>
    public BorderColorBuilder Danger => ChainValue("danger");

    /// <summary>
    /// Sets the border color to warning.
    /// </summary>
    public BorderColorBuilder Warning => ChainValue("warning");

    /// <summary>
    /// Sets the border color to info.
    /// </summary>
    public BorderColorBuilder Info => ChainValue("info");

    /// <summary>
    /// Sets the border color to light.
    /// </summary>
    public BorderColorBuilder Light => ChainValue("light");

    /// <summary>
    /// Sets the border color to dark.
    /// </summary>
    public BorderColorBuilder Dark => ChainValue("dark");

    /// <summary>
    /// Sets the border color to muted.
    /// </summary>
    public BorderColorBuilder Muted => ChainValue("muted");

    /// <summary>
    /// Sets the border color to white.
    /// </summary>
    public BorderColorBuilder White => ChainValue("white");

    /// <summary>
    /// Sets the border color to black.
    /// </summary>
    public BorderColorBuilder Black => ChainValue("black");

    /// <summary>
    /// Sets the border color to body.
    /// </summary>
    public BorderColorBuilder Body => ChainValue("body");

    /// <summary>
    /// Sets the border color to body-secondary.
    /// </summary>
    public BorderColorBuilder BodySecondary => ChainValue("body-secondary");

    /// <summary>
    /// Sets the border color to body-tertiary.
    /// </summary>
    public BorderColorBuilder BodyTertiary => ChainValue("body-tertiary");

    /// <summary>
    /// Sets the border color to body-emphasis.
    /// </summary>
    public BorderColorBuilder BodyEmphasis => ChainValue("body-emphasis");

    /// <summary>
    /// Sets the border color to body-highlight.
    /// </summary>
    public BorderColorBuilder BodyHighlight => ChainValue("body-highlight");

    /// <summary>
    /// Sets the border color to body-muted.
    /// </summary>
    public BorderColorBuilder BodyMuted => ChainValue("body-muted");

    /// <summary>
    /// Sets the border color to body-reset.
    /// </summary>
    public BorderColorBuilder BodyReset => ChainValue("body-reset");

    /// <summary>
    /// Sets the border color to body-inverse.
    /// </summary>
    public BorderColorBuilder BodyInverse => ChainValue("body-inverse");

    /// <summary>
    /// Sets the border color to body-inverse-secondary.
    /// </summary>
    public BorderColorBuilder BodyInverseSecondary => ChainValue("body-inverse-secondary");

    /// <summary>
    /// Sets the border color to body-inverse-tertiary.
    /// </summary>
    public BorderColorBuilder BodyInverseTertiary => ChainValue("body-inverse-tertiary");

    /// <summary>
    /// Sets the border color to body-inverse-emphasis.
    /// </summary>
    public BorderColorBuilder BodyInverseEmphasis => ChainValue("body-inverse-emphasis");

    /// <summary>
    /// Sets the border color to body-inverse-highlight.
    /// </summary>
    public BorderColorBuilder BodyInverseHighlight => ChainValue("body-inverse-highlight");

    /// <summary>
    /// Sets the border color to body-inverse-muted.
    /// </summary>
    public BorderColorBuilder BodyInverseMuted => ChainValue("body-inverse-muted");

    /// <summary>
    /// Sets the border color to body-inverse-reset.
    /// </summary>
    public BorderColorBuilder BodyInverseReset => ChainValue("body-inverse-reset");

    // Opacity variants
    /// <summary>
    /// Sets the border color opacity to 75%.
    /// </summary>
    public BorderColorBuilder Opacity75 => ChainValue("75");

    /// <summary>
    /// Sets the border color opacity to 50%.
    /// </summary>
    public BorderColorBuilder Opacity50 => ChainValue("50");

    /// <summary>
    /// Sets the border color opacity to 25%.
    /// </summary>
    public BorderColorBuilder Opacity25 => ChainValue("25");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderColorBuilder ChainValue(string value)
    {
        _rules.Add(new BorderColorRule(value, null));
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
            BorderColorRule rule = _rules[i];
            string cls = GetClass(rule);
            if (cls.Length == 0)
                continue;

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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            BorderColorRule rule = _rules[i];
            string? css = GetStyle(rule);
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
        string classResult = ToClass();
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
            "75" => "border-opacity-75",
            "50" => "border-opacity-50",
            "25" => "border-opacity-25",
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
