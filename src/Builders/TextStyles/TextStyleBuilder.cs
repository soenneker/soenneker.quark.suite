using Soenneker.Quark.Enums;
using System.Collections.Generic;

namespace Soenneker.Quark;

/// <summary>
/// Builder for Bootstrap text utilities.
/// </summary>
public sealed class TextStyleBuilder : ICssBuilder
{
    private readonly List<TextStyleRule> _rules = new(4);

    internal TextStyleBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextStyleRule(value, breakpoint));
    }

    internal TextStyleBuilder(List<TextStyleRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Gets a value indicating whether the builder is empty (has no rules).
    /// </summary>
    public bool IsEmpty => _rules.Count == 0;
    /// <summary>
    /// Gets a value indicating whether this builder generates CSS classes.
    /// </summary>
    public bool IsCssClass => true;
    /// <summary>
    /// Gets a value indicating whether this builder generates CSS styles.
    /// </summary>
    public bool IsCssStyle => false;

    // Wrapping
    /// <summary>
    /// Sets the text style to wrap.
    /// </summary>
    public TextStyleBuilder Wrap => ChainValue("wrap");
    /// <summary>
    /// Sets the text style to nowrap.
    /// </summary>
    public TextStyleBuilder Nowrap => ChainValue("nowrap");
    /// <summary>
    /// Sets the text style to truncate.
    /// </summary>
    public TextStyleBuilder Truncate => ChainValue("truncate");

    // Transformation
    /// <summary>
    /// Sets the text style to lowercase.
    /// </summary>
    public TextStyleBuilder Lowercase => ChainValue("lowercase");
    /// <summary>
    /// Sets the text style to uppercase.
    /// </summary>
    public TextStyleBuilder Uppercase => ChainValue("uppercase");
    /// <summary>
    /// Sets the text style to capitalize.
    /// </summary>
    public TextStyleBuilder Capitalize => ChainValue("capitalize");

    // Styling
    /// <summary>
    /// Sets the text style to reset.
    /// </summary>
    public TextStyleBuilder Reset => ChainValue("reset");
    /// <summary>
    /// Sets the text style to muted.
    /// </summary>
    public TextStyleBuilder Muted => ChainValue("muted");
    /// <summary>
    /// Sets the text style to emphasis.
    /// </summary>
    public TextStyleBuilder Emphasis => ChainValue("emphasis");

    // Breakpoints
    /// <summary>
    /// Applies the text style on phone breakpoint.
    /// </summary>
    public TextStyleBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text style on tablet breakpoint.
    /// </summary>
    public TextStyleBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text style on laptop breakpoint.
    /// </summary>
    public TextStyleBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text style on desktop breakpoint.
    /// </summary>
    public TextStyleBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text style on widescreen breakpoint.
    /// </summary>
    public TextStyleBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private TextStyleBuilder ChainValue(string value)
    {
        var newRules = new List<TextStyleRule>(_rules) { new(value, null) };
        return new TextStyleBuilder(newRules);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private TextStyleBuilder ChainBp(BreakpointType bp)
    {
        var newRules = new List<TextStyleRule>(_rules);
        if (newRules.Count == 0)
        {
            newRules.Add(new TextStyleRule("inherit", bp));
            return new TextStyleBuilder(newRules);
        }

        int lastIdx = newRules.Count - 1;
        TextStyleRule last = newRules[lastIdx];
        newRules[lastIdx] = new TextStyleRule(last.Value, bp);
        return new TextStyleBuilder(newRules);
    }

    /// <summary>
    /// Returns the CSS class string representation of this text style builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new Utils.PooledStringBuilders.PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            TextStyleRule rule = _rules[i];
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

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static string GetClass(TextStyleRule styleRule)
    {
        return styleRule.Value switch
        {
            "wrap" => "text-wrap",
            "nowrap" => "text-nowrap",
            "truncate" => "text-truncate",
            "lowercase" => "text-lowercase",
            "uppercase" => "text-uppercase",
            "capitalize" => "text-capitalize",
            "reset" => "text-reset",
            "muted" => "text-muted",
            "emphasis" => "text-emphasis",
            _ => string.Empty
        };
    }


    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public string ToClass() => ToString();
    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// This builder only generates CSS classes, so this returns an empty string.
    /// </summary>
    /// <returns>An empty string.</returns>
    public string ToStyle() => string.Empty;
}