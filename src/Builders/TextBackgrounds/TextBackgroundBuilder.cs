using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance text background combination builder.
/// Produces Bootstrap text-bg utility classes that set both text and background colors.
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
    public TextBackgroundBuilder OnPhone => ChainBp(BreakpointType.Phone);
    /// <summary>
    /// Applies the text background on tablet breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    /// <summary>
    /// Applies the text background on laptop breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    /// <summary>
    /// Applies the text background on desktop breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    /// <summary>
    /// Applies the text background on widescreen breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the text background on ultrawide breakpoint.
    /// </summary>
    public TextBackgroundBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
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
    /// Text-bg classes should always use Bootstrap classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as text-bg classes are handled via classes.</returns>
    public string ToStyle()
    {
        // Text-bg classes should always use Bootstrap classes, not inline styles
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
            "primary" => "text-bg-primary",
            "secondary" => "text-bg-secondary",
            "success" => "text-bg-success",
            "danger" => "text-bg-danger",
            "warning" => "text-bg-warning",
            "info" => "text-bg-info",
            "light" => "text-bg-light",
            "dark" => "text-bg-dark",
            "body" => "text-bg-body",
            "body-secondary" => "text-bg-body-secondary",
            "body-tertiary" => "text-bg-body-tertiary",
            _ => string.Empty
        };
    }

}

