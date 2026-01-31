using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified truncate builder with fluent API for chaining truncate rules.
/// </summary>
public sealed class TruncateBuilder : ICssBuilder
{
    private readonly List<TruncateRule> _rules = new(4);

    private const string _classTextTruncate = "text-truncate";

    internal TruncateBuilder(BreakpointType? breakpoint = null)
    {
        _rules.Add(new TruncateRule(breakpoint));
    }

    internal TruncateBuilder(List<TruncateRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Applies the truncate on phone breakpoint.
    /// </summary>
    public TruncateBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    /// <summary>
    /// Applies the truncate on tablet breakpoint.
    /// </summary>
    public TruncateBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    /// <summary>
    /// Applies the truncate on laptop breakpoint.
    /// </summary>
    public TruncateBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    /// <summary>
    /// Applies the truncate on desktop breakpoint.
    /// </summary>
    public TruncateBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    /// <summary>
    /// Applies the truncate on widescreen breakpoint.
    /// </summary>
    public TruncateBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    /// <summary>
    /// Applies the truncate on ultrawide breakpoint.
    /// </summary>
    public TruncateBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TruncateBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new TruncateRule(breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new TruncateRule(breakpoint);
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
            var cls = _classTextTruncate;

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

            sb.Append("overflow: hidden; text-overflow: ellipsis; white-space: nowrap;");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns the CSS class string representation of this truncate builder.
    /// </summary>
    /// <returns>The CSS class string.</returns>
    public override string ToString()
    {
        return ToClass();
    }
}

