using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class SpaceBuilder : ICssBuilder
{
    private readonly List<SpaceRule> _rules = new(6);

    internal SpaceBuilder(string utility, string value = "", BreakpointType? breakpoint = null)
    {
        _rules.Add(new SpaceRule(utility, value, breakpoint));
    }

    public SpaceBuilder X(int value) => Chain("space-x", value.ToString());
    public SpaceBuilder Y(int value) => Chain("space-y", value.ToString());
    public SpaceBuilder XReverse => Chain("space-x-reverse", "");
    public SpaceBuilder YReverse => Chain("space-y-reverse", "");

    public SpaceBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public SpaceBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public SpaceBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public SpaceBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public SpaceBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public SpaceBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SpaceBuilder Chain(string utility, string value)
    {
        _rules.Add(new SpaceRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SpaceBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new SpaceRule("space-x", "0", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        SpaceRule last = _rules[lastIdx];
        _rules[lastIdx] = new SpaceRule(last.Utility, last.Value, breakpoint);
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
            SpaceRule rule = _rules[i];
            string cls = rule.Value.Length == 0 ? rule.Utility : $"{rule.Utility}-{rule.Value}";
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
