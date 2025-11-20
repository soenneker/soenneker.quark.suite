using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Simplified stretched link builder with fluent API for chaining stretched link rules.
/// </summary>
public sealed class StretchedLinkBuilder : ICssBuilder
{
    private readonly List<StretchedLinkRule> _rules = new(4);

    private const string _classStretchedLink = "stretched-link";

    internal StretchedLinkBuilder(BreakpointType? breakpoint = null)
    {
        _rules.Add(new StretchedLinkRule(breakpoint));
    }

    internal StretchedLinkBuilder(List<StretchedLinkRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public StretchedLinkBuilder OnPhone => ChainWithBreakpoint(BreakpointType.Phone);
    public StretchedLinkBuilder OnTablet => ChainWithBreakpoint(BreakpointType.Tablet);
    public StretchedLinkBuilder OnLaptop => ChainWithBreakpoint(BreakpointType.Laptop);
    public StretchedLinkBuilder OnDesktop => ChainWithBreakpoint(BreakpointType.Desktop);
    public StretchedLinkBuilder OnWidescreen => ChainWithBreakpoint(BreakpointType.Widescreen);
    public StretchedLinkBuilder OnUltrawide => ChainWithBreakpoint(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StretchedLinkBuilder ChainWithBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new StretchedLinkRule(breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new StretchedLinkRule(breakpoint);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = _classStretchedLink;

            var bp = BreakpointUtil.GetBreakpointClass(rule.breakpoint);
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
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;

        for (var i = 0; i < _rules.Count; i++)
        {
            if (!first) sb.Append("; ");
            else first = false;

            sb.Append("position: absolute; top: 0; right: 0; bottom: 0; left: 0; z-index: 1; pointer-events: auto; content: \"\";");
        }

        return sb.ToString();
    }


    public override string ToString()
    {
        return ToClass();
    }
}

