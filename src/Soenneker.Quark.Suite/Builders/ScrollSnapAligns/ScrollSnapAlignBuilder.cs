using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll snap align builder. Tailwind: snap-start, snap-center, snap-end, snap-align-none.
/// </summary>
[TailwindPrefix("snap-", Responsive = true)]
public sealed class ScrollSnapAlignBuilder : ICssBuilder
{
    private readonly List<ScrollSnapAlignRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal ScrollSnapAlignBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollSnapAlignRule(value, breakpoint));
    }

    internal ScrollSnapAlignBuilder(List<ScrollSnapAlignRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `Start` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapAlignBuilder Start => Chain("start");
    /// <summary>
    /// Fluent step for `Center` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapAlignBuilder Center => Chain("center");
    /// <summary>
    /// Fluent step for `End` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapAlignBuilder End => Chain("end");
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public ScrollSnapAlignBuilder None => Chain("none");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public ScrollSnapAlignBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public ScrollSnapAlignBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public ScrollSnapAlignBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public ScrollSnapAlignBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public ScrollSnapAlignBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapAlignBuilder Chain(string value)
    {
        _rules.Add(new ScrollSnapAlignRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapAlignBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
                "start" => "snap-start",
                "center" => "snap-center",
                "end" => "snap-end",
                "none" => "snap-align-none",
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
            if (rule.Value is not ("start" or "center" or "end" or "none")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("scroll-snap-align: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
