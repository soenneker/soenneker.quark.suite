using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("grid-cols", Responsive = true)]
public sealed class ColumnBuilder : ICssBuilder
{
    private readonly List<GridRule> _rules = new(8);
    private BreakpointType? _pendingBreakpoint;

    internal ColumnBuilder(string utility, string value = "", BreakpointType? breakpoint = null)
    {
        _rules.Add(new GridRule(utility, value, breakpoint));
    }

    internal ColumnBuilder(List<GridRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public ColumnBuilder Is1 => Chain("grid-cols", "1");
    public ColumnBuilder Is2 => Chain("grid-cols", "2");
    public ColumnBuilder Is3 => Chain("grid-cols", "3");
    public ColumnBuilder Is4 => Chain("grid-cols", "4");
    public ColumnBuilder Is5 => Chain("grid-cols", "5");
    public ColumnBuilder Is6 => Chain("grid-cols", "6");
    public ColumnBuilder Is7 => Chain("grid-cols", "7");
    public ColumnBuilder Is8 => Chain("grid-cols", "8");
    public ColumnBuilder Is9 => Chain("grid-cols", "9");
    public ColumnBuilder Is10 => Chain("grid-cols", "10");
    public ColumnBuilder Is11 => Chain("grid-cols", "11");
    public ColumnBuilder Is12 => Chain("grid-cols", "12");
    public ColumnBuilder None => Chain("grid-cols", "none");
    public ColumnBuilder Subgrid => Chain("grid-cols", "subgrid");

    public ColumnBuilder Cols(int value) => Chain("grid-cols", value.ToString());
    public ColumnBuilder ColSpan(int value) => Chain("col-span", value.ToString());
    public ColumnBuilder ColSpanFull => Chain("col-span", "full");
    public ColumnBuilder ColStart(int value) => Chain("col-start", value.ToString());
    public ColumnBuilder ColEnd(int value) => Chain("col-end", value.ToString());
    public ColumnBuilder ColStartAuto => Chain("col-start", "auto");
    public ColumnBuilder ColEndAuto => Chain("col-end", "auto");
    public ColumnBuilder AutoCols(string value) => Chain("auto-cols", value);

    public ColumnBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public ColumnBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public ColumnBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public ColumnBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public ColumnBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public ColumnBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnBuilder Chain(string utility, string value)
    {
        _rules.Add(new GridRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ColumnBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
