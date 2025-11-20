using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class LineHeightBuilder : ICssBuilder
{
    private readonly List<LineHeightRule> _rules = new(4);

    private const string _classLh1 = "lh-1";
    private const string _classLhSm = "lh-sm";
    private const string _classLhBase = "lh-base";
    private const string _classLhLg = "lh-lg";

    internal LineHeightBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new LineHeightRule(value, breakpoint));
    }

    internal LineHeightBuilder(List<LineHeightRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public LineHeightBuilder Is1 => Chain(ScaleType.Is1.Value);
    public LineHeightBuilder Small => Chain(SizeType.Small.Value);
    public LineHeightBuilder Base => Chain("base");
    public LineHeightBuilder Large => Chain(SizeType.Large.Value);

    public LineHeightBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public LineHeightBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public LineHeightBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public LineHeightBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public LineHeightBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public LineHeightBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LineHeightBuilder Chain(string value)
    {
        _rules.Add(new LineHeightRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LineHeightBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new LineHeightRule("base", bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new LineHeightRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Value switch
            {
                ScaleType.Is1Value => _classLh1,
                "sm" => _classLhSm,
                "base" => _classLhBase,
                "lg" => _classLhLg,
                _ => string.Empty
            };
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
            var rule = _rules[i];
            var css = rule.Value switch
            {
                ScaleType.Is1Value => "1",
                "sm" => "1.25",
                "base" => "1.5",
                "lg" => "2",
                _ => rule.Value
            };

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("line-height: ");
            sb.Append(css);
        }
        return sb.ToString();
    }


}

 
