using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Outline style builder. Tailwind: outline-none, outline, outline-dashed, outline-dotted, outline-double.
/// </summary>
[TailwindPrefix("outline-", Responsive = true)]
public sealed class OutlineStyleBuilder : ICssBuilder
{
    private readonly List<OutlineStyleRule> _rules = new(4);

    internal OutlineStyleBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new OutlineStyleRule(value, breakpoint));
    }

    internal OutlineStyleBuilder(List<OutlineStyleRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public OutlineStyleBuilder None => Chain("none");
    public OutlineStyleBuilder Solid => Chain("solid");
    public OutlineStyleBuilder Dashed => Chain("dashed");
    public OutlineStyleBuilder Dotted => Chain("dotted");
    public OutlineStyleBuilder Double => Chain("double");

    public OutlineStyleBuilder OnSm => ChainBp(BreakpointType.Sm);
    public OutlineStyleBuilder OnMd => ChainBp(BreakpointType.Md);
    public OutlineStyleBuilder OnLg => ChainBp(BreakpointType.Lg);
    public OutlineStyleBuilder OnXl => ChainBp(BreakpointType.Xl);
    public OutlineStyleBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OutlineStyleBuilder Chain(string value)
    {
        _rules.Add(new OutlineStyleRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private OutlineStyleBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new OutlineStyleRule("none", bp));
        else _rules[^1] = new OutlineStyleRule(_rules[^1].Value, bp);
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
                "none" => "outline-none",
                "solid" => "outline",
                "dashed" => "outline-dashed",
                "dotted" => "outline-dotted",
                "double" => "outline-double",
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
            if (rule.Value is not ("none" or "solid" or "dashed" or "dotted" or "double")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("outline-style: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
