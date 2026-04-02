using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("grid-rows", Responsive = true)]
public sealed class RowBuilder : ICssBuilder
{
    private readonly List<GridRule> _rules = new(8);
    private BreakpointType? _pendingBreakpoint;

    internal RowBuilder(string utility, string value = "", BreakpointType? breakpoint = null)
    {
        _rules.Add(new GridRule(utility, value, breakpoint));
    }

    internal RowBuilder(List<GridRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public RowBuilder Is1 => Chain("grid-rows", "1");
    public RowBuilder Is2 => Chain("grid-rows", "2");
    public RowBuilder Is3 => Chain("grid-rows", "3");
    public RowBuilder Is4 => Chain("grid-rows", "4");
    public RowBuilder Is5 => Chain("grid-rows", "5");
    public RowBuilder Is6 => Chain("grid-rows", "6");
    public RowBuilder None => Chain("grid-rows", "none");
    public RowBuilder Subgrid => Chain("grid-rows", "subgrid");

    public RowBuilder Rows(int value) => Chain("grid-rows", value.ToString());
    public RowBuilder RowSpan(int value) => Chain("row-span", value.ToString());
    public RowBuilder RowSpanFull => Chain("row-span", "full");
    public RowBuilder RowStart(int value) => Chain("row-start", value.ToString());
    public RowBuilder RowEnd(int value) => Chain("row-end", value.ToString());
    public RowBuilder RowStartAuto => Chain("row-start", "auto");
    public RowBuilder RowEndAuto => Chain("row-end", "auto");
    public RowBuilder AutoRows(string value) => Chain("auto-rows", value);

    public RowBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public RowBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public RowBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public RowBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public RowBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public RowBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RowBuilder Chain(string utility, string value)
    {
        _rules.Add(new GridRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RowBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
