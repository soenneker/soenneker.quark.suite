using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("divide-", Responsive = true)]
public sealed class DivideBuilder : ICssBuilder
{
    private readonly List<DivideRule> _rules = new(8);
    private BreakpointType? _pendingBreakpoint;

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

    public DivideBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public DivideBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public DivideBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public DivideBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public DivideBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public DivideBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DivideBuilder Chain(string utility, string value)
    {
        _rules.Add(new DivideRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DivideBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var cls = rule.Value.Length == 0 ? rule.Utility : $"{rule.Utility}-{rule.Value}";
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
