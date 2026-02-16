using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified column size builder with fluent API for chaining column size rules.
/// </summary>
public sealed class ColumnSizeBuilder : ICssBuilder
{
    private readonly List<ColumnSizeRule> _rules = new(4);

    // Tailwind grid: col-span-1..col-span-12, col-auto
    private const string _classCol1 = "col-span-1";
    private const string _classCol2 = "col-span-2";
    private const string _classCol3 = "col-span-3";
    private const string _classCol4 = "col-span-4";
    private const string _classCol5 = "col-span-5";
    private const string _classCol6 = "col-span-6";
    private const string _classCol7 = "col-span-7";
    private const string _classCol8 = "col-span-8";
    private const string _classCol9 = "col-span-9";
    private const string _classCol10 = "col-span-10";
    private const string _classCol11 = "col-span-11";
    private const string _classCol12 = "col-span-12";
    private const string _classColAuto = "col-auto";

    internal ColumnSizeBuilder(string size, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ColumnSizeRule(size, breakpoint));
    }


    internal ColumnSizeBuilder(List<ColumnSizeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Sets the column size to 1.
    /// </summary>
    public ColumnSizeBuilder Is1 => ChainWithSize("1");
    /// <summary>
    /// Sets the column size to 2.
    /// </summary>
    public ColumnSizeBuilder Is2 => ChainWithSize("2");
    /// <summary>
    /// Sets the column size to 3.
    /// </summary>
    public ColumnSizeBuilder Is3 => ChainWithSize("3");
    /// <summary>
    /// Sets the column size to 4.
    /// </summary>
    public ColumnSizeBuilder Is4 => ChainWithSize("4");
    /// <summary>
    /// Sets the column size to 5.
    /// </summary>
    public ColumnSizeBuilder Is5 => ChainWithSize("5");
    /// <summary>
    /// Sets the column size to 6.
    /// </summary>
    public ColumnSizeBuilder Is6 => ChainWithSize("6");
    /// <summary>
    /// Sets the column size to 7.
    /// </summary>
    public ColumnSizeBuilder Is7 => ChainWithSize("7");
    /// <summary>
    /// Sets the column size to 8.
    /// </summary>
    public ColumnSizeBuilder Is8 => ChainWithSize("8");
    /// <summary>
    /// Sets the column size to 9.
    /// </summary>
    public ColumnSizeBuilder Is9 => ChainWithSize("9");
    /// <summary>
    /// Sets the column size to 10.
    /// </summary>
    public ColumnSizeBuilder Is10 => ChainWithSize("10");
    /// <summary>
    /// Sets the column size to 11.
    /// </summary>
    public ColumnSizeBuilder Is11 => ChainWithSize("11");
    /// <summary>
    /// Sets the column size to 12.
    /// </summary>
    public ColumnSizeBuilder Is12 => ChainWithSize("12");
    /// <summary>
    /// Sets the column size to auto.
    /// </summary>
    public ColumnSizeBuilder Auto => ChainWithSize("auto");

    /// <summary>
    /// Applies the column size at base (mobile-first default).
    /// </summary>
    public ColumnSizeBuilder OnBase => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the column size at sm breakpoint (≥640px).
    /// </summary>
    public ColumnSizeBuilder OnSm => ChainWithBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies the column size at md breakpoint (≥768px).
    /// </summary>
    public ColumnSizeBuilder OnMd => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the column size at lg breakpoint (≥1024px).
    /// </summary>
    public ColumnSizeBuilder OnLg => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the column size at xl breakpoint (≥1280px).
    /// </summary>
    public ColumnSizeBuilder OnXl => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the column size at 2xl breakpoint (≥1536px).
    /// </summary>
    public ColumnSizeBuilder On2xl => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnSizeBuilder ChainWithSize(string size)
    {
        _rules.Add(new ColumnSizeRule(size, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnSizeBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ColumnSizeRule("12", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        ColumnSizeRule last = _rules[lastIdx];
        _rules[lastIdx] = new ColumnSizeRule(last.Size, breakpoint);
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
            ColumnSizeRule rule = _rules[i];
            string cls = GetColumnSizeClass(rule.Size);
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
    /// Column sizes are typically handled via Bootstrap classes, not inline styles.
    /// </summary>
    /// <returns>An empty string, as column sizes are handled via classes.</returns>
    public string ToStyle()
    {
        // Column sizes are typically handled via Bootstrap classes, not inline styles
        return string.Empty;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetColumnSizeClass(string size)
    {
        return size switch
        {
            "1" => _classCol1,
            "2" => _classCol2,
            "3" => _classCol3,
            "4" => _classCol4,
            "5" => _classCol5,
            "6" => _classCol6,
            "7" => _classCol7,
            "8" => _classCol8,
            "9" => _classCol9,
            "10" => _classCol10,
            "11" => _classCol11,
            "12" => _classCol12,
            "auto" => _classColAuto,
            _ => string.Empty
        };
    }

}
