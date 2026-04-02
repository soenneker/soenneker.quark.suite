using System;
using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified filter builder with fluent API for chaining filter rules.
/// </summary>
[TailwindPrefix("filter", Responsive = true)]
public sealed class FilterBuilder : ICssBuilder
{
    private readonly List<FilterRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    private const string _classFilterNone = "filter-none";
    private const string _classFilterBlur = "blur";
    private const string _classFilterBrightness = "brightness";
    private const string _classFilterContrast = "contrast";
    private const string _classFilterDropShadow = "drop-shadow";
    private const string _classFilterGrayscale = "grayscale";
    private const string _classFilterHueRotate = "hue-rotate";
    private const string _classFilterInvert = "invert";
    private const string _classFilterOpacity = "opacity";
    private const string _classFilterSaturate = "saturate";
    private const string _classFilterSepia = "sepia";

    internal FilterBuilder(string filter, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FilterRule(filter, breakpoint));
    }

    internal FilterBuilder(List<FilterRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the filter to none.
    /// </summary>
    public FilterBuilder None => ChainWithFilter("none");
    /// <summary>
    /// Sets the filter to blur.
    /// </summary>
    public FilterBuilder Blur => ChainWithFilter("blur");
    /// <summary>
    /// Sets the filter to brightness.
    /// </summary>
    public FilterBuilder Brightness => ChainWithFilter("brightness");
    /// <summary>
    /// Sets the filter to contrast.
    /// </summary>
    public FilterBuilder Contrast => ChainWithFilter("contrast");
    /// <summary>
    /// Sets the filter to drop-shadow.
    /// </summary>
    public FilterBuilder DropShadow => ChainWithFilter("drop-shadow");
    /// <summary>
    /// Sets the filter to grayscale.
    /// </summary>
    public FilterBuilder Grayscale => ChainWithFilter("grayscale");
    /// <summary>
    /// Sets the filter to hue-rotate.
    /// </summary>
    public FilterBuilder HueRotate => ChainWithFilter("hue-rotate");
    /// <summary>
    /// Sets the filter to invert.
    /// </summary>
    public FilterBuilder Invert => ChainWithFilter("invert");
    /// <summary>
    /// Sets the filter to opacity.
    /// </summary>
    public FilterBuilder Opacity => ChainWithFilter("opacity");
    /// <summary>
    /// Sets the filter to saturate.
    /// </summary>
    public FilterBuilder Saturate => ChainWithFilter("saturate");
    /// <summary>
    /// Sets the filter to sepia.
    /// </summary>
    public FilterBuilder Sepia => ChainWithFilter("sepia");

    /// <summary>
    /// Applies an exact Tailwind filter utility token, e.g. "blur-xs".
    /// </summary>
    public FilterBuilder Token(string token) => ChainWithFilter(token);

    /// <summary>
    /// Applies the filter on phone breakpoint.
    /// </summary>
    public FilterBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the filter on small breakpoint (≥640px).
    /// </summary>
    public FilterBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the filter on tablet breakpoint.
    /// </summary>
    public FilterBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the filter on laptop breakpoint.
    /// </summary>
    public FilterBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the filter on desktop breakpoint.
    /// </summary>
    public FilterBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the filter on the 2xl breakpoint.
    /// </summary>
    public FilterBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FilterBuilder ChainWithFilter(string filter)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new FilterRule(filter, bp));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FilterBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
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
            var cls = GetFilterClass(rule.Filter);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
            var rule = _rules[i];
            var filterValue = GetFilterValue(rule.Filter);

            if (filterValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("filter: ");
            sb.Append(filterValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetFilterClass(string filter)
    {
        return filter switch
        {
            "none" => _classFilterNone,
            "blur" => _classFilterBlur,
            "brightness" => _classFilterBrightness,
            "contrast" => _classFilterContrast,
            "drop-shadow" => _classFilterDropShadow,
            "grayscale" => _classFilterGrayscale,
            "hue-rotate" => _classFilterHueRotate,
            "invert" => _classFilterInvert,
            "opacity" => _classFilterOpacity,
            "saturate" => _classFilterSaturate,
            "sepia" => _classFilterSepia,
            _ when IsRawFilterUtility(filter) => filter,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetFilterValue(string filter)
    {
        return filter switch
        {
            "none" => "none",
            "blur" => "blur(8px)",
            "brightness" => "brightness(1.1)",
            "contrast" => "contrast(1.1)",
            "drop-shadow" => "drop-shadow(0 4px 6px -1px rgb(0 0 0 / 0.1))",
            "grayscale" => "grayscale(1)",
            "hue-rotate" => "hue-rotate(90deg)",
            "invert" => "invert(1)",
            "opacity" => "opacity(0.5)",
            "saturate" => "saturate(1.1)",
            "sepia" => "sepia(1)",
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool IsRawFilterUtility(string filter)
    {
        return filter.Equals("filter", StringComparison.Ordinal) ||
               filter.StartsWith("filter-", StringComparison.Ordinal) ||
               filter.Equals("blur", StringComparison.Ordinal) ||
               filter.StartsWith("blur-", StringComparison.Ordinal) ||
               filter.Equals("brightness", StringComparison.Ordinal) ||
               filter.StartsWith("brightness-", StringComparison.Ordinal) ||
               filter.Equals("contrast", StringComparison.Ordinal) ||
               filter.StartsWith("contrast-", StringComparison.Ordinal) ||
               filter.Equals("drop-shadow", StringComparison.Ordinal) ||
               filter.StartsWith("drop-shadow-", StringComparison.Ordinal) ||
               filter.Equals("grayscale", StringComparison.Ordinal) ||
               filter.StartsWith("hue-rotate", StringComparison.Ordinal) ||
               filter.Equals("invert", StringComparison.Ordinal) ||
               filter.StartsWith("opacity-", StringComparison.Ordinal) ||
               filter.Equals("opacity", StringComparison.Ordinal) ||
               filter.Equals("saturate", StringComparison.Ordinal) ||
               filter.StartsWith("saturate-", StringComparison.Ordinal) ||
               filter.Equals("sepia", StringComparison.Ordinal);
    }

    /// <summary>
    /// Returns the CSS class string representation of this filter builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}