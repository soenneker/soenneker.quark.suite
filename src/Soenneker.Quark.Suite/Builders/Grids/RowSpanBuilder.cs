using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("row-span", Responsive = true)]
public sealed class RowSpanBuilder : ICssBuilder
{
    private readonly List<GridRule> _rules = new(8);
    private BreakpointType? _pendingBreakpoint;

    internal RowSpanBuilder(string value, BreakpointType? breakpoint = null)
    {
        if (value.Length > 0)
            _rules.Add(new GridRule("row-span", value, breakpoint));
    }

    internal RowSpanBuilder(List<GridRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public RowSpanBuilder Is1 => Chain("row-span", "1");
    public RowSpanBuilder Is2 => Chain("row-span", "2");
    public RowSpanBuilder Is3 => Chain("row-span", "3");
    public RowSpanBuilder Is4 => Chain("row-span", "4");
    public RowSpanBuilder Is5 => Chain("row-span", "5");
    public RowSpanBuilder Is6 => Chain("row-span", "6");
    public RowSpanBuilder Full => Chain("row-span", "full");

    public RowSpanBuilder Span(int value) => Chain("row-span", value.ToString());

    public RowSpanBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public RowSpanBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public RowSpanBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public RowSpanBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public RowSpanBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public RowSpanBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RowSpanBuilder Chain(string utility, string value)
    {
        _rules.Add(new GridRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RowSpanBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
