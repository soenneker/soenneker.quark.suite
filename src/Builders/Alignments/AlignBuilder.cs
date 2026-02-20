using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class AlignBuilder : ICssBuilder
{
    private readonly List<AlignRule> _rules = new(8);

    internal AlignBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new AlignRule(utility, value, breakpoint));
    }

    public AlignBuilder JustifyStart => Chain("justify-start", "");
    public AlignBuilder JustifyEnd => Chain("justify-end", "");
    public AlignBuilder JustifyCenter => Chain("justify-center", "");
    public AlignBuilder JustifyBetween => Chain("justify-between", "");
    public AlignBuilder JustifyAround => Chain("justify-around", "");
    public AlignBuilder JustifyEvenly => Chain("justify-evenly", "");

    public AlignBuilder ItemsStart => Chain("items-start", "");
    public AlignBuilder ItemsEnd => Chain("items-end", "");
    public AlignBuilder ItemsCenter => Chain("items-center", "");
    public AlignBuilder ItemsBaseline => Chain("items-baseline", "");
    public AlignBuilder ItemsStretch => Chain("items-stretch", "");

    public AlignBuilder ContentStart => Chain("content-start", "");
    public AlignBuilder ContentEnd => Chain("content-end", "");
    public AlignBuilder ContentCenter => Chain("content-center", "");
    public AlignBuilder ContentBetween => Chain("content-between", "");
    public AlignBuilder ContentAround => Chain("content-around", "");
    public AlignBuilder ContentEvenly => Chain("content-evenly", "");
    public AlignBuilder ContentStretch => Chain("content-stretch", "");

    public AlignBuilder SelfAuto => Chain("self-auto", "");
    public AlignBuilder SelfStart => Chain("self-start", "");
    public AlignBuilder SelfEnd => Chain("self-end", "");
    public AlignBuilder SelfCenter => Chain("self-center", "");
    public AlignBuilder SelfStretch => Chain("self-stretch", "");
    public AlignBuilder SelfBaseline => Chain("self-baseline", "");

    public AlignBuilder JustifyItemsStart => Chain("justify-items-start", "");
    public AlignBuilder JustifyItemsEnd => Chain("justify-items-end", "");
    public AlignBuilder JustifyItemsCenter => Chain("justify-items-center", "");
    public AlignBuilder JustifyItemsStretch => Chain("justify-items-stretch", "");

    public AlignBuilder JustifySelfAuto => Chain("justify-self-auto", "");
    public AlignBuilder JustifySelfStart => Chain("justify-self-start", "");
    public AlignBuilder JustifySelfEnd => Chain("justify-self-end", "");
    public AlignBuilder JustifySelfCenter => Chain("justify-self-center", "");
    public AlignBuilder JustifySelfStretch => Chain("justify-self-stretch", "");

    public AlignBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public AlignBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public AlignBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public AlignBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public AlignBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public AlignBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AlignBuilder Chain(string utility, string value)
    {
        _rules.Add(new AlignRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AlignBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new AlignRule("items-start", "", breakpoint));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new AlignRule(last.Utility, last.Value, breakpoint);
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
