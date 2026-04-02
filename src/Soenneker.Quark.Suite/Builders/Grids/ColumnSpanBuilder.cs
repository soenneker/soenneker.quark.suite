using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("col-span", Responsive = true)]
public sealed class ColumnSpanBuilder : ICssBuilder
{
    private readonly List<GridRule> _rules = new(8);
    private BreakpointType? _pendingBreakpoint;

    internal ColumnSpanBuilder(string value, BreakpointType? breakpoint = null)
    {
        if (value.Length > 0)
            _rules.Add(new GridRule("col-span", value, breakpoint));
    }

    internal ColumnSpanBuilder(List<GridRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ColumnSpanBuilder Is1 => Chain("col-span", "1");
    public ColumnSpanBuilder Is2 => Chain("col-span", "2");
    public ColumnSpanBuilder Is3 => Chain("col-span", "3");
    public ColumnSpanBuilder Is4 => Chain("col-span", "4");
    public ColumnSpanBuilder Is5 => Chain("col-span", "5");
    public ColumnSpanBuilder Is6 => Chain("col-span", "6");
    public ColumnSpanBuilder Is7 => Chain("col-span", "7");
    public ColumnSpanBuilder Is8 => Chain("col-span", "8");
    public ColumnSpanBuilder Is9 => Chain("col-span", "9");
    public ColumnSpanBuilder Is10 => Chain("col-span", "10");
    public ColumnSpanBuilder Is11 => Chain("col-span", "11");
    public ColumnSpanBuilder Is12 => Chain("col-span", "12");
    public ColumnSpanBuilder Full => Chain("col-span", "full");

    public ColumnSpanBuilder Span(int value) => Chain("col-span", value.ToString());

    public ColumnSpanBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public ColumnSpanBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public ColumnSpanBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public ColumnSpanBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public ColumnSpanBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public ColumnSpanBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnSpanBuilder Chain(string utility, string value)
    {
        _rules.Add(new GridRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnSpanBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
