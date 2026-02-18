using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class DivideBuilder : ICssBuilder
{
    private readonly List<DivideRule> _rules = new(8);

    internal DivideBuilder(string utility, string value = "", BreakpointType? breakpoint = null)
    {
        _rules.Add(new DivideRule(utility, value, breakpoint));
    }

    public DivideBuilder X => Chain("divide-x", "");
    public DivideBuilder Y => Chain("divide-y", "");
    public DivideBuilder XReverse => Chain("divide-x-reverse", "");
    public DivideBuilder YReverse => Chain("divide-y-reverse", "");
    public DivideBuilder Color(string value) => Chain("divide", value);
    public DivideBuilder Opacity(int value) => Chain("divide-opacity", value.ToString());
    public DivideBuilder Solid => Chain("divide-solid", "");
    public DivideBuilder Dashed => Chain("divide-dashed", "");
    public DivideBuilder Dotted => Chain("divide-dotted", "");
    public DivideBuilder Double => Chain("divide-double", "");
    public DivideBuilder None => Chain("divide-none", "");

    public DivideBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public DivideBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public DivideBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public DivideBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public DivideBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public DivideBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DivideBuilder Chain(string utility, string value)
    {
        _rules.Add(new DivideRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DivideBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new DivideRule("divide-x", "", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        DivideRule last = _rules[lastIdx];
        _rules[lastIdx] = new DivideRule(last.Utility, last.Value, breakpoint);
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
            DivideRule rule = _rules[i];
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
