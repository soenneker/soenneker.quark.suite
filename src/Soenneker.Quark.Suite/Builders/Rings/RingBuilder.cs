using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Tailwind ring utility builder.
/// </summary>
[TailwindPrefix("ring-", Responsive = true)]
public sealed class RingBuilder : ICssBuilder
{
    private readonly List<RingRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal RingBuilder(string token, BreakpointType? breakpoint = null)
    {
        _rules.Add(new RingRule(token, breakpoint));
    }

    internal RingBuilder(List<RingRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public RingBuilder Default => Chain("ring");
    public RingBuilder None => Chain("0");
    public RingBuilder One => Chain("1");
    public RingBuilder Two => Chain("2");
    public RingBuilder Four => Chain("4");
    public RingBuilder Eight => Chain("8");
    public RingBuilder Inset => Chain("inset");

    public RingBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public RingBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public RingBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public RingBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public RingBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public RingBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingBuilder Chain(string token)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new RingRule(token, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
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
            var cls = GetClass(rule.Token);
            if (cls.Length == 0)
                continue;

            var breakpoint = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (breakpoint.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, breakpoint);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle() => string.Empty;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetClass(string token)
    {
        return token switch
        {
            "ring" => "ring",
            "0" => "ring-0",
            "1" => "ring-1",
            "2" => "ring-2",
            "4" => "ring-4",
            "8" => "ring-8",
            "inset" => "ring-inset",
            _ => string.Empty
        };
    }
}
