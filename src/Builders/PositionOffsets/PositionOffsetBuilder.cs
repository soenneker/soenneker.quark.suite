using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified position offset builder with fluent API for chaining position offset rules.
/// </summary>
public sealed class PositionOffsetBuilder : ICssBuilder
{
    private readonly List<PositionOffsetRule> _rules = new(6);

    private const string _classTranslateMiddle = "translate-middle";
    private const string _classTranslateMiddleX = "translate-middle-x";
    private const string _classTranslateMiddleY = "translate-middle-y";

    internal PositionOffsetBuilder(string property, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new PositionOffsetRule(property, value, breakpoint));
    }

    internal PositionOffsetBuilder(List<PositionOffsetRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the top offset to 0.
    /// </summary>
    public PositionOffsetBuilder Top0 => Chain("top", "0");

    /// <summary>
    /// Sets the top offset to 50%.
    /// </summary>
    public PositionOffsetBuilder Top50 => Chain("top", "50");

    /// <summary>
    /// Sets the top offset to 100%.
    /// </summary>
    public PositionOffsetBuilder Top100 => Chain("top", "100");

    /// <summary>
    /// Sets the bottom offset to 0.
    /// </summary>
    public PositionOffsetBuilder Bottom0 => Chain("bottom", "0");

    /// <summary>
    /// Sets the bottom offset to 50%.
    /// </summary>
    public PositionOffsetBuilder Bottom50 => Chain("bottom", "50");

    /// <summary>
    /// Sets the bottom offset to 100%.
    /// </summary>
    public PositionOffsetBuilder Bottom100 => Chain("bottom", "100");

    /// <summary>
    /// Sets the start (inline-start) offset to 0.
    /// </summary>
    public PositionOffsetBuilder Start0 => Chain("start", "0");

    /// <summary>
    /// Sets the start (inline-start) offset to 50%.
    /// </summary>
    public PositionOffsetBuilder Start50 => Chain("start", "50");

    /// <summary>
    /// Sets the start (inline-start) offset to 100%.
    /// </summary>
    public PositionOffsetBuilder Start100 => Chain("start", "100");

    /// <summary>
    /// Sets the end (inline-end) offset to 0.
    /// </summary>
    public PositionOffsetBuilder End0 => Chain("end", "0");

    /// <summary>
    /// Sets the end (inline-end) offset to 50%.
    /// </summary>
    public PositionOffsetBuilder End50 => Chain("end", "50");

    /// <summary>
    /// Sets the end (inline-end) offset to 100%.
    /// </summary>
    public PositionOffsetBuilder End100 => Chain("end", "100");

    /// <summary>
    /// Sets the translate transform to middle (both X and Y).
    /// </summary>
    public PositionOffsetBuilder TranslateMiddle => Chain("translate", "middle");

    /// <summary>
    /// Sets the translate transform to middle X.
    /// </summary>
    public PositionOffsetBuilder TranslateMiddleX => Chain("translate", "middle-x");

    /// <summary>
    /// Sets the translate transform to middle Y.
    /// </summary>
    public PositionOffsetBuilder TranslateMiddleY => Chain("translate", "middle-y");

    /// <summary>
    /// Applies the position offset on phone breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnPhone => ChainBp(BreakpointType.Phone);

    /// <summary>
    /// Applies the position offset on tablet breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnTablet => ChainBp(BreakpointType.Tablet);

    /// <summary>
    /// Applies the position offset on laptop breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnLaptop => ChainBp(BreakpointType.Laptop);

    /// <summary>
    /// Applies the position offset on desktop breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnDesktop => ChainBp(BreakpointType.Desktop);

    /// <summary>
    /// Applies the position offset on widescreen breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);

    /// <summary>
    /// Applies the position offset on ultrawide breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionOffsetBuilder Chain(string property, string value)
    {
        _rules.Add(new PositionOffsetRule(property, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionOffsetBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new PositionOffsetRule("top", "0", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new PositionOffsetRule(last.Property, last.Value, bp);
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
            var cls = GetClass(rule.Property, rule.Value);
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
            var css = GetStyle(rule.Property, rule.Value);
            if (css is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(string property, string value)
    {
        if (property == "translate")
        {
            return value switch
            {
                "middle" => _classTranslateMiddle,
                "middle-x" => _classTranslateMiddleX,
                "middle-y" => _classTranslateMiddleY,
                _ => string.Empty
            };
        }

        var prefix = property switch
        {
            "top" => "top",
            "bottom" => "bottom",
            "start" => "start",
            "end" => "end",
            _ => string.Empty
        };
        if (prefix.Length == 0) return string.Empty;
        return $"{prefix}-{value}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetStyle(string property, string value)
    {
        if (property == "translate") return null;
        var cssProp = property switch
        {
            "top" => "top",
            "bottom" => "bottom",
            "start" => "inset-inline-start",
            "end" => "inset-inline-end",
            _ => string.Empty
        };
        if (cssProp.Length == 0) return null;
        var cssVal = value switch
        {
            "0" => "0",
            "50" => "50%",
            "100" => "100%",
            _ => string.Empty
        };
        if (cssVal.Length == 0) return null;
        return $"{cssProp}: {cssVal}";
    }

}

