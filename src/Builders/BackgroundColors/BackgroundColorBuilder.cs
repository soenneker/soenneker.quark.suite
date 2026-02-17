using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Extensions.String;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance background color builder.
/// Produces Bootstrap utility classes when possible, otherwise falls back to inline style.
/// </summary>
public sealed class BackgroundColorBuilder : ICssBuilder
{
    private readonly List<BackgroundColorRule> _rules = new(4);

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
	/// Sets the background color to success.
	/// </summary>
    public BackgroundColorBuilder Success => ChainValue("success");
	/// <summary>
	/// Sets the background color to danger.
	/// </summary>
    public BackgroundColorBuilder Danger => ChainValue("danger");
	/// <summary>
	/// Sets the background color to warning.
	/// </summary>
    public BackgroundColorBuilder Warning => ChainValue("warning");
	/// <summary>
	/// Sets the background color to info.
	/// </summary>
    public BackgroundColorBuilder Info => ChainValue("info");
	/// <summary>
	/// Sets the background color to light.
	/// </summary>
    public BackgroundColorBuilder Light => ChainValue("light");
	/// <summary>
	/// Sets the background color to dark.
	/// </summary>
    public BackgroundColorBuilder Dark => ChainValue("dark");
	/// <summary>
	/// Sets the background color to link.
	/// </summary>
    public BackgroundColorBuilder Link => ChainValue("link");
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
    public BackgroundColorBuilder Inherit => ChainValue(GlobalKeyword.InheritValue);
	/// <summary>
	/// Sets the background color to initial.
	/// </summary>
    public BackgroundColorBuilder Initial => ChainValue(GlobalKeyword.InitialValue);
	/// <summary>
	/// Sets the background color to revert.
	/// </summary>
    public BackgroundColorBuilder Revert => ChainValue(GlobalKeyword.RevertValue);
	/// <summary>
	/// Sets the background color to revert-layer.
	/// </summary>
    public BackgroundColorBuilder RevertLayer => ChainValue(GlobalKeyword.RevertLayerValue);
	/// <summary>
	/// Sets the background color to unset.
	/// </summary>
    public BackgroundColorBuilder Unset => ChainValue(GlobalKeyword.UnsetValue);

	/// <summary>
	/// Applies the background color on phone breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnPhone => ChainBp(BreakpointType.Base);
	/// <summary>
	/// Applies the background color on tablet breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnTablet => ChainBp(BreakpointType.Md);
	/// <summary>
	/// Applies the background color on laptop breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnLaptop => ChainBp(BreakpointType.Lg);
	/// <summary>
	/// Applies the background color on desktop breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnDesktop => ChainBp(BreakpointType.Xl);
	/// <summary>
	/// Applies the background color on widescreen breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
	/// <summary>
	/// Applies the background color on ultrawide breakpoint.
	/// </summary>
    public BackgroundColorBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder ChainValue(string value)
    {
        _rules.Add(new BackgroundColorRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BackgroundColorRule("inherit", bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        BackgroundColorRule last = _rules[lastIdx];
        _rules[lastIdx] = new BackgroundColorRule(last.Value, bp);
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
            BackgroundColorRule rule = _rules[i];
            string cls = GetClass(rule);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
            BackgroundColorRule rule = _rules[i];
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
    /// Returns the string representation of the background color builder.
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
    private static string GetClass(BackgroundColorRule rule)
    {
        return rule.Value switch
        {
            "primary" or "secondary" or "success" or "danger" or "warning" or "info" or "light" or "dark" or "link" or "muted" or "white"
                or "black" or "transparent" => rule.Value,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(BackgroundColorRule rule)
    {
        return rule.Value switch
        {
            "primary" or "secondary" or "success" or "danger" or "warning" or "info" or "light" or "dark" or "link" or "muted" or "white" or "black" or "transparent" => null,
            _ => rule.Value
        };
    }

}
