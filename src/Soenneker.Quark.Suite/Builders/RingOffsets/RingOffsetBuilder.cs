using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("ring-offset-", Responsive = true)]
public sealed class RingOffsetBuilder : ICssBuilder
{
    private readonly List<RingOffsetRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal RingOffsetBuilder(string utility, string value = "", BreakpointType? breakpoint = null)
    {
        _rules.Add(new RingOffsetRule(utility, value, breakpoint));
    }

    public RingOffsetBuilder Width(int value) => Chain("ring-offset", value.ToString());
    public RingOffsetBuilder Color(string value) => Chain("ring-offset", value);

    public RingOffsetBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public RingOffsetBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public RingOffsetBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public RingOffsetBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public RingOffsetBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public RingOffsetBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingOffsetBuilder Chain(string utility, string value)
    {
        _rules.Add(new RingOffsetRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RingOffsetBuilder SetPendingBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BreakpointType? ConsumePendingBreakpoint()
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        return breakpoint;
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
            var cls = $"{rule.Utility}-{rule.Value}";
            var bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
