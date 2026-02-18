using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class GridBuilder : ICssBuilder
{
    private readonly List<GridRule> _rules = new(8);

    internal GridBuilder(string utility, string value = "", BreakpointType? breakpoint = null)
    {
        _rules.Add(new GridRule(utility, value, breakpoint));
    }

    internal GridBuilder(List<GridRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public GridBuilder Grid => Chain("display", "grid");
    public GridBuilder InlineGrid => Chain("display", "inline-grid");

    public GridBuilder Cols(int value) => Chain("grid-cols", value.ToString());
    public GridBuilder Rows(int value) => Chain("grid-rows", value.ToString());

    public GridBuilder ColSpan(int value) => Chain("col-span", value.ToString());
    public GridBuilder RowSpan(int value) => Chain("row-span", value.ToString());
    public GridBuilder ColSpanFull => Chain("col-span", "full");
    public GridBuilder RowSpanFull => Chain("row-span", "full");

    public GridBuilder ColStart(int value) => Chain("col-start", value.ToString());
    public GridBuilder ColEnd(int value) => Chain("col-end", value.ToString());
    public GridBuilder RowStart(int value) => Chain("row-start", value.ToString());
    public GridBuilder RowEnd(int value) => Chain("row-end", value.ToString());
    public GridBuilder ColStartAuto => Chain("col-start", "auto");
    public GridBuilder ColEndAuto => Chain("col-end", "auto");
    public GridBuilder RowStartAuto => Chain("row-start", "auto");
    public GridBuilder RowEndAuto => Chain("row-end", "auto");

    public GridBuilder AutoCols(string value) => Chain("auto-cols", value);
    public GridBuilder AutoRows(string value) => Chain("auto-rows", value);

    public GridBuilder PlaceItems(string value) => Chain("place-items", value);
    public GridBuilder PlaceContent(string value) => Chain("place-content", value);
    public GridBuilder PlaceSelf(string value) => Chain("place-self", value);

    public GridBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public GridBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public GridBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public GridBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public GridBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public GridBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GridBuilder Chain(string utility, string value)
    {
        _rules.Add(new GridRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GridBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new GridRule("display", "grid", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        GridRule last = _rules[lastIdx];
        _rules[lastIdx] = new GridRule(last.Utility, last.Value, breakpoint);
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
            GridRule rule = _rules[i];
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
