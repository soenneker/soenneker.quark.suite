using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance background color builder.
/// Produces Tailwind utility tokens When possible, otherwise falls back to inline style.
/// </summary>
[TailwindPrefix("bg-", Responsive = true)]
public sealed class BackgroundColorBuilder : ICssBuilder
{
    private readonly List<BackgroundColorRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal BackgroundColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackgroundColorRule(value, breakpoint));
    }

    internal BackgroundColorBuilder(List<BackgroundColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

	/// <summary>
	/// Sets the background color to primary.
	/// </summary>
    public BackgroundColorBuilder Primary => ChainValue("primary");
	/// <summary>
	/// Sets the background color to secondary.
	/// </summary>
    public BackgroundColorBuilder Secondary => ChainValue("secondary");
	/// <summary>
	/// Sets the background color to primary-foreground.
	/// </summary>
    public BackgroundColorBuilder PrimaryForeground => ChainValue("primary-foreground");
	/// <summary>
	/// Sets the background color to secondary-foreground.
	/// </summary>
    public BackgroundColorBuilder SecondaryForeground => ChainValue("secondary-foreground");
	/// <summary>
	/// Sets the background color to destructive.
	/// </summary>
    public BackgroundColorBuilder Destructive => ChainValue("destructive");
	/// <summary>
	/// Sets the background color to destructive-foreground.
	/// </summary>
    public BackgroundColorBuilder DestructiveForeground => ChainValue("destructive-foreground");
	/// <summary>
	/// Sets the background color to muted-foreground.
	/// </summary>
    public BackgroundColorBuilder MutedForeground => ChainValue("muted-foreground");
	/// <summary>
	/// Sets the background color to accent.
	/// </summary>
    public BackgroundColorBuilder Accent => ChainValue("accent");
	/// <summary>
	/// Sets the background color to accent-foreground.
	/// </summary>
    public BackgroundColorBuilder AccentForeground => ChainValue("accent-foreground");
	/// <summary>
	/// Sets the background color to popover.
	/// </summary>
    public BackgroundColorBuilder Popover => ChainValue("popover");
	/// <summary>
	/// Sets the background color to popover-foreground.
	/// </summary>
    public BackgroundColorBuilder PopoverForeground => ChainValue("popover-foreground");
	/// <summary>
	/// Sets the background color to card.
	/// </summary>
    public BackgroundColorBuilder Card => ChainValue("card");
	/// <summary>
	/// Sets the background color to card-foreground.
	/// </summary>
    public BackgroundColorBuilder CardForeground => ChainValue("card-foreground");
	/// <summary>
	/// Sets the background color to background.
	/// </summary>
    public BackgroundColorBuilder Background => ChainValue("background");
	/// <summary>
	/// Sets the background color to foreground.
	/// </summary>
    public BackgroundColorBuilder Foreground => ChainValue("foreground");
	/// <summary>
	/// Sets the background color to border.
	/// </summary>
    public BackgroundColorBuilder Border => ChainValue("border");
	/// <summary>
	/// Sets the background color to input.
	/// </summary>
    public BackgroundColorBuilder Input => ChainValue("input");
	/// <summary>
	/// Sets the background color to ring.
	/// </summary>
    public BackgroundColorBuilder Ring => ChainValue("ring");
	/// <summary>
	/// Sets the background color to success.
	/// </summary>
    public BackgroundColorBuilder Success => ChainValue("success");
	/// <summary>
	/// Sets the background color to warning.
	/// </summary>
    public BackgroundColorBuilder Warning => ChainValue("warning");
	/// <summary>
	/// Sets the background color to info.
	/// </summary>
    public BackgroundColorBuilder Info => ChainValue("info");
	/// <summary>
	/// Sets the background color to muted.
	/// </summary>
    public BackgroundColorBuilder Muted => ChainValue("muted");

	/// <summary>
	/// Sets the background color to white.
	/// </summary>
    public BackgroundColorBuilder White => ChainValue("white");

	/// <summary>
	/// Sets the background color to black.
	/// </summary>
    public BackgroundColorBuilder Black => ChainValue("black");

	/// <summary>
	/// Sets the background color to transparent.
	/// </summary>
    public BackgroundColorBuilder Transparent => ChainValue("transparent");

	/// <summary>
	/// Sets the background color to inherit.
	/// </summary>
    public BackgroundColorBuilder Inherit => ChainValue(GlobalKeyword.Inherit);
	/// <summary>
	/// Sets the background color to initial.
	/// </summary>
    public BackgroundColorBuilder Initial => ChainValue(GlobalKeyword.Initial);
	/// <summary>
	/// Sets the background color to revert.
	/// </summary>
    public BackgroundColorBuilder Revert => ChainValue(GlobalKeyword.Revert);
	/// <summary>
	/// Sets the background color to revert-layer.
	/// </summary>
    public BackgroundColorBuilder RevertLayer => ChainValue(GlobalKeyword.RevertLayer);
	/// <summary>
	/// Sets the background color to unset.
	/// </summary>
    public BackgroundColorBuilder Unset => ChainValue(GlobalKeyword.Unset);

	/// <summary>
	/// Applies the background color on phone breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
	/// <summary>
	/// Applies the background color on small breakpoint (≥640px).
	/// </summary>
    public BackgroundColorBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
	/// <summary>
	/// Applies the background color on tablet breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
	/// <summary>
	/// Applies the background color on laptop breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
	/// <summary>
	/// Applies the background color on desktop breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
	/// <summary>
	/// Applies the background color on the 2xl breakpoint.
	/// </summary>
    public BackgroundColorBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder ChainValue(string value)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new BackgroundColorRule(value, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder SetPendingBreakpoint(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
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
    /// Returns the string representation of the background color builder.
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
    private static string GetClass(BackgroundColorRule rule)
    {
        return rule.Value switch
        {
            "primary" => "bg-primary",
            "primary-foreground" => "bg-primary-foreground",
            "secondary" => "bg-secondary",
            "secondary-foreground" => "bg-secondary-foreground",
            "destructive" => "bg-destructive",
            "destructive-foreground" => "bg-destructive-foreground",
            "muted-foreground" => "bg-muted-foreground",
            "accent" => "bg-accent",
            "accent-foreground" => "bg-accent-foreground",
            "popover" => "bg-popover",
            "popover-foreground" => "bg-popover-foreground",
            "card" => "bg-card",
            "card-foreground" => "bg-card-foreground",
            "background" => "bg-background",
            "foreground" => "bg-foreground",
            "border" => "bg-border",
            "input" => "bg-input",
            "ring" => "bg-ring",
            "success" => "bg-success",
            "warning" => "bg-warning",
            "info" => "bg-info",
            "muted" => "bg-muted",
            "white" => "bg-white",
            "black" => "bg-black",
            "transparent" => "bg-transparent",
            _ when rule.Value.StartsWith("bg-") => rule.Value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(BackgroundColorRule rule)
    {
        return GetClass(rule).Length != 0 ? null : rule.Value;
    }

}