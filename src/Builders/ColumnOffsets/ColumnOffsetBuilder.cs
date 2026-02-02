using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified column offset builder with fluent API for chaining column offset rules.
/// </summary>
public sealed class ColumnOffsetBuilder : ICssBuilder
{
    private readonly List<ColumnOffsetRule> _rules = new(4);

    private const string _classOffset1 = "offset-1";
    private const string _classOffset2 = "offset-2";
    private const string _classOffset3 = "offset-3";
    private const string _classOffset4 = "offset-4";
    private const string _classOffset5 = "offset-5";
    private const string _classOffset6 = "offset-6";
    private const string _classOffset7 = "offset-7";
    private const string _classOffset8 = "offset-8";
    private const string _classOffset9 = "offset-9";
    private const string _classOffset10 = "offset-10";
    private const string _classOffset11 = "offset-11";
    private const string _classOffset12 = "offset-12";

    internal ColumnOffsetBuilder(string offset, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ColumnOffsetRule(offset, breakpoint));
    }


    internal ColumnOffsetBuilder(List<ColumnOffsetRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the column offset to 1.
    /// </summary>
    public ColumnOffsetBuilder O1 => ChainWithOffset("1");
    /// <summary>
    /// Sets the column offset to 2.
    /// </summary>
    public ColumnOffsetBuilder O2 => ChainWithOffset("2");
    /// <summary>
    /// Sets the column offset to 3.
    /// </summary>
    public ColumnOffsetBuilder O3 => ChainWithOffset("3");
    /// <summary>
    /// Sets the column offset to 4.
    /// </summary>
    public ColumnOffsetBuilder O4 => ChainWithOffset("4");
    /// <summary>
    /// Sets the column offset to 5.
    /// </summary>
    public ColumnOffsetBuilder O5 => ChainWithOffset("5");
    /// <summary>
    /// Sets the column offset to 6.
    /// </summary>
    public ColumnOffsetBuilder O6 => ChainWithOffset("6");
    /// <summary>
    /// Sets the column offset to 7.
    /// </summary>
    public ColumnOffsetBuilder O7 => ChainWithOffset("7");
    /// <summary>
    /// Sets the column offset to 8.
    /// </summary>
    public ColumnOffsetBuilder O8 => ChainWithOffset("8");
    /// <summary>
    /// Sets the column offset to 9.
    /// </summary>
    public ColumnOffsetBuilder O9 => ChainWithOffset("9");
    /// <summary>
    /// Sets the column offset to 10.
    /// </summary>
    public ColumnOffsetBuilder O10 => ChainWithOffset("10");
    /// <summary>
    /// Sets the column offset to 11.
    /// </summary>
    public ColumnOffsetBuilder O11 => ChainWithOffset("11");
    /// <summary>
    /// Sets the column offset to 12.
    /// </summary>
    public ColumnOffsetBuilder O12 => ChainWithOffset("12");

    /// <summary>
    /// Applies the column offset on phone breakpoint.
    /// </summary>
    public ColumnOffsetBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    /// <summary>
    /// Applies the column offset on tablet breakpoint.
    /// </summary>
    public ColumnOffsetBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    /// <summary>
    /// Applies the column offset on laptop breakpoint.
    /// </summary>
    public ColumnOffsetBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    /// <summary>
    /// Applies the column offset on desktop breakpoint.
    /// </summary>
    public ColumnOffsetBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    /// <summary>
    /// Applies the column offset on widescreen breakpoint.
    /// </summary>
    public ColumnOffsetBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the column offset on ultrawide breakpoint.
    /// </summary>
    public ColumnOffsetBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnOffsetBuilder ChainWithOffset(string offset)
    {
        _rules.Add(new ColumnOffsetRule(offset, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnOffsetBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ColumnOffsetRule("1", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ColumnOffsetRule(last.Offset, breakpoint);
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
            var cls = GetColumnOffsetClass(rule.Offset);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
    /// Column offsets are typically handled via Bootstrap classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as column offsets are handled via classes.</returns>
    public string ToStyle()
    {
        // Column offsets are typically handled via Bootstrap classes, not inline styles
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetColumnOffsetClass(string offset)
    {
        return offset switch
        {
            "1" => _classOffset1,
            "2" => _classOffset2,
            "3" => _classOffset3,
            "4" => _classOffset4,
            "5" => _classOffset5,
            "6" => _classOffset6,
            "7" => _classOffset7,
            "8" => _classOffset8,
            "9" => _classOffset9,
            "10" => _classOffset10,
            "11" => _classOffset11,
            "12" => _classOffset12,
            _ => string.Empty
        };
    }

}
