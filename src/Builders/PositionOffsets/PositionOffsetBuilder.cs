using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark;
using Soenneker.Quark.Enums;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

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

    public PositionOffsetBuilder Top0 => Chain("top", "0");
    public PositionOffsetBuilder Top50 => Chain("top", "50");
    public PositionOffsetBuilder Top100 => Chain("top", "100");

    public PositionOffsetBuilder Bottom0 => Chain("bottom", "0");
    public PositionOffsetBuilder Bottom50 => Chain("bottom", "50");
    public PositionOffsetBuilder Bottom100 => Chain("bottom", "100");

    public PositionOffsetBuilder Start0 => Chain("start", "0");
    public PositionOffsetBuilder Start50 => Chain("start", "50");
    public PositionOffsetBuilder Start100 => Chain("start", "100");

    public PositionOffsetBuilder End0 => Chain("end", "0");
    public PositionOffsetBuilder End50 => Chain("end", "50");
    public PositionOffsetBuilder End100 => Chain("end", "100");

    public PositionOffsetBuilder TranslateMiddle => Chain("translate", "middle");
    public PositionOffsetBuilder TranslateMiddleX => Chain("translate", "middle-x");
    public PositionOffsetBuilder TranslateMiddleY => Chain("translate", "middle-y");

    public PositionOffsetBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public PositionOffsetBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public PositionOffsetBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public PositionOffsetBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public PositionOffsetBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
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

        int lastIdx = _rules.Count - 1;
        PositionOffsetRule last = _rules[lastIdx];
        _rules[lastIdx] = new PositionOffsetRule(last.Property, last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            PositionOffsetRule rule = _rules[i];
            string cls = GetClass(rule.Property, rule.Value);
            if (cls.Length == 0)
                continue;

            string bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                cls = InsertBreakpointType(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            PositionOffsetRule rule = _rules[i];
            string? css = GetStyle(rule.Property, rule.Value);
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

        string prefix = property switch
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
        string cssProp = property switch
        {
            "top" => "top",
            "bottom" => "bottom",
            "start" => "inset-inline-start",
            "end" => "inset-inline-end",
            _ => string.Empty
        };
        if (cssProp.Length == 0) return null;
        string cssVal = value switch
        {
            "0" => "0",
            "50" => "50%",
            "100" => "100%",
            _ => string.Empty
        };
        if (cssVal.Length == 0) return null;
        return $"{cssProp}: {cssVal}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string InsertBreakpointType(string className, string bp)
    {
        int dashIndex = className.IndexOf('-');
        if (dashIndex > 0)
        {
            int len = dashIndex + 1 + bp.Length + (className.Length - dashIndex);
            return string.Create(len, (className, dashIndex, bp), static (dst, s) =>
            {
                s.className.AsSpan(0, s.dashIndex).CopyTo(dst);
                int idx = s.dashIndex;
                dst[idx++] = '-';
                s.bp.AsSpan().CopyTo(dst[idx..]);
                idx += s.bp.Length;
                s.className.AsSpan(s.dashIndex).CopyTo(dst[idx..]);
            });
        }

        return string.Create(bp.Length + 1 + className.Length, (className, bp), static (dst, s) =>
        {
            s.bp.AsSpan().CopyTo(dst);
            int idx = s.bp.Length;
            dst[idx++] = '-';
            s.className.AsSpan().CopyTo(dst[idx..]);
        });
    }
}

