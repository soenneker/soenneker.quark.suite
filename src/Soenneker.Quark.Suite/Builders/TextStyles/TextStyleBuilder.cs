using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Builder for text utility classes.
/// </summary>
[TailwindPrefix("font-", Responsive = true)]
public sealed class TextStyleBuilder : ICssBuilder
{
    private readonly List<TextStyleRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal TextStyleBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextStyleRule(value, breakpoint));
    }

    internal TextStyleBuilder(List<TextStyleRule> rules, BreakpointType? pendingBreakpoint = null)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
        _pendingBreakpoint = pendingBreakpoint;
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
    public TextStyleBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the text style on small breakpoint (≥640px).
    /// </summary>
    public TextStyleBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the text style on tablet breakpoint.
    /// </summary>
    public TextStyleBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the text style on laptop breakpoint.
    /// </summary>
    public TextStyleBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the text style on desktop breakpoint.
    /// </summary>
    public TextStyleBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the text style on the 2xl breakpoint.
    /// </summary>
    public TextStyleBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private TextStyleBuilder ChainValue(string value)
    {
        var bp = _pendingBreakpoint;
        var newRules = new List<TextStyleRule>(_rules) { new TextStyleRule(value, bp) };
        return new TextStyleBuilder(newRules);
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private TextStyleBuilder SetPendingBreakpoint(BreakpointType bp)
    {
        return new TextStyleBuilder(new List<TextStyleRule>(_rules), bp);
    }

    /// <summary>
    /// Returns the CSS class string representation of this text style builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
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

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private static string GetClass(TextStyleRule styleRule)
    {
        return styleRule.Value switch
        {
            "wrap" => "text-wrap",
            "nowrap" => "whitespace-nowrap",
            "truncate" => "truncate",
            "lowercase" => "lowercase",
            "uppercase" => "uppercase",
            "capitalize" => "capitalize",
            "reset" => "text-reset",
            "muted" => "text-muted-foreground",
            "emphasis" => "font-semibold",
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