using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified clearfix builder with fluent API for chaining clearfix rules.
/// </summary>
public sealed class ClearfixBuilder : ICssBuilder
{
    private readonly List<ClearfixRule> _rules = new(4);

    private const string _classClearfix = "clearfix";

    internal ClearfixBuilder(BreakpointType? breakpoint = null)
    {
        _rules.Add(new ClearfixRule(breakpoint));
    }

    internal ClearfixBuilder(List<ClearfixRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Applies the clearfix on phone breakpoint.
    /// </summary>
    public ClearfixBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the clearfix on tablet breakpoint.
    /// </summary>
    public ClearfixBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies the clearfix on laptop breakpoint.
    /// </summary>
    public ClearfixBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies the clearfix on desktop breakpoint.
    /// </summary>
    public ClearfixBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies the clearfix on widescreen breakpoint.
    /// </summary>
    public ClearfixBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Xxl);
    /// <summary>
    /// Applies the clearfix on ultrawide breakpoint.
    /// </summary>
    public ClearfixBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ClearfixBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new ClearfixRule(breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        ClearfixRule last = _rules[lastIdx];
        _rules[lastIdx] = new ClearfixRule(breakpoint);
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
            ClearfixRule rule = _rules[i];
            string cls = _classClearfix;

            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("&::after { display: block; clear: both; content: \"\"; }");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns the CSS class string representation of this clearfix builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

