using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Scroll snap stop builder. Tailwind: snap-stop-normal, snap-stop-always.
/// </summary>
[TailwindPrefix("snap-stop-", Responsive = true)]
public sealed class ScrollSnapStopBuilder : ICssBuilder
{
    private readonly List<ScrollSnapStopRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal ScrollSnapStopBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ScrollSnapStopRule(value, breakpoint));
    }

    internal ScrollSnapStopBuilder(List<ScrollSnapStopRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `Normal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapStopBuilder Normal => Chain("normal");
    /// <summary>
    /// Fluent step for `Always` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ScrollSnapStopBuilder Always => Chain("always");

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint.
    /// </summary>
    public ScrollSnapStopBuilder OnBase => ChainBp(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public ScrollSnapStopBuilder OnSm => ChainBp(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public ScrollSnapStopBuilder OnMd => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public ScrollSnapStopBuilder OnLg => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public ScrollSnapStopBuilder OnXl => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public ScrollSnapStopBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapStopBuilder Chain(string value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new ScrollSnapStopRule(value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ScrollSnapStopBuilder ChainBp(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = rule.Value switch { "normal" => "snap-stop-normal", "always" => "snap-stop-always", _ => string.Empty };
            if (cls.Length == 0) continue;
            var b = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (b.Length != 0) cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, b);
            if (!first) sb.Append(' ');
            else first = false;
            sb.Append(cls);
        }
        return sb.ToString();
    }

    public string ToStyle() => string.Empty;

    public override string ToString() => ToClass();
}
