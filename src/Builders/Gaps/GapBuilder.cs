using Soenneker.Quark.Enums;
using Soenneker.Extensions.String;

using System;
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

    private const string _classGap0 = "gap-0";
    private const string _classGap1 = "gap-1";
    private const string _classGap2 = "gap-2";
    private const string _classGap3 = "gap-3";
    private const string _classGap4 = "gap-4";
    private const string _classGap5 = "gap-5";
    private const string _classColumnGap0 = "column-gap-0";
    private const string _classColumnGap1 = "column-gap-1";
    private const string _classColumnGap2 = "column-gap-2";
    private const string _classColumnGap3 = "column-gap-3";
    private const string _classColumnGap4 = "column-gap-4";
    private const string _classColumnGap5 = "column-gap-5";
    private const string _classRowGap0 = "row-gap-0";
    private const string _classRowGap1 = "row-gap-1";
    private const string _classRowGap2 = "row-gap-2";
    private const string _classRowGap3 = "row-gap-3";
    private const string _classRowGap4 = "row-gap-4";
    private const string _classRowGap5 = "row-gap-5";
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
    public GapBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);

    /// <summary>
    /// Apply on mobile devices (landscape phones, 576px and up).
    /// </summary>
    public GapBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);

    /// <summary>
    /// Apply on laptop devices (laptops, 992px and up).
    /// </summary>
    public GapBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);

    /// <summary>
    /// Apply on desktop devices (desktops, 1200px and up).
    /// </summary>
    public GapBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);

    /// <summary>
    /// Apply on wide screen devices (larger desktops, 1400px and up).
    /// </summary>
    public GapBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public GapBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new GapRule(last.Size, last.breakpoint, direction);
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

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
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
            var rule = _rules[i];
            var cls = GetSizeClass(rule.Size, rule.Direction);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);

            if (bp.Length != 0)
                cls = InsertBreakpointType(cls, bp);

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
            var rule = _rules[i];
            var sizeValue = GetSizeValue(rule.Size);
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
                ScaleType.Is0Value => _classColumnGap0,
                ScaleType.Is1Value => _classColumnGap1,
                ScaleType.Is2Value => _classColumnGap2,
                ScaleType.Is3Value => _classColumnGap3,
                ScaleType.Is4Value => _classColumnGap4,
                ScaleType.Is5Value => _classColumnGap5,
                _ => string.Empty
            };
        }
        else if (direction == "row")
        {
            return size switch
            {
                ScaleType.Is0Value => _classRowGap0,
                ScaleType.Is1Value => _classRowGap1,
                ScaleType.Is2Value => _classRowGap2,
                ScaleType.Is3Value => _classRowGap3,
                ScaleType.Is4Value => _classRowGap4,
                ScaleType.Is5Value => _classRowGap5,
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
            _ => null
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        var dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            var len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                var idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            var idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}
