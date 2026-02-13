using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// High-performance font style builder with fluent API for chaining font style rules.
/// </summary>
public sealed class FontStyleBuilder : ICssBuilder
{
    private readonly List<FontStyleRule> _rules = new(4);

    private const string _classItalic = "fst-italic";
    private const string _classNormal = "fst-normal";
    private const string _stylePrefix = "font-style: ";

    internal FontStyleBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FontStyleRule(value, breakpoint));
    }

    internal FontStyleBuilder(List<FontStyleRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the font style to italic.
    /// </summary>
    public FontStyleBuilder Italic => Chain(FontStyleKeyword.ItalicValue);
    /// <summary>
    /// Sets the font style to normal.
    /// </summary>
    public FontStyleBuilder Normal => Chain(FontStyleKeyword.NormalValue);
    /// <summary>
    /// Sets the font style to inherit.
    /// </summary>
    public FontStyleBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    /// <summary>
    /// Sets the font style to initial.
    /// </summary>
    public FontStyleBuilder Initial => Chain(GlobalKeyword.InitialValue);
    /// <summary>
    /// Sets the font style to revert.
    /// </summary>
    public FontStyleBuilder Revert => Chain(GlobalKeyword.RevertValue);
    /// <summary>
    /// Sets the font style to revert-layer.
    /// </summary>
    public FontStyleBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Sets the font style to unset.
    /// </summary>
    public FontStyleBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Applies the font style on phone breakpoint.
    /// </summary>
    public FontStyleBuilder OnPhone => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the font style on tablet breakpoint.
    /// </summary>
    public FontStyleBuilder OnTablet => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies the font style on laptop breakpoint.
    /// </summary>
    public FontStyleBuilder OnLaptop => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies the font style on desktop breakpoint.
    /// </summary>
    public FontStyleBuilder OnDesktop => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies the font style on widescreen breakpoint.
    /// </summary>
    public FontStyleBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);
    /// <summary>
    /// Applies the font style on ultrawide breakpoint.
    /// </summary>
    public FontStyleBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontStyleBuilder Chain(string value)
    {
        _rules.Add(new FontStyleRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontStyleBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FontStyleRule(FontStyleKeyword.NormalValue, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        FontStyleRule last = _rules[lastIdx];
        _rules[lastIdx] = new FontStyleRule(last.Value, bp);
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
            FontStyleRule rule = _rules[i];
            string cls = rule.Value switch
            {
                FontStyleKeyword.ItalicValue => _classItalic,
                FontStyleKeyword.NormalValue => _classNormal,
                _ => string.Empty
            };
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
            FontStyleRule rule = _rules[i];
            string val = rule.Value;
            if (string.IsNullOrEmpty(val))
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(_stylePrefix);
            sb.Append(val);
        }
        return sb.ToString();
    }


}

 
