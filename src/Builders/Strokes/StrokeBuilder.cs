using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class StrokeBuilder : ICssBuilder
{
    private readonly List<StrokeRule> _rules = new(4);

    internal StrokeBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new StrokeRule(utility, value, breakpoint));
    }

    public StrokeBuilder Token(string value) => Chain("stroke", value);
    public StrokeBuilder Width(int value) => Chain("stroke", value.ToString());
    public StrokeBuilder None => Chain("stroke", "none");

    public StrokeBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public StrokeBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public StrokeBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public StrokeBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public StrokeBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public StrokeBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeBuilder Chain(string utility, string value)
    {
        _rules.Add(new StrokeRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new StrokeRule("stroke", "1", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new StrokeRule(last.Utility, last.Value, breakpoint);
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
