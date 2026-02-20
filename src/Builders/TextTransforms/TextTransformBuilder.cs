using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text transform builder with fluent API for chaining text transform rules.
/// </summary>
public sealed class TextTransformBuilder : ICssBuilder
{
    private readonly List<TextTransformRule> _rules = new(4);

    // Tailwind text-transform utilities (for Quark Suite / shadcn)
    private const string _classLower = "lowercase";
    private const string _classUpper = "uppercase";
    private const string _classCap = "capitalize";
    private const string _stylePrefix = "text-transform: ";

    internal TextTransformBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextTransformRule(value, breakpoint));
    }

    internal TextTransformBuilder(List<TextTransformRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the text transform to lowercase.
    /// </summary>
    public TextTransformBuilder Lowercase => Chain(TextTransformKeyword.LowercaseValue);
    /// <summary>
    /// Sets the text transform to uppercase.
    /// </summary>
    public TextTransformBuilder Uppercase => Chain(TextTransformKeyword.UppercaseValue);
    /// <summary>
    /// Sets the text transform to capitalize.
    /// </summary>
    public TextTransformBuilder Capitalize => Chain(TextTransformKeyword.CapitalizeValue);
    /// <summary>
    /// Sets the text transform to inherit.
    /// </summary>
    public TextTransformBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    /// <summary>
    /// Sets the text transform to initial.
    /// </summary>
    public TextTransformBuilder Initial => Chain(GlobalKeyword.InitialValue);
    /// <summary>
    /// Sets the text transform to revert.
    /// </summary>
    public TextTransformBuilder Revert => Chain(GlobalKeyword.RevertValue);
    /// <summary>
    /// Sets the text transform to revert-layer.
    /// </summary>
    public TextTransformBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Sets the text transform to unset.
    /// </summary>
    public TextTransformBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Applies the text transform on phone breakpoint.
    /// </summary>
    public TextTransformBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the text transform on tablet breakpoint.
    /// </summary>
    public TextTransformBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the text transform on laptop breakpoint.
    /// </summary>
    public TextTransformBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the text transform on desktop breakpoint.
    /// </summary>
    public TextTransformBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the text transform on widescreen breakpoint.
    /// </summary>
    public TextTransformBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the text transform on ultrawide breakpoint.
    /// </summary>
    public TextTransformBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextTransformBuilder Chain(string value)
    {
        _rules.Add(new TextTransformRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextTransformBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextTransformRule(TextTransformKeyword.LowercaseValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TextTransformRule(last.Value, bp);
        return this;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Value switch
            {
                TextTransformKeyword.LowercaseValue => _classLower,
                TextTransformKeyword.UppercaseValue => _classUpper,
                TextTransformKeyword.CapitalizeValue => _classCap,
                _ => string.Empty
            };
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
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var val = rule.Value;
            if (string.IsNullOrEmpty(val))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_stylePrefix);
            sb.Append(val);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}

 
