using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll snap type builder. Tailwind: snap-none, snap-x, snap-y, snap-both, snap-mandatory, snap-proximity.
/// </summary>
[TailwindPrefix("snap-", Responsive = true)]
public sealed class ScrollSnapBuilder : ICssBuilder
{
    private readonly List<ScrollSnapRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal ScrollSnapBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollSnapRule(value, breakpoint));
    }

    internal ScrollSnapBuilder(List<ScrollSnapRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public ScrollSnapBuilder None => Chain("none");
    /// <summary>
    /// Fluent step for `X` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapBuilder X => Chain("x");
    /// <summary>
    /// Fluent step for `Y` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapBuilder Y => Chain("y");
    /// <summary>
    /// Fluent step for `Both` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapBuilder Both => Chain("both");
    /// <summary>
    /// Fluent step for `Mandatory` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapBuilder Mandatory => Chain("mandatory");
    /// <summary>
    /// Fluent step for `Proximity` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapBuilder Proximity => Chain("proximity");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public ScrollSnapBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public ScrollSnapBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public ScrollSnapBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public ScrollSnapBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public ScrollSnapBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapBuilder Chain(string value)
    {
        _rules.Add(new ScrollSnapRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch
            {
                "none" => "snap-none",
                "x" => "snap-x",
                "y" => "snap-y",
                "both" => "snap-both",
                "mandatory" => "snap-mandatory",
                "proximity" => "snap-proximity",
                _ => string.Empty
            };
            if (cls.Length == 0) continue;
            var b = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (b.Length != 0) cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, b);
            if (!first) sb.Append(' ');
            else first = false;
            sb.Append(cls);
        }
        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var styleVal = rule.Value switch
            {
                "none" => "none",
                "x" => "x mandatory",
                "y" => "y mandatory",
                "both" => "both mandatory",
                "mandatory" => "both mandatory",
                "proximity" => "both proximity",
                _ => null
            };
            if (styleVal is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("scroll-snap-type: ");
            sb.Append(styleVal);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
