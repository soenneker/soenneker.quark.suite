using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance font weight builder with fluent API for chaining font weight rules.
/// </summary>
public sealed class FontWeightBuilder : ICssBuilder
{
    private readonly List<FontWeightRule> _rules = new(6);

    private const string _classLighter = "fw-lighter";
    private const string _classLight = "fw-light";
    private const string _classNormal = "fw-normal";
    private const string _classMedium = "fw-medium";
    private const string _classSemibold = "fw-semibold";
    private const string _classBold = "fw-bold";
    private const string _classBolder = "fw-bolder";
    private const string _stylePrefix = "font-weight: ";

    internal FontWeightBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FontWeightRule(value, breakpoint));
    }

    internal FontWeightBuilder(List<FontWeightRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the font weight to lighter.
    /// </summary>
    public FontWeightBuilder Lighter => Chain(FontWeightKeyword.LighterValue);
    /// <summary>
    /// Sets the font weight to light.
    /// </summary>
    public FontWeightBuilder Light => Chain(FontWeightKeyword.LightValue);
    /// <summary>
    /// Sets the font weight to normal.
    /// </summary>
    public FontWeightBuilder Normal => Chain(FontWeightKeyword.NormalValue);
    // Note: Medium is not available in the current FontWeightKeyword package version
    // public FontWeightBuilder Medium => Chain(FontWeightKeyword.MediumValue);
    /// <summary>
    /// Sets the font weight to semibold.
    /// </summary>
    public FontWeightBuilder Semibold => Chain(FontWeightKeyword.SemiboldValue);
    /// <summary>
    /// Sets the font weight to bold.
    /// </summary>
    public FontWeightBuilder Bold => Chain(FontWeightKeyword.BoldValue);
    /// <summary>
    /// Sets the font weight to bolder.
    /// </summary>
    public FontWeightBuilder Bolder => Chain(FontWeightKeyword.BolderValue);
    /// <summary>
    /// Sets the font weight to inherit.
    /// </summary>
    public FontWeightBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    /// <summary>
    /// Sets the font weight to initial.
    /// </summary>
    public FontWeightBuilder Initial => Chain(GlobalKeyword.InitialValue);
    /// <summary>
    /// Sets the font weight to revert.
    /// </summary>
    public FontWeightBuilder Revert => Chain(GlobalKeyword.RevertValue);
    /// <summary>
    /// Sets the font weight to revert-layer.
    /// </summary>
    public FontWeightBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Sets the font weight to unset.
    /// </summary>
    public FontWeightBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    /// <summary>
    /// Applies the font weight on phone breakpoint.
    /// </summary>
    public FontWeightBuilder OnPhone => ChainBp(BreakpointType.Base);

    /// <summary>
    /// Applies the font weight on tablet breakpoint.
    /// </summary>
    public FontWeightBuilder OnTablet => ChainBp(BreakpointType.Md);

    /// <summary>
    /// Applies the font weight on laptop breakpoint.
    /// </summary>
    public FontWeightBuilder OnLaptop => ChainBp(BreakpointType.Lg);

    /// <summary>
    /// Applies the font weight on desktop breakpoint.
    /// </summary>
    public FontWeightBuilder OnDesktop => ChainBp(BreakpointType.Xl);

    /// <summary>
    /// Applies the font weight on widescreen breakpoint.
    /// </summary>
    public FontWeightBuilder OnWideScreen => ChainBp(BreakpointType.Xxl);

    /// <summary>
    /// Applies the font weight on ultrawide breakpoint.
    /// </summary>
    public FontWeightBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontWeightBuilder Chain(string value)
    {
        _rules.Add(new FontWeightRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontWeightBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FontWeightRule(FontWeightKeyword.NormalValue, bp));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        FontWeightRule last = _rules[lastIdx];
        _rules[lastIdx] = new FontWeightRule(last.Value, bp);
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
            FontWeightRule rule = _rules[i];
            string cls = rule.Value switch
            {
                FontWeightKeyword.LighterValue => _classLighter,
                FontWeightKeyword.LightValue => _classLight,
                FontWeightKeyword.NormalValue => _classNormal,
                FontWeightKeyword.SemiboldValue => _classSemibold,
                FontWeightKeyword.BoldValue => _classBold,
                FontWeightKeyword.BolderValue => _classBolder,
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
            FontWeightRule rule = _rules[i];
            string? css = rule.Value switch
            {
                FontWeightKeyword.LighterValue => "lighter",
                FontWeightKeyword.LightValue => "300",
                FontWeightKeyword.NormalValue => "400",
                // FontWeightKeyword.MediumValue => "500",
                FontWeightKeyword.SemiboldValue => "600",
                FontWeightKeyword.BoldValue => "700",
                FontWeightKeyword.BolderValue => "bolder",
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


}
