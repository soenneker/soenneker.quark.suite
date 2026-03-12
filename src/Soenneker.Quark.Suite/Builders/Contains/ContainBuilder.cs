using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// CSS contain builder. Tailwind: contain-none, contain-size, contain-layout, contain-style, contain-paint, contain-strict, contain-content.
/// </summary>
[TailwindPrefix("contain-", Responsive = true)]
public sealed class ContainBuilder : ICssBuilder
{
    private readonly List<ContainRule> _rules = new(4);

    internal ContainBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ContainRule(value, breakpoint));
    }

    internal ContainBuilder(List<ContainRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ContainBuilder None => Chain("none");
    public ContainBuilder Size => Chain("size");
    public ContainBuilder Layout => Chain("layout");
    public ContainBuilder Style => Chain("style");
    public ContainBuilder Paint => Chain("paint");
    public ContainBuilder Strict => Chain("strict");
    public ContainBuilder Content => Chain("content");

    public ContainBuilder OnSm => ChainBp(BreakpointType.Sm);
    public ContainBuilder OnMd => ChainBp(BreakpointType.Md);
    public ContainBuilder OnLg => ChainBp(BreakpointType.Lg);
    public ContainBuilder OnXl => ChainBp(BreakpointType.Xl);
    public ContainBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ContainBuilder Chain(string value)
    {
        _rules.Add(new ContainRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ContainBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new ContainRule("none", bp));
        else _rules[^1] = new ContainRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch
            {
                "none" => "contain-none",
                "size" => "contain-size",
                "layout" => "contain-layout",
                "style" => "contain-style",
                "paint" => "contain-paint",
                "strict" => "contain-strict",
                "content" => "contain-content",
                _ => string.Empty
            };
            if (cls.Length == 0) continue;
            var b = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (b.Length != 0) cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, b);
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
        foreach (var rule in _rules)
        {
            if (!IsValid(rule.Value)) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("contain: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static bool IsValid(string v) =>
        v is "none" or "size" or "layout" or "style" or "paint" or "strict" or "content";
}
