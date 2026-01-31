using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance text wrap builder with fluent API for chaining text wrap rules.
/// </summary>
public sealed class TextWrapBuilder : ICssBuilder
{
    private readonly List<TextWrapRule> _rules = new(4);

    private const string _classWrap = "text-wrap";
    private const string _classNoWrap = "text-nowrap";
    private const string _stylePrefix = "text-wrap: ";

    internal TextWrapBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new TextWrapRule(value, breakpoint));
    }

    internal TextWrapBuilder(List<TextWrapRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the text wrap to wrap.
    /// </summary>
    public TextWrapBuilder Wrap => Chain(TextWrapKeyword.WrapValue);
    /// <summary>
    /// Sets the text wrap to no-wrap.
    /// </summary>
    public TextWrapBuilder NoWrap => Chain(TextWrapKeyword.NoWrapValue);
    /// <summary>
    /// Sets the text wrap to inherit.
    /// </summary>
    public TextWrapBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    /// <summary>
    /// Sets the text wrap to initial.
    /// </summary>
    public TextWrapBuilder Initial => Chain(GlobalKeyword.InitialValue);
    /// <summary>
    /// Sets the text wrap to revert.
    /// </summary>
    public TextWrapBuilder Revert => Chain(GlobalKeyword.RevertValue);
    /// <summary>
    /// Sets the text wrap to revert-layer.
    /// </summary>
    public TextWrapBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Sets the text wrap to unset.
    /// </summary>
    public TextWrapBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Applies the text wrap on phone breakpoint.
    /// </summary>
    public TextWrapBuilder OnPhone => ChainBp(BreakpointType.Phone);
    /// <summary>
    /// Applies the text wrap on tablet breakpoint.
    /// </summary>
    public TextWrapBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    /// <summary>
    /// Applies the text wrap on laptop breakpoint.
    /// </summary>
    public TextWrapBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    /// <summary>
    /// Applies the text wrap on desktop breakpoint.
    /// </summary>
    public TextWrapBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    /// <summary>
    /// Applies the text wrap on widescreen breakpoint.
    /// </summary>
    public TextWrapBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the text wrap on ultrawide breakpoint.
    /// </summary>
    public TextWrapBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextWrapBuilder Chain(string value)
    {
        _rules.Add(new TextWrapRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextWrapBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TextWrapRule(TextWrapKeyword.WrapValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TextWrapRule(last.Value, bp);
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
                TextWrapKeyword.WrapValue => _classWrap,
                TextWrapKeyword.NoWrapValue => _classNoWrap,
                _ => string.Empty
            };

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
            var css = rule.Value switch
            {
                TextWrapKeyword.WrapValue => "wrap",
                TextWrapKeyword.NoWrapValue => "nowrap",
                GlobalKeyword.InheritValue => "inherit",
                GlobalKeyword.InitialValue => "initial",
                GlobalKeyword.UnsetValue => "unset",
                GlobalKeyword.RevertValue => "revert",
                GlobalKeyword.RevertLayerValue => "revert-layer",
                _ => null
            };

            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_stylePrefix);
            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}

 
