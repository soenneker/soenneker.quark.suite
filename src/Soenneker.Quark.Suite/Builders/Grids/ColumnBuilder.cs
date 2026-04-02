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

    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is1 => Chain("grid-cols", "1");
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is2 => Chain("grid-cols", "2");
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is3 => Chain("grid-cols", "3");
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is4 => Chain("grid-cols", "4");
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is5 => Chain("grid-cols", "5");
    /// <summary>
    /// Spacing/sizing scale step `6` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 6` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is6 => Chain("grid-cols", "6");
    /// <summary>
    /// Spacing/sizing scale step `7` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 7` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is7 => Chain("grid-cols", "7");
    /// <summary>
    /// Spacing/sizing scale step `8` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 8` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is8 => Chain("grid-cols", "8");
    /// <summary>
    /// Spacing/sizing scale step `9` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 9` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is9 => Chain("grid-cols", "9");
    /// <summary>
    /// Spacing/sizing scale step `10` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 10` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is10 => Chain("grid-cols", "10");
    /// <summary>
    /// Spacing/sizing scale step `11` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 11` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is11 => Chain("grid-cols", "11");
    /// <summary>
    /// Spacing/sizing scale step `12` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 12` for integer spacing utilities unless overridden).
    /// </summary>
    public ColumnBuilder Is12 => Chain("grid-cols", "12");
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public ColumnBuilder None => Chain("grid-cols", "none");
    /// <summary>
    /// Fluent step for `Subgrid` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ColumnBuilder Subgrid => Chain("grid-cols", "subgrid");

    public ColumnBuilder Cols(int value) => Chain("grid-cols", value.ToString());
    public ColumnBuilder ColSpan(int value) => Chain("col-span", value.ToString());
    /// <summary>
    /// Fluent step for `Col Span Full` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ColumnBuilder ColSpanFull => Chain("col-span", "full");
    public ColumnBuilder ColStart(int value) => Chain("col-start", value.ToString());
    public ColumnBuilder ColEnd(int value) => Chain("col-end", value.ToString());
    /// <summary>
    /// Fluent step for `Col Start Auto` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ColumnBuilder ColStartAuto => Chain("col-start", "auto");
    /// <summary>
    /// Fluent step for `Col End Auto` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ColumnBuilder ColEndAuto => Chain("col-end", "auto");
    public ColumnBuilder AutoCols(string value) => Chain("auto-cols", value);

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public ColumnBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public ColumnBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public ColumnBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public ColumnBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public ColumnBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
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
