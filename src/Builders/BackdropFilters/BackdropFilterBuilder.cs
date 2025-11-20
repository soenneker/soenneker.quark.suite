using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified backdrop filter builder with fluent API for chaining backdrop filter rules.
/// </summary>
public sealed class BackdropFilterBuilder : ICssBuilder
{
    private readonly List<BackdropFilterRule> _rules = new(4);

    private const string _classBackdropFilterNone = "backdrop-filter-none";
    private const string _classBackdropFilterBlur = "backdrop-blur";
    private const string _classBackdropFilterBrightness = "backdrop-brightness";
    private const string _classBackdropFilterContrast = "backdrop-contrast";
    private const string _classBackdropFilterGrayscale = "backdrop-grayscale";
    private const string _classBackdropFilterHueRotate = "backdrop-hue-rotate";
    private const string _classBackdropFilterInvert = "backdrop-invert";
    private const string _classBackdropFilterOpacity = "backdrop-opacity";
    private const string _classBackdropFilterSaturate = "backdrop-saturate";
    private const string _classBackdropFilterSepia = "backdrop-sepia";

    internal BackdropFilterBuilder(string filter, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackdropFilterRule(filter, breakpoint));
    }

    internal BackdropFilterBuilder(List<BackdropFilterRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the backdrop filter to none.
    /// </summary>
    public BackdropFilterBuilder None => ChainWithFilter("none");
    /// <summary>
    /// Sets the backdrop filter to blur.
    /// </summary>
    public BackdropFilterBuilder Blur => ChainWithFilter("blur");
    /// <summary>
    /// Sets the backdrop filter to brightness.
    /// </summary>
    public BackdropFilterBuilder Brightness => ChainWithFilter("brightness");
    /// <summary>
    /// Sets the backdrop filter to contrast.
    /// </summary>
    public BackdropFilterBuilder Contrast => ChainWithFilter("contrast");
    /// <summary>
    /// Sets the backdrop filter to grayscale.
    /// </summary>
    public BackdropFilterBuilder Grayscale => ChainWithFilter("grayscale");
    /// <summary>
    /// Sets the backdrop filter to hue-rotate.
    /// </summary>
    public BackdropFilterBuilder HueRotate => ChainWithFilter("hue-rotate");
    /// <summary>
    /// Sets the backdrop filter to invert.
    /// </summary>
    public BackdropFilterBuilder Invert => ChainWithFilter("invert");
    /// <summary>
    /// Sets the backdrop filter to opacity.
    /// </summary>
    public BackdropFilterBuilder Opacity => ChainWithFilter("opacity");
    /// <summary>
    /// Sets the backdrop filter to saturate.
    /// </summary>
    public BackdropFilterBuilder Saturate => ChainWithFilter("saturate");
    /// <summary>
    /// Sets the backdrop filter to sepia.
    /// </summary>
    public BackdropFilterBuilder Sepia => ChainWithFilter("sepia");

    /// <summary>
    /// Applies the backdrop filter on phone breakpoint.
    /// </summary>
    public BackdropFilterBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    /// <summary>
    /// Applies the backdrop filter on tablet breakpoint.
    /// </summary>
    public BackdropFilterBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    /// <summary>
    /// Applies the backdrop filter on laptop breakpoint.
    /// </summary>
    public BackdropFilterBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    /// <summary>
    /// Applies the backdrop filter on desktop breakpoint.
    /// </summary>
    public BackdropFilterBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    /// <summary>
    /// Applies the backdrop filter on widescreen breakpoint.
    /// </summary>
    public BackdropFilterBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the backdrop filter on ultrawide breakpoint.
    /// </summary>
    public BackdropFilterBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackdropFilterBuilder ChainWithFilter(string filter)
    {
        _rules.Add(new BackdropFilterRule(filter, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackdropFilterBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new BackdropFilterRule("none", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new BackdropFilterRule(last.Filter, breakpoint);
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
            var cls = GetBackdropFilterClass(rule.Filter);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var filterValue = GetBackdropFilterValue(rule.Filter);

            if (filterValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("backdrop-filter: ");
            sb.Append(filterValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBackdropFilterClass(string filter)
    {
        return filter switch
        {
            "none" => _classBackdropFilterNone,
            "blur" => _classBackdropFilterBlur,
            "brightness" => _classBackdropFilterBrightness,
            "contrast" => _classBackdropFilterContrast,
            "grayscale" => _classBackdropFilterGrayscale,
            "hue-rotate" => _classBackdropFilterHueRotate,
            "invert" => _classBackdropFilterInvert,
            "opacity" => _classBackdropFilterOpacity,
            "saturate" => _classBackdropFilterSaturate,
            "sepia" => _classBackdropFilterSepia,
            _ => string.Empty
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetBackdropFilterValue(string filter)
    {
        return filter switch
        {
            "none" => "none",
            "blur" => "blur(8px)",
            "brightness" => "brightness(1.1)",
            "contrast" => "contrast(1.1)",
            "grayscale" => "grayscale(1)",
            "hue-rotate" => "hue-rotate(90deg)",
            "invert" => "invert(1)",
            "opacity" => "opacity(0.5)",
            "saturate" => "saturate(1.1)",
            "sepia" => "sepia(1)",
            _ => null
        };
    }


    public override string ToString()
    {
        return ToClass();
    }
}

