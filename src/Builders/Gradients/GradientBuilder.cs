using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class GradientBuilder : ICssBuilder
{
    private readonly List<GradientRule> _rules = new(6);

    internal GradientBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new GradientRule(utility, value, breakpoint));
    }

    public GradientBuilder To(string value) => Chain("bg-gradient-to", value);
    public GradientBuilder From(string value) => Chain("from", value);
    public GradientBuilder Via(string value) => Chain("via", value);
    public GradientBuilder End(string value) => Chain("to", value);

    public GradientBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public GradientBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public GradientBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public GradientBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public GradientBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public GradientBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GradientBuilder Chain(string utility, string value)
    {
        _rules.Add(new GradientRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GradientBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new GradientRule("bg-gradient-to", "r", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        GradientRule last = _rules[lastIdx];
        _rules[lastIdx] = new GradientRule(last.Utility, last.Value, breakpoint);
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
            GradientRule rule = _rules[i];
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
