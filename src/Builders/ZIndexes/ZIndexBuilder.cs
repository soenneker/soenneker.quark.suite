using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified z-index builder with fluent API for chaining z-index rules.
/// </summary>
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

    /// <summary>
    /// Sets the z-index to -1.
    /// </summary>
    public ZIndexBuilder N1 => Chain(-1);

    /// <summary>
    /// Sets the z-index to 0.
    /// </summary>
    public ZIndexBuilder Z0 => Chain(0);

    /// <summary>
    /// Sets the z-index to 1.
    /// </summary>
    public ZIndexBuilder Z1 => Chain(1);

    /// <summary>
    /// Sets the z-index to 2.
    /// </summary>
    public ZIndexBuilder Z2 => Chain(2);

    /// <summary>
    /// Sets the z-index to 3.
    /// </summary>
    public ZIndexBuilder Z3 => Chain(3);

    /// <summary>
    /// Applies the z-index on phone breakpoint.
    /// </summary>
    public ZIndexBuilder OnPhone => ChainBp(BreakpointType.Base);

    /// <summary>
    /// Applies the z-index on tablet breakpoint.
    /// </summary>
    public ZIndexBuilder OnTablet => ChainBp(BreakpointType.Md);

    /// <summary>
    /// Applies the z-index on laptop breakpoint.
    /// </summary>
    public ZIndexBuilder OnLaptop => ChainBp(BreakpointType.Lg);

    /// <summary>
    /// Applies the z-index on desktop breakpoint.
    /// </summary>
    public ZIndexBuilder OnDesktop => ChainBp(BreakpointType.Xl);

    /// <summary>
    /// Applies the z-index on widescreen breakpoint.
    /// </summary>
    public ZIndexBuilder OnWidescreen => ChainBp(BreakpointType.Xxl);

    /// <summary>
    /// Applies the z-index on ultrawide breakpoint.
    /// </summary>
    public ZIndexBuilder OnUltrawide => ChainBp(BreakpointType.Xxl);

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

        int lastIdx = _rules.Count - 1;
        ZIndexRule last = _rules[lastIdx];
        _rules[lastIdx] = new ZIndexRule(last.Value, bp);
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
            ZIndexRule rule = _rules[i];
            string cls = rule.Value switch
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

            string bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
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
            ZIndexRule rule = _rules[i];
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("z-index: ");
            sb.Append(rule.Value.ToString());
        }
        return sb.ToString();
    }


}

 

