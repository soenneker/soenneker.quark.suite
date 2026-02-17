using Soenneker.Quark.Enums;
using Soenneker.Extensions.String;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

/// <summary>
/// Simplified gap builder with fluent API for chaining gap rules.
/// </summary>
public sealed class GapBuilder : ICssBuilder
{
    private readonly List<GapRule> _rules = new(4);

    // Tailwind: gap-*, gap-x-* (column), gap-y-* (row) — shadcn/Tailwind compatible
    private const string _classGap0 = "gap-0";
    private const string _classGap1 = "gap-1";
    private const string _classGap2 = "gap-2";
    private const string _classGap3 = "gap-3";
    private const string _classGap4 = "gap-4";
    private const string _classGap5 = "gap-5";
    private const string _classGapX0 = "gap-x-0";
    private const string _classGapX1 = "gap-x-1";
    private const string _classGapX2 = "gap-x-2";
    private const string _classGapX3 = "gap-x-3";
    private const string _classGapX4 = "gap-x-4";
    private const string _classGapX5 = "gap-x-5";
    private const string _classGapY0 = "gap-y-0";
    private const string _classGapY1 = "gap-y-1";
    private const string _classGapY2 = "gap-y-2";
    private const string _classGapY3 = "gap-y-3";
    private const string _classGapY4 = "gap-y-4";
    private const string _classGapY5 = "gap-y-5";
    private const string _stylePrefix = "gap: ";

    internal GapBuilder(string size, BreakpointType? breakpoint = null, string direction = "")
    {
        if (size.HasContent())
            _rules.Add(new GapRule(size, breakpoint, direction));
    }

    internal GapBuilder(List<GapRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Chain with a new size for the next rule.
    /// </summary>
    public GapBuilder Is0 => ChainWithSize(ScaleType.Is0Value);

    /// <summary>
    /// Chain with a new size for the next rule.
    /// </summary>
    public GapBuilder Is1 => ChainWithSize(ScaleType.Is1Value);

    /// <summary>
    /// Chain with a new size for the next rule.
    /// </summary>
    public GapBuilder Is2 => ChainWithSize(ScaleType.Is2Value);

    /// <summary>
    /// Chain with a new size for the next rule.
    /// </summary>
    public GapBuilder Is3 => ChainWithSize(ScaleType.Is3Value);

    /// <summary>
    /// Chain with a new size for the next rule.
    /// </summary>
    public GapBuilder Is4 => ChainWithSize(ScaleType.Is4Value);

    /// <summary>
    /// Chain with a new size for the next rule.
    /// </summary>
    public GapBuilder Is5 => ChainWithSize(ScaleType.Is5Value);

    /// <summary>
    /// Apply to column gap only.
    /// </summary>
    public GapBuilder Column => ChainWithDirection("column");

    /// <summary>
    /// Apply to row gap only.
    /// </summary>
    public GapBuilder Row => ChainWithDirection("row");

    /// <summary>
    /// Apply on phone devices (portrait phones, less than 576px).
    /// </summary>
    public GapBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Apply on mobile devices (landscape phones, 576px and up).
    /// </summary>
    public GapBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);

    /// <summary>
    /// Apply on laptop devices (laptops, 992px and up).
    /// </summary>
    public GapBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);

    /// <summary>
    /// Apply on desktop devices (desktops, 1200px and up).
    /// </summary>
    public GapBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);

    /// <summary>
    /// Apply on wide screen devices (larger desktops, 1400px and up).
    /// </summary>
    public GapBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);

    /// <summary>
    /// Apply on ultrawide devices (ultrawide screens, 1920px and up).
    /// </summary>
    public GapBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GapBuilder ChainWithSize(string size)
    {
        _rules.Add(new GapRule(size, null, ""));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GapBuilder ChainWithDirection(string direction)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new GapRule(ScaleType.Is0Value, null, direction));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        GapRule last = _rules[lastIdx];
        _rules[lastIdx] = new GapRule(last.Size, last.Breakpoint, direction);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GapBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new GapRule(ScaleType.Is0Value, breakpoint, ""));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        GapRule last = _rules[lastIdx];
        _rules[lastIdx] = new GapRule(last.Size, breakpoint, last.Direction);
        return this;
    }

    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            GapRule rule = _rules[i];
            string cls = GetSizeClass(rule.Size, rule.Direction);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);

            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) 
                sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            GapRule rule = _rules[i];
            string? sizeValue = GetSizeValue(rule.Size);
            if (sizeValue is null)
                continue;

            if (!first) sb.Append("; ");
            else first = false;

            if (rule.Direction == "column")
            {
                sb.Append("column-gap: ");
            }
            else if (rule.Direction == "row")
            {
                sb.Append("row-gap: ");
            }
            else
            {
                sb.Append(_stylePrefix);
            }
            sb.Append(sizeValue);
        }

        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetSizeClass(string size, string direction)
    {
        if (direction == "column")
        {
            return size switch
            {
                ScaleType.Is0Value => _classGapX0,
                ScaleType.Is1Value => _classGapX1,
                ScaleType.Is2Value => _classGapX2,
                ScaleType.Is3Value => _classGapX3,
                ScaleType.Is4Value => _classGapX4,
                ScaleType.Is5Value => _classGapX5,
                _ => string.Empty
            };
        }
        else if (direction == "row")
        {
            return size switch
            {
                ScaleType.Is0Value => _classGapY0,
                ScaleType.Is1Value => _classGapY1,
                ScaleType.Is2Value => _classGapY2,
                ScaleType.Is3Value => _classGapY3,
                ScaleType.Is4Value => _classGapY4,
                ScaleType.Is5Value => _classGapY5,
                _ => string.Empty
            };
        }
        else
        {
            return size switch
            {
                ScaleType.Is0Value => _classGap0,
                ScaleType.Is1Value => _classGap1,
                ScaleType.Is2Value => _classGap2,
                ScaleType.Is3Value => _classGap3,
                ScaleType.Is4Value => _classGap4,
                ScaleType.Is5Value => _classGap5,
                _ => string.Empty
            };
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string? GetSizeValue(string size)
    {
        return size switch
        {
            ScaleType.Is0Value => "0",
            ScaleType.Is1Value => "0.25rem",
            ScaleType.Is2Value => "0.5rem",
            ScaleType.Is3Value => "1rem",
            ScaleType.Is4Value => "1.5rem",
            ScaleType.Is5Value => "3rem",
            _ => size
        };
    }

}
