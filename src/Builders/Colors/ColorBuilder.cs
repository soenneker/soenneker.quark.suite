using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance generic color builder.
/// Produces theme utility classes When possible, otherwise falls back to inline style.
/// </summary>
[TailwindPrefix("text-", Responsive = true)]
public sealed class ColorBuilder : ICssBuilder
{
    private readonly List<ColorRule> _rules = new(4);

    internal ColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ColorRule(value, breakpoint));
    }

    internal ColorBuilder(List<ColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

	/// <summary>
	/// Sets the color to primary.
	/// </summary>
    public ColorBuilder Primary => ChainValue("primary");
	/// <summary>
	/// Sets the color to secondary.
	/// </summary>
    public ColorBuilder Secondary => ChainValue("secondary");
	/// <summary>
	/// Sets the color to primary-foreground.
	/// </summary>
    public ColorBuilder PrimaryForeground => ChainValue("primary-foreground");
	/// <summary>
	/// Sets the color to secondary-foreground.
	/// </summary>
    public ColorBuilder SecondaryForeground => ChainValue("secondary-foreground");
	/// <summary>
	/// Sets the color to destructive.
	/// </summary>
    public ColorBuilder Destructive => ChainValue("destructive");
	/// <summary>
	/// Sets the color to destructive-foreground.
	/// </summary>
    public ColorBuilder DestructiveForeground => ChainValue("destructive-foreground");
	/// <summary>
	/// Sets the color to muted-foreground.
	/// </summary>
    public ColorBuilder MutedForeground => ChainValue("muted-foreground");
	/// <summary>
	/// Sets the color to accent.
	/// </summary>
    public ColorBuilder Accent => ChainValue("accent");
	/// <summary>
	/// Sets the color to accent-foreground.
	/// </summary>
    public ColorBuilder AccentForeground => ChainValue("accent-foreground");
	/// <summary>
	/// Sets the color to popover.
	/// </summary>
    public ColorBuilder Popover => ChainValue("popover");
	/// <summary>
	/// Sets the color to popover-foreground.
	/// </summary>
    public ColorBuilder PopoverForeground => ChainValue("popover-foreground");
	/// <summary>
	/// Sets the color to card.
	/// </summary>
    public ColorBuilder Card => ChainValue("card");
	/// <summary>
	/// Sets the color to card-foreground.
	/// </summary>
    public ColorBuilder CardForeground => ChainValue("card-foreground");
	/// <summary>
	/// Sets the color to background.
	/// </summary>
    public ColorBuilder Background => ChainValue("background");
	/// <summary>
	/// Sets the color to foreground.
	/// </summary>
    public ColorBuilder Foreground => ChainValue("foreground");
	/// <summary>
	/// Sets the color to border.
	/// </summary>
    public ColorBuilder Border => ChainValue("border");
	/// <summary>
	/// Sets the color to input.
	/// </summary>
    public ColorBuilder Input => ChainValue("input");
	/// <summary>
	/// Sets the color to ring.
	/// </summary>
    public ColorBuilder Ring => ChainValue("ring");
	/// <summary>
	/// Sets the color to success.
	/// </summary>
    public ColorBuilder Success => ChainValue("success");
	/// <summary>
	/// Sets the color to danger.
	/// </summary>
    public ColorBuilder Danger => ChainValue("danger");
	/// <summary>
	/// Sets the color to warning.
	/// </summary>
    public ColorBuilder Warning => ChainValue("warning");
	/// <summary>
	/// Sets the color to info.
	/// </summary>
    public ColorBuilder Info => ChainValue("info");
	/// <summary>
	/// Sets the color to light.
	/// </summary>
    public ColorBuilder Light => ChainValue("light");
	/// <summary>
	/// Sets the color to dark.
	/// </summary>
    public ColorBuilder Dark => ChainValue("dark");
	/// <summary>
	/// Sets the color to link.
	/// </summary>
    public ColorBuilder Link => ChainValue("link");
	/// <summary>
	/// Sets the color to muted.
	/// </summary>
    public ColorBuilder Muted => ChainValue("muted");

	/// <summary>
	/// Sets the color to white.
	/// </summary>
    public ColorBuilder White => ChainValue("white");

	/// <summary>
	/// Sets the color to black.
	/// </summary>
    public ColorBuilder Black => ChainValue("black");

	/// <summary>
	/// Sets the color to inherit.
	/// </summary>
    public ColorBuilder Inherit => ChainValue(GlobalKeyword.InheritValue);
	/// <summary>
	/// Sets the color to initial.
	/// </summary>
    public ColorBuilder Initial => ChainValue(GlobalKeyword.InitialValue);
	/// <summary>
	/// Sets the color to revert.
	/// </summary>
    public ColorBuilder Revert => ChainValue(GlobalKeyword.RevertValue);
	/// <summary>
	/// Sets the color to revert-layer.
	/// </summary>
    public ColorBuilder RevertLayer => ChainValue(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Sets the color to unset.
	/// </summary>
    public ColorBuilder Unset => ChainValue(GlobalKeyword.UnsetValue);

	/// <summary>
	/// Applies the color on phone breakpoint.
	/// </summary>
    public ColorBuilder OnBase => ChainBp(BreakpointType.Base);
	/// <summary>
	/// Applies the color on small breakpoint (≥640px).
	/// </summary>
    public ColorBuilder OnSm => ChainBp(BreakpointType.Sm);
	/// <summary>
	/// Applies the color on tablet breakpoint.
	/// </summary>
    public ColorBuilder OnMd => ChainBp(BreakpointType.Md);
	/// <summary>
	/// Applies the color on laptop breakpoint.
	/// </summary>
    public ColorBuilder OnLg => ChainBp(BreakpointType.Lg);
	/// <summary>
	/// Applies the color on desktop breakpoint.
	/// </summary>
    public ColorBuilder OnXl => ChainBp(BreakpointType.Xl);
	/// <summary>
	/// Applies the color on widescreen breakpoint.
	/// </summary>
    public ColorBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColorBuilder ChainValue(string value)
    {
        _rules.Add(new ColorRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColorBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ColorRule("inherit", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ColorRule(last.Value, bp);
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
    /// Returns the string representation of the color builder.
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
    private static string GetClass(ColorRule rule)
    {
        return rule.Value switch
        {
            "primary" => "text-primary",
            "primary-foreground" => "text-primary-foreground",
            "secondary" => "text-secondary",
            "secondary-foreground" => "text-secondary-foreground",
            "destructive" => "text-destructive",
            "destructive-foreground" => "text-destructive-foreground",
            "muted" => "text-muted",
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
            "link" => "text-primary",
            "white" => "text-white",
            "black" => "text-black",
            _ when rule.Value.StartsWith("text-") => rule.Value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(ColorRule rule)
    {
        return GetClass(rule).Length != 0 ? null : rule.Value;
    }

}