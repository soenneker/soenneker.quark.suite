using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified position offset builder with fluent API for chaining position offset rules.
/// </summary>
[TailwindPrefix("top-", Responsive = true)]
public sealed class PositionOffsetBuilder : ICssBuilder
{
    private readonly List<PositionOffsetRule> _rules = new(6);
    private BreakpointType? _pendingBreakpoint;

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
    public PositionOffsetBuilder OnBase => ChainBp(BreakpointType.Base);

    /// <summary>
    /// Applies the position offset on small breakpoint (≥640px).
    /// </summary>
    public PositionOffsetBuilder OnSm => ChainBp(BreakpointType.Sm);

    /// <summary>
    /// Applies the position offset on tablet breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnMd => ChainBp(BreakpointType.Md);

    /// <summary>
    /// Applies the position offset on laptop breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnLg => ChainBp(BreakpointType.Lg);

    /// <summary>
    /// Applies the position offset on desktop breakpoint.
    /// </summary>
    public PositionOffsetBuilder OnXl => ChainBp(BreakpointType.Xl);

    /// <summary>
    /// Applies the position offset on the 2xl breakpoint.
    /// </summary>
    public PositionOffsetBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionOffsetBuilder Chain(string property, string value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new PositionOffsetRule(property, value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private PositionOffsetBuilder ChainBp(BreakpointType bp)
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
            var cls = GetClass(rule.Property, rule.Value);
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

    public override string ToString() => ToClass();
}