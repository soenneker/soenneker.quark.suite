using Soenneker.Quark.Attributes;
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
[TailwindPrefix("border-", Responsive = true)]
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
    /// Sets the border color to primary-foreground.
    /// </summary>
    public BorderColorBuilder PrimaryForeground => ChainValue("primary-foreground");

    /// <summary>
    /// Sets the border color to secondary-foreground.
    /// </summary>
    public BorderColorBuilder SecondaryForeground => ChainValue("secondary-foreground");

    /// <summary>
    /// Sets the border color to destructive.
    /// </summary>
    public BorderColorBuilder Destructive => ChainValue("destructive");

    /// <summary>
    /// Sets the border color to destructive-foreground.
    /// </summary>
    public BorderColorBuilder DestructiveForeground => ChainValue("destructive-foreground");

    /// <summary>
    /// Sets the border color to muted-foreground.
    /// </summary>
    public BorderColorBuilder MutedForeground => ChainValue("muted-foreground");

    /// <summary>
    /// Sets the border color to accent.
    /// </summary>
    public BorderColorBuilder Accent => ChainValue("accent");

    /// <summary>
    /// Sets the border color to accent-foreground.
    /// </summary>
    public BorderColorBuilder AccentForeground => ChainValue("accent-foreground");

    /// <summary>
    /// Sets the border color to popover.
    /// </summary>
    public BorderColorBuilder Popover => ChainValue("popover");

    /// <summary>
    /// Sets the border color to popover-foreground.
    /// </summary>
    public BorderColorBuilder PopoverForeground => ChainValue("popover-foreground");

    /// <summary>
    /// Sets the border color to card.
    /// </summary>
    public BorderColorBuilder Card => ChainValue("card");

    /// <summary>
    /// Sets the border color to card-foreground.
    /// </summary>
    public BorderColorBuilder CardForeground => ChainValue("card-foreground");

    /// <summary>
    /// Sets the border color to background.
    /// </summary>
    public BorderColorBuilder Background => ChainValue("background");

    /// <summary>
    /// Sets the border color to foreground.
    /// </summary>
    public BorderColorBuilder Foreground => ChainValue("foreground");

    /// <summary>
    /// Sets the border color to border token.
    /// </summary>
    public BorderColorBuilder Border => ChainValue("border");

    /// <summary>
    /// Sets the border color to input.
    /// </summary>
    public BorderColorBuilder Input => ChainValue("input");

    /// <summary>
    /// Sets the border color to ring.
    /// </summary>
    public BorderColorBuilder Ring => ChainValue("ring");

    /// <summary>
    /// Sets the border color to success.
    /// </summary>
    public BorderColorBuilder Success => ChainValue("success");

    /// <summary>
    /// Sets the border color to warning.
    /// </summary>
    public BorderColorBuilder Warning => ChainValue("warning");

    /// <summary>
    /// Sets the border color to info.
    /// </summary>
    public BorderColorBuilder Info => ChainValue("info");

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
            "primary-foreground" => "border-primary-foreground",
            "secondary" => "border-secondary",
            "secondary-foreground" => "border-secondary-foreground",
            "destructive" => "border-destructive",
            "destructive-foreground" => "border-destructive-foreground",
            "muted-foreground" => "border-muted-foreground",
            "accent" => "border-accent",
            "accent-foreground" => "border-accent-foreground",
            "popover" => "border-popover",
            "popover-foreground" => "border-popover-foreground",
            "card" => "border-card",
            "card-foreground" => "border-card-foreground",
            "background" => "border-background",
            "foreground" => "border-foreground",
            "border" => "border-border",
            "input" => "border-input",
            "ring" => "border-ring",
            "success" => "border-success",
            "warning" => "border-warning",
            "info" => "border-info",
            "muted" => "border-muted",
            "white" => "border-white",
            "black" => "border-black",
            _ when rule.Value.StartsWith("border-") => rule.Value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(BorderColorRule rule)
    {
        return GetClass(rule).Length != 0 ? null : rule.Value;
    }

}
