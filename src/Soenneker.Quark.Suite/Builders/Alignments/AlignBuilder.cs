using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("content-", Responsive = true)]
public sealed class AlignBuilder : ICssBuilder
{
    private readonly List<AlignRule> _rules = new(8);
    private BreakpointType? _pendingBreakpoint;

    internal AlignBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new AlignRule(utility, value, breakpoint));
    }

    /// <summary>
    /// `justify-start` — packs flex/grid items to the start of the main axis (`justify-content: flex-start`).
    /// </summary>
    public AlignBuilder JustifyStart => Chain("justify-start", "");
    /// <summary>
    /// `justify-end` — packs items to the end of the main axis (`justify-content: flex-end`).
    /// </summary>
    public AlignBuilder JustifyEnd => Chain("justify-end", "");
    /// <summary>
    /// `justify-center` — centers items on the main axis (`justify-content: center`).
    /// </summary>
    public AlignBuilder JustifyCenter => Chain("justify-center", "");
    /// <summary>
    /// `justify-between` — space between items (`justify-content: space-between`).
    /// </summary>
    public AlignBuilder JustifyBetween => Chain("justify-between", "");
    /// <summary>
    /// `justify-around` — space around each item (`justify-content: space-around`).
    /// </summary>
    public AlignBuilder JustifyAround => Chain("justify-around", "");
    /// <summary>
    /// `justify-evenly` — evenly distributed space (`justify-content: space-evenly`).
    /// </summary>
    public AlignBuilder JustifyEvenly => Chain("justify-evenly", "");

    /// <summary>
    /// `items-start` — align items to the start of the cross axis (`align-items: flex-start`).
    /// </summary>
    public AlignBuilder ItemsStart => Chain("items-start", "");
    /// <summary>
    /// `items-end` — align to the end of the cross axis (`align-items: flex-end`).
    /// </summary>
    public AlignBuilder ItemsEnd => Chain("items-end", "");
    /// <summary>
    /// `items-center` — center on the cross axis (`align-items: center`).
    /// </summary>
    public AlignBuilder ItemsCenter => Chain("items-center", "");
    /// <summary>
    /// `items-baseline` — align baselines (`align-items: baseline`).
    /// </summary>
    public AlignBuilder ItemsBaseline => Chain("items-baseline", "");
    /// <summary>
    /// `items-stretch` — stretch to fill the cross axis (`align-items: stretch`).
    /// </summary>
    public AlignBuilder ItemsStretch => Chain("items-stretch", "");

    /// <summary>
    /// `content-start` — packs rows/columns to the start (`align-content: flex-start`).
    /// </summary>
    public AlignBuilder ContentStart => Chain("content-start", "");
    /// <summary>
    /// `content-end` — packs to the end (`align-content: flex-end`).
    /// </summary>
    public AlignBuilder ContentEnd => Chain("content-end", "");
    /// <summary>
    /// `content-center` — centers wrapped lines (`align-content: center`).
    /// </summary>
    public AlignBuilder ContentCenter => Chain("content-center", "");
    /// <summary>
    /// `content-between` — space between rows (`align-content: space-between`).
    /// </summary>
    public AlignBuilder ContentBetween => Chain("content-between", "");
    /// <summary>
    /// `content-around` — space around rows (`align-content: space-around`).
    /// </summary>
    public AlignBuilder ContentAround => Chain("content-around", "");
    /// <summary>
    /// `content-evenly` — evenly spaced rows (`align-content: space-evenly`).
    /// </summary>
    public AlignBuilder ContentEvenly => Chain("content-evenly", "");
    /// <summary>
    /// `content-stretch` — stretch rows (`align-content: stretch`).
    /// </summary>
    public AlignBuilder ContentStretch => Chain("content-stretch", "");

    /// <summary>
    /// `self-auto` — default alignment for this item (`align-self: auto`).
    /// </summary>
    public AlignBuilder SelfAuto => Chain("self-auto", "");
    /// <summary>
    /// `self-start` — align this item to the cross-start edge (`align-self: flex-start`).
    /// </summary>
    public AlignBuilder SelfStart => Chain("self-start", "");
    /// <summary>
    /// `self-end` — align to the cross-end edge (`align-self: flex-end`).
    /// </summary>
    public AlignBuilder SelfEnd => Chain("self-end", "");
    /// <summary>
    /// `self-center` — center this item on the cross axis (`align-self: center`).
    /// </summary>
    public AlignBuilder SelfCenter => Chain("self-center", "");
    /// <summary>
    /// `self-stretch` — stretch this item (`align-self: stretch`).
    /// </summary>
    public AlignBuilder SelfStretch => Chain("self-stretch", "");
    /// <summary>
    /// `self-baseline` — baseline alignment (`align-self: baseline`).
    /// </summary>
    public AlignBuilder SelfBaseline => Chain("self-baseline", "");

    /// <summary>
    /// `justify-items-start` — grid items align to column start.
    /// </summary>
    public AlignBuilder JustifyItemsStart => Chain("justify-items-start", "");
    /// <summary>
    /// `justify-items-end` — grid items align to column end.
    /// </summary>
    public AlignBuilder JustifyItemsEnd => Chain("justify-items-end", "");
    /// <summary>
    /// `justify-items-center` — grid items centered in their cell.
    /// </summary>
    public AlignBuilder JustifyItemsCenter => Chain("justify-items-center", "");
    /// <summary>
    /// `justify-items-stretch` — items stretch to fill the cell.
    /// </summary>
    public AlignBuilder JustifyItemsStretch => Chain("justify-items-stretch", "");

    /// <summary>
    /// `justify-self-auto` — default grid self-alignment.
    /// </summary>
    public AlignBuilder JustifySelfAuto => Chain("justify-self-auto", "");
    /// <summary>
    /// `justify-self-start` — align this grid item to the start of its area.
    /// </summary>
    public AlignBuilder JustifySelfStart => Chain("justify-self-start", "");
    /// <summary>
    /// `justify-self-end` — align to the end of its area.
    /// </summary>
    public AlignBuilder JustifySelfEnd => Chain("justify-self-end", "");
    /// <summary>
    /// `justify-self-center` — center within its area.
    /// </summary>
    public AlignBuilder JustifySelfCenter => Chain("justify-self-center", "");
    /// <summary>
    /// `justify-self-stretch` — stretch across the area.
    /// </summary>
    public AlignBuilder JustifySelfStretch => Chain("justify-self-stretch", "");

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public AlignBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public AlignBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public AlignBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public AlignBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public AlignBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public AlignBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AlignBuilder Chain(string utility, string value)
    {
        _rules.Add(new AlignRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AlignBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
