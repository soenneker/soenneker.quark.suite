using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class FillBuilder : ICssBuilder
{
    private readonly List<FillRule> _rules = new(4);

    internal FillBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FillRule(utility, value, breakpoint));
    }

    public FillBuilder Token(string value) => Chain("fill", value);
    public FillBuilder None => Chain("fill", "none");
    public FillBuilder Current => Chain("fill", "current");

    public FillBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public FillBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public FillBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public FillBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public FillBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public FillBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillBuilder Chain(string utility, string value)
    {
        _rules.Add(new FillRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new FillRule("fill", "current", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new FillRule(last.Utility, last.Value, breakpoint);
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
