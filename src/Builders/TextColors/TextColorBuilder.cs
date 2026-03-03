using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text color builder.
/// Produces text color utility classes.
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
    
	/// <summary>
	/// Sets the text color to primary.
	/// </summary>
    public TextColorBuilder Primary => ChainValue("primary");
	/// <summary>
	/// Sets the text color to secondary.
	/// </summary>
    public TextColorBuilder Secondary => ChainValue("secondary");
	/// <summary>
	/// Sets the text color to primary-foreground.
	/// </summary>
    public TextColorBuilder PrimaryForeground => ChainValue("primary-foreground");
	/// <summary>
	/// Sets the text color to secondary-foreground.
	/// </summary>
    public TextColorBuilder SecondaryForeground => ChainValue("secondary-foreground");
	/// <summary>
	/// Sets the text color to destructive.
	/// </summary>
    public TextColorBuilder Destructive => ChainValue("destructive");
	/// <summary>
	/// Sets the text color to destructive-foreground.
	/// </summary>
    public TextColorBuilder DestructiveForeground => ChainValue("destructive-foreground");
	/// <summary>
	/// Sets the text color to muted-foreground.
	/// </summary>
    public TextColorBuilder MutedForeground => ChainValue("muted-foreground");
	/// <summary>
	/// Sets the text color to accent.
	/// </summary>
    public TextColorBuilder Accent => ChainValue("accent");
	/// <summary>
	/// Sets the text color to accent-foreground.
	/// </summary>
    public TextColorBuilder AccentForeground => ChainValue("accent-foreground");
	/// <summary>
	/// Sets the text color to popover.
	/// </summary>
    public TextColorBuilder Popover => ChainValue("popover");
	/// <summary>
	/// Sets the text color to popover-foreground.
	/// </summary>
    public TextColorBuilder PopoverForeground => ChainValue("popover-foreground");
	/// <summary>
	/// Sets the text color to card.
	/// </summary>
    public TextColorBuilder Card => ChainValue("card");
	/// <summary>
	/// Sets the text color to card-foreground.
	/// </summary>
    public TextColorBuilder CardForeground => ChainValue("card-foreground");
	/// <summary>
	/// Sets the text color to background.
	/// </summary>
    public TextColorBuilder Background => ChainValue("background");
	/// <summary>
	/// Sets the text color to foreground.
	/// </summary>
    public TextColorBuilder Foreground => ChainValue("foreground");
	/// <summary>
	/// Sets the text color to border.
	/// </summary>
    public TextColorBuilder Border => ChainValue("border");
	/// <summary>
	/// Sets the text color to input.
	/// </summary>
    public TextColorBuilder Input => ChainValue("input");
	/// <summary>
	/// Sets the text color to ring.
	/// </summary>
    public TextColorBuilder Ring => ChainValue("ring");
	/// <summary>
	/// Sets the text color to success.
	/// </summary>
    public TextColorBuilder Success => ChainValue("success");
	/// <summary>
	/// Sets the text color to danger.
	/// </summary>
    public TextColorBuilder Danger => ChainValue("danger");
	/// <summary>
	/// Sets the text color to warning.
	/// </summary>
    public TextColorBuilder Warning => ChainValue("warning");
	/// <summary>
	/// Sets the text color to info.
	/// </summary>
    public TextColorBuilder Info => ChainValue("info");
	/// <summary>
	/// Sets the text color to light.
	/// </summary>
    public TextColorBuilder Light => ChainValue("light");
	/// <summary>
	/// Sets the text color to dark.
	/// </summary>
    public TextColorBuilder Dark => ChainValue("dark");
	/// <summary>
	/// Sets the text color to muted.
	/// </summary>
    public TextColorBuilder Muted => ChainValue("muted");
	/// <summary>
	/// Sets the text color to white.
	/// </summary>
    public TextColorBuilder White => ChainValue("white");
	/// <summary>
	/// Sets the text color to black.
	/// </summary>
    public TextColorBuilder Black => ChainValue("black");
	/// <summary>
	/// Sets the text color to body.
	/// </summary>
    public TextColorBuilder Body => ChainValue("body");
	/// <summary>
	/// Sets the text color to body-secondary.
	/// </summary>
    public TextColorBuilder BodySecondary => ChainValue("body-secondary");
	/// <summary>
	/// Sets the text color to body-tertiary.
	/// </summary>
    public TextColorBuilder BodyTertiary => ChainValue("body-tertiary");
	/// <summary>
	/// Sets the text color to body-emphasis.
	/// </summary>
    public TextColorBuilder BodyEmphasis => ChainValue("body-emphasis");
	/// <summary>
	/// Sets the text color to body-highlight.
	/// </summary>
    public TextColorBuilder BodyHighlight => ChainValue("body-highlight");
	/// <summary>
	/// Sets the text color to body-muted.
	/// </summary>
    public TextColorBuilder BodyMuted => ChainValue("body-muted");
	/// <summary>
	/// Sets the text color to body-reset.
	/// </summary>
    public TextColorBuilder BodyReset => ChainValue("body-reset");
	/// <summary>
	/// Sets the text color to body-inverse.
	/// </summary>
    public TextColorBuilder BodyInverse => ChainValue("body-inverse");
	/// <summary>
	/// Sets the text color to body-inverse-secondary.
	/// </summary>
    public TextColorBuilder BodyInverseSecondary => ChainValue("body-inverse-secondary");
	/// <summary>
	/// Sets the text color to body-inverse-tertiary.
	/// </summary>
    public TextColorBuilder BodyInverseTertiary => ChainValue("body-inverse-tertiary");
	/// <summary>
	/// Sets the text color to body-inverse-emphasis.
	/// </summary>
    public TextColorBuilder BodyInverseEmphasis => ChainValue("body-inverse-emphasis");
	/// <summary>
	/// Sets the text color to body-inverse-highlight.
	/// </summary>
    public TextColorBuilder BodyInverseHighlight => ChainValue("body-inverse-highlight");
	/// <summary>
	/// Sets the text color to body-inverse-muted.
	/// </summary>
    public TextColorBuilder BodyInverseMuted => ChainValue("body-inverse-muted");
	/// <summary>
	/// Sets the text color to body-inverse-reset.
	/// </summary>
    public TextColorBuilder BodyInverseReset => ChainValue("body-inverse-reset");

	/// <summary>
	/// Sets the text color to primary-emphasis.
	/// </summary>
    public TextColorBuilder PrimaryEmphasis => ChainValue("primary-emphasis");
	/// <summary>
	/// Sets the text color to secondary-emphasis.
	/// </summary>
    public TextColorBuilder SecondaryEmphasis => ChainValue("secondary-emphasis");
	/// <summary>
	/// Sets the text color to success-emphasis.
	/// </summary>
    public TextColorBuilder SuccessEmphasis => ChainValue("success-emphasis");
	/// <summary>
	/// Sets the text color to danger-emphasis.
	/// </summary>
    public TextColorBuilder DangerEmphasis => ChainValue("danger-emphasis");
	/// <summary>
	/// Sets the text color to warning-emphasis.
	/// </summary>
    public TextColorBuilder WarningEmphasis => ChainValue("warning-emphasis");
	/// <summary>
	/// Sets the text color to info-emphasis.
	/// </summary>
    public TextColorBuilder InfoEmphasis => ChainValue("info-emphasis");
	/// <summary>
	/// Sets the text color to light-emphasis.
	/// </summary>
    public TextColorBuilder LightEmphasis => ChainValue("light-emphasis");
	/// <summary>
	/// Sets the text color to dark-emphasis.
	/// </summary>
    public TextColorBuilder DarkEmphasis => ChainValue("dark-emphasis");

	/// <summary>
	/// Sets the text color opacity to 75%.
	/// </summary>
    public TextColorBuilder Opacity75 => ChainValue("75");
	/// <summary>
	/// Sets the text color opacity to 50%.
	/// </summary>
    public TextColorBuilder Opacity50 => ChainValue("50");
	/// <summary>
	/// Sets the text color opacity to 25%.
	/// </summary>
    public TextColorBuilder Opacity25 => ChainValue("25");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextColorBuilder ChainValue(string value)
    {
        _rules.Add(new TextColorRule(value, null));
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

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
            "primary-foreground" => "text-primary-foreground",
            "secondary" => "text-secondary",
            "secondary-foreground" => "text-secondary-foreground",
            "destructive" => "text-destructive",
            "destructive-foreground" => "text-destructive-foreground",
            "muted-foreground" => "text-muted-foreground",
            "accent" => "text-accent",
            "accent-foreground" => "text-accent-foreground",
            "popover" => "text-popover",
            "popover-foreground" => "text-popover-foreground",
            "card" => "text-card",
            "card-foreground" => "text-card-foreground",
            "background" => "text-background",
            "foreground" => "text-foreground",
            "border" => "text-border",
            "input" => "text-input",
            "ring" => "text-ring",
            "success" => "text-success",
            "danger" => "text-destructive",
            "warning" => "text-warning",
            "info" => "text-info",
            "light" => "text-white",
            "dark" => "text-black",
            "muted" => "text-muted-foreground",
            "white" => "text-white",
            "black" => "text-black",
            "body" => "text-foreground",
            "body-secondary" => "text-muted-foreground",
            "body-tertiary" => "text-muted-foreground",
            "body-emphasis" => "text-foreground",
            "body-highlight" => "text-foreground",
            "body-muted" => "text-muted-foreground",
            "body-reset" => "text-foreground",
            "body-inverse" => "text-background",
            "body-inverse-secondary" => "text-secondary-foreground",
            "body-inverse-tertiary" => "text-muted-foreground",
            "body-inverse-emphasis" => "text-primary-foreground",
            "body-inverse-highlight" => "text-accent-foreground",
            "body-inverse-muted" => "text-muted-foreground",
            "body-inverse-reset" => "text-foreground",
            "primary-emphasis" => "text-primary",
            "secondary-emphasis" => "text-secondary",
            "success-emphasis" => "text-success",
            "danger-emphasis" => "text-destructive",
            "warning-emphasis" => "text-warning",
            "info-emphasis" => "text-info",
            "light-emphasis" => "text-white",
            "dark-emphasis" => "text-black",
            "75" => "text-opacity-75",
            "50" => "text-opacity-50",
            "25" => "text-opacity-25",
            _ when rule.Value.StartsWith("text-") => rule.Value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(TextColorRule rule)
    {
        return GetClass(rule).Length != 0 ? null : rule.Value;
    }

}
