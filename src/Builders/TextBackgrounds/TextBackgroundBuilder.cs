using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text background combination builder.
/// Produces theme utility classes that set both text and background colors (e.g. q-text-bg-*).
/// </summary>
public sealed class TextBackgroundBuilder : ICssBuilder
{
    private readonly List<TextBackgroundRule> _rules = new(4);

    internal TextBackgroundBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextBackgroundRule(value, breakpoint));
    }

    internal TextBackgroundBuilder(List<TextBackgroundRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }
    
    /// <summary>
    /// Sets the text background to primary theme color.
    /// </summary>
    public TextBackgroundBuilder Primary => ChainValue("primary");
    /// <summary>
    /// Sets the text background to secondary theme color.
    /// </summary>
    public TextBackgroundBuilder Secondary => ChainValue("secondary");
    /// <summary>
    /// Sets the text background to success theme color.
    /// </summary>
    public TextBackgroundBuilder Success => ChainValue("success");
    /// <summary>
    /// Sets the text background to danger theme color.
    /// </summary>
    public TextBackgroundBuilder Danger => ChainValue("danger");
    /// <summary>
    /// Sets the text background to warning theme color.
    /// </summary>
    public TextBackgroundBuilder Warning => ChainValue("warning");
    /// <summary>
    /// Sets the text background to info theme color.
    /// </summary>
    public TextBackgroundBuilder Info => ChainValue("info");
    /// <summary>
    /// Sets the text background to light theme color.
    /// </summary>
    public TextBackgroundBuilder Light => ChainValue("light");
    /// <summary>
    /// Sets the text background to dark theme color.
    /// </summary>
    public TextBackgroundBuilder Dark => ChainValue("dark");
    /// <summary>
    /// Sets the text background to body color.
    /// </summary>
    public TextBackgroundBuilder Body => ChainValue("body");
    /// <summary>
    /// Sets the text background to body-secondary color.
    /// </summary>
    public TextBackgroundBuilder BodySecondary => ChainValue("body-secondary");
    /// <summary>
    /// Sets the text background to body-tertiary color.
    /// </summary>
    public TextBackgroundBuilder BodyTertiary => ChainValue("body-tertiary");

    /// <summary>
    /// Applies the text background on phone breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text background on tablet breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text background on laptop breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text background on desktop breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text background on widescreen breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the text background on ultrawide breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextBackgroundBuilder ChainValue(string value)
    {
        _rules.Add(new TextBackgroundRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextBackgroundBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextBackgroundRule("primary", bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        TextBackgroundRule last = _rules[lastIdx];
        _rules[lastIdx] = new TextBackgroundRule(last.Value, bp);
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
            TextBackgroundRule rule = _rules[i];
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
    /// Text-bg is applied via theme classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as text-bg is handled via classes.</returns>
    public string ToStyle()
    {
        return string.Empty;
    }

    /// <summary>
    /// Returns the string representation of the text background builder.
    /// </summary>
    public override string ToString()
    {
        return ToClass();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(TextBackgroundRule rule)
    {
        return rule.Value switch
        {
            "primary" => "q-text-bg-primary",
            "secondary" => "q-text-bg-secondary",
            "success" => "q-text-bg-success",
            "danger" => "q-text-bg-danger",
            "warning" => "q-text-bg-warning",
            "info" => "q-text-bg-info",
            "light" => "q-text-bg-light",
            "dark" => "q-text-bg-dark",
            "body" => "q-text-bg-body",
            "body-secondary" => "q-text-bg-body-secondary",
            "body-tertiary" => "q-text-bg-body-tertiary",
            _ => string.Empty
        };
    }

}

