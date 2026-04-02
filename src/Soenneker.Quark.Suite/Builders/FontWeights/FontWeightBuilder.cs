using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// High-performance font weight builder with fluent API for chaining font weight rules.
/// </summary>
[TailwindPrefix("font-", Responsive = true)]
public sealed class FontWeightBuilder : ICssBuilder
{
    private readonly List<FontWeightRule> _rules = new(6);
    private BreakpointType? _pendingBreakpoint;

    // Tailwind font-weight utilities (for Quark Suite / shadcn)
    private const string _classExtralight = "font-extralight";
    private const string _classLight = "font-light";
    private const string _classNormal = "font-normal";
    private const string _classMedium = "font-medium";
    private const string _classSemibold = "font-semibold";
    private const string _classBold = "font-bold";
    private const string _classExtrabold = "font-extrabold";
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
    /// Sets the font weight to extralight.
    /// </summary>
    public FontWeightBuilder Extralight => Chain("extralight");
    /// <summary>
    /// Sets the font weight to light.
    /// </summary>
    public FontWeightBuilder Light => Chain(FontWeightKeyword.LightValue);
    /// <summary>
    /// Sets the font weight to normal.
    /// </summary>
    public FontWeightBuilder Normal => Chain(FontWeightKeyword.NormalValue);
    /// <summary>
    /// Sets the font weight to medium.
    /// </summary>
    public FontWeightBuilder Medium => Chain("medium");
    /// <summary>
    /// Sets the font weight to semibold.
    /// </summary>
    public FontWeightBuilder Semibold => Chain(FontWeightKeyword.SemiboldValue);
    /// <summary>
    /// Sets the font weight to bold.
    /// </summary>
    public FontWeightBuilder Bold => Chain(FontWeightKeyword.BoldValue);
    /// <summary>
    /// Sets the font weight to extrabold.
    /// </summary>
    public FontWeightBuilder Extrabold => Chain("extrabold");
    /// <summary>
    /// Applies the font weight on phone breakpoint.
    /// </summary>
    public FontWeightBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Applies the font weight on small breakpoint (≥640px).
    /// </summary>
    public FontWeightBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);

    /// <summary>
    /// Applies the font weight on tablet breakpoint.
    /// </summary>
    public FontWeightBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);

    /// <summary>
    /// Applies the font weight on laptop breakpoint.
    /// </summary>
    public FontWeightBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);

    /// <summary>
    /// Applies the font weight on desktop breakpoint.
    /// </summary>
    public FontWeightBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);

    /// <summary>
    /// Applies the font weight on the 2xl breakpoint.
    /// </summary>
    public FontWeightBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontWeightBuilder Chain(string value)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new FontWeightRule(value, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FontWeightBuilder SetPendingBreakpoint(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
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
                "extralight" => _classExtralight,
                FontWeightKeyword.LightValue => _classLight,
                FontWeightKeyword.NormalValue => _classNormal,
                "medium" => _classMedium,
                FontWeightKeyword.SemiboldValue => _classSemibold,
                FontWeightKeyword.BoldValue => _classBold,
                "extrabold" => _classExtrabold,
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
        return string.Empty;
    }
    public override string ToString() => ToClass();
}