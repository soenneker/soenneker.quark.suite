using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified column order builder with fluent API for chaining column order rules.
/// </summary>
public sealed class ColumnOrderBuilder : ICssBuilder
{
    private readonly List<ColumnOrderRule> _rules = new(4);

    private const string _classOrder1 = "order-1";
    private const string _classOrder2 = "order-2";
    private const string _classOrder3 = "order-3";
    private const string _classOrder4 = "order-4";
    private const string _classOrder5 = "order-5";
    private const string _classOrder6 = "order-6";
    private const string _classOrder7 = "order-7";
    private const string _classOrder8 = "order-8";
    private const string _classOrder9 = "order-9";
    private const string _classOrder10 = "order-10";
    private const string _classOrder11 = "order-11";
    private const string _classOrder12 = "order-12";
    private const string _classOrderFirst = "order-first";
    private const string _classOrderLast = "order-last";

    internal ColumnOrderBuilder(string order, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ColumnOrderRule(order, breakpoint));
    }


    internal ColumnOrderBuilder(List<ColumnOrderRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the column order to 1.
    /// </summary>
    public ColumnOrderBuilder O1 => ChainWithOrder("1");
    /// <summary>
    /// Sets the column order to 2.
    /// </summary>
    public ColumnOrderBuilder O2 => ChainWithOrder("2");
    /// <summary>
    /// Sets the column order to 3.
    /// </summary>
    public ColumnOrderBuilder O3 => ChainWithOrder("3");
    /// <summary>
    /// Sets the column order to 4.
    /// </summary>
    public ColumnOrderBuilder O4 => ChainWithOrder("4");
    /// <summary>
    /// Sets the column order to 5.
    /// </summary>
    public ColumnOrderBuilder O5 => ChainWithOrder("5");
    /// <summary>
    /// Sets the column order to 6.
    /// </summary>
    public ColumnOrderBuilder O6 => ChainWithOrder("6");
    /// <summary>
    /// Sets the column order to 7.
    /// </summary>
    public ColumnOrderBuilder O7 => ChainWithOrder("7");
    /// <summary>
    /// Sets the column order to 8.
    /// </summary>
    public ColumnOrderBuilder O8 => ChainWithOrder("8");
    /// <summary>
    /// Sets the column order to 9.
    /// </summary>
    public ColumnOrderBuilder O9 => ChainWithOrder("9");
    /// <summary>
    /// Sets the column order to 10.
    /// </summary>
    public ColumnOrderBuilder O10 => ChainWithOrder("10");
    /// <summary>
    /// Sets the column order to 11.
    /// </summary>
    public ColumnOrderBuilder O11 => ChainWithOrder("11");
    /// <summary>
    /// Sets the column order to 12.
    /// </summary>
    public ColumnOrderBuilder O12 => ChainWithOrder("12");
    /// <summary>
    /// Sets the column order to first.
    /// </summary>
    public ColumnOrderBuilder First => ChainWithOrder("first");
    /// <summary>
    /// Sets the column order to last.
    /// </summary>
    public ColumnOrderBuilder Last => ChainWithOrder("last");

    /// <summary>
    /// Applies the column order at base (mobile-first default).
    /// </summary>
    public ColumnOrderBuilder OnBase => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the column order at sm breakpoint (≥640px).
    /// </summary>
    public ColumnOrderBuilder OnSm => ChainWithBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the column order at md breakpoint (≥768px).
    /// </summary>
    public ColumnOrderBuilder OnMd => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the column order at lg breakpoint (≥1024px).
    /// </summary>
    public ColumnOrderBuilder OnLg => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the column order at xl breakpoint (≥1280px).
    /// </summary>
    public ColumnOrderBuilder OnXl => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the column order at 2xl breakpoint (≥1536px).
    /// </summary>
    public ColumnOrderBuilder On2xl => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnOrderBuilder ChainWithOrder(string order)
    {
        _rules.Add(new ColumnOrderRule(order, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnOrderBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ColumnOrderRule("1", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        ColumnOrderRule last = _rules[lastIdx];
        _rules[lastIdx] = new ColumnOrderRule(last.Order, breakpoint);
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
            ColumnOrderRule rule = _rules[i];
            string cls = GetColumnOrderClass(rule.Order);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
    /// Column orders are typically handled via Bootstrap classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as column orders are handled via classes.</returns>
    public string ToStyle()
    {
        // Column orders are typically handled via Bootstrap classes, not inline styles
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetColumnOrderClass(string order)
    {
        return order switch
        {
            "1" => _classOrder1,
            "2" => _classOrder2,
            "3" => _classOrder3,
            "4" => _classOrder4,
            "5" => _classOrder5,
            "6" => _classOrder6,
            "7" => _classOrder7,
            "8" => _classOrder8,
            "9" => _classOrder9,
            "10" => _classOrder10,
            "11" => _classOrder11,
            "12" => _classOrder12,
            "first" => _classOrderFirst,
            "last" => _classOrderLast,
            _ => string.Empty
        };
    }

}
