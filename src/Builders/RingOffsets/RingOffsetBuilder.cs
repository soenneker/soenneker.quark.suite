using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class RingOffsetBuilder : ICssBuilder
{
    private readonly List<RingOffsetRule> _rules = new(4);

    internal RingOffsetBuilder(string utility, string value = "", BreakpointType? breakpoint = null)
    {
        _rules.Add(new RingOffsetRule(utility, value, breakpoint));
    }

    public RingOffsetBuilder Width(int value) => Chain("ring-offset", value.ToString());
    public RingOffsetBuilder Color(string value) => Chain("ring-offset", value);

    public RingOffsetBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public RingOffsetBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public RingOffsetBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public RingOffsetBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public RingOffsetBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public RingOffsetBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingOffsetBuilder Chain(string utility, string value)
    {
        _rules.Add(new RingOffsetRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingOffsetBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new RingOffsetRule("ring-offset", "0", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        RingOffsetRule last = _rules[lastIdx];
        _rules[lastIdx] = new RingOffsetRule(last.Utility, last.Value, breakpoint);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        bool first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            RingOffsetRule rule = _rules[i];
            string cls = $"{rule.Utility}-{rule.Value}";
            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle() => string.Empty;
}
