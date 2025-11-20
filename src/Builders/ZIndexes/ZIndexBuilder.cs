using Soenneker.Quark.Enums;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class ZIndexBuilder : ICssBuilder
{
    private readonly List<ZIndexRule> _rules = new(4);

    private const string _classNeg1 = "z-n1";
    private const string _class0 = "z-0";
    private const string _class1 = "z-1";
    private const string _class2 = "z-2";
    private const string _class3 = "z-3";

    internal ZIndexBuilder(int value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ZIndexRule(value, breakpoint));
    }

    internal ZIndexBuilder(List<ZIndexRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ZIndexBuilder N1 => Chain(-1);
    public ZIndexBuilder Z0 => Chain(0);
    public ZIndexBuilder Z1 => Chain(1);
    public ZIndexBuilder Z2 => Chain(2);
    public ZIndexBuilder Z3 => Chain(3);

    public ZIndexBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public ZIndexBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public ZIndexBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public ZIndexBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public ZIndexBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public ZIndexBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ZIndexBuilder Chain(int value)
    {
        _rules.Add(new ZIndexRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ZIndexBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ZIndexRule(0, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new ZIndexRule(last.Value, bp);
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
                -1 => _classNeg1,
                0 => _class0,
                1 => _class1,
                2 => _class2,
                3 => _class3,
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
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("z-index: ");
            sb.Append(rule.Value.ToString());
        }
        return sb.ToString();
    }


}

 

