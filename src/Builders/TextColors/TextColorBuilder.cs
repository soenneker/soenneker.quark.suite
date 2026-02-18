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

	/// <summary>
	/// Applies the text color on phone breakpoint.
	/// </summary>
    public TextColorBuilder OnPhone => ChainBp(BreakpointType.Base);
	/// <summary>
	/// Applies the text color on tablet breakpoint.
	/// </summary>
    public TextColorBuilder OnTablet => ChainBp(BreakpointType.Md);
	/// <summary>
	/// Applies the text color on laptop breakpoint.
	/// </summary>
    public TextColorBuilder OnLaptop => ChainBp(BreakpointType.Lg);
	/// <summary>
	/// Applies the text color on desktop breakpoint.
	/// </summary>
    public TextColorBuilder OnDesktop => ChainBp(BreakpointType.Xl);
	/// <summary>
	/// Applies the text color on widescreen breakpoint.
	/// </summary>
    public TextColorBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
	/// <summary>
	/// Applies the text color on ultrawide breakpoint.
	/// </summary>
    public TextColorBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

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

        int lastIdx = _rules.Count - 1;
        TextColorRule last = _rules[lastIdx];
        _rules[lastIdx] = new TextColorRule(last.Value, bp);
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
            TextColorRule rule = _rules[i];
            string cls = GetClass(rule);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
            TextColorRule rule = _rules[i];
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
    /// Returns the string representation of the text color builder.
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
