using Soenneker.Quark.Attributes;
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
[TailwindPrefix("text-", Responsive = true)]
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
	/// Sets the text color to popover-foreground.
	/// </summary>
    public TextColorBuilder PopoverForeground => ChainValue("popover-foreground");
	/// <summary>
	/// Sets the text color to card-foreground.
	/// </summary>
    public TextColorBuilder CardForeground => ChainValue("card-foreground");
	/// <summary>
	/// Sets the text color to foreground.
	/// </summary>
    public TextColorBuilder Foreground => ChainValue("foreground");
	/// <summary>
	/// Sets the text color to success.
	/// </summary>
    public TextColorBuilder Success => ChainValue("success");
	/// <summary>
	/// Sets the text color to warning.
	/// </summary>
    public TextColorBuilder Warning => ChainValue("warning");
	/// <summary>
	/// Sets the text color to info.
	/// </summary>
    public TextColorBuilder Info => ChainValue("info");
	/// <summary>
	/// Sets the text color to white.
	/// </summary>
    public TextColorBuilder White => ChainValue("white");
	/// <summary>
	/// Sets the text color to black.
	/// </summary>
    public TextColorBuilder Black => ChainValue("black");
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
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

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
            "popover-foreground" => "text-popover-foreground",
            "card-foreground" => "text-card-foreground",
            "foreground" => "text-foreground",
            "success" => "text-success",
            "warning" => "text-warning",
            "info" => "text-info",
            "white" => "text-white",
            "black" => "text-black",
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
