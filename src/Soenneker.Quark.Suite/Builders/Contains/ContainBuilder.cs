using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// CSS contain builder. Tailwind: contain-none, contain-size, contain-layout, contain-style, contain-paint, contain-strict, contain-content.
/// </summary>
[TailwindPrefix("contain-", Responsive = true)]
public sealed class ContainBuilder : ICssBuilder
{
    private readonly List<ContainRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal ContainBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new ContainRule(value, breakpoint));
    }

    internal ContainBuilder(List<ContainRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public ContainBuilder None => Chain("none");
    /// <summary>
    /// Fluent step for `Size` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ContainBuilder Size => Chain("size");
    /// <summary>
    /// Fluent step for `Layout` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ContainBuilder Layout => Chain("layout");
    /// <summary>
    /// Fluent step for `Style` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ContainBuilder Style => Chain("style");
    /// <summary>
    /// Fluent step for `Paint` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ContainBuilder Paint => Chain("paint");
    /// <summary>
    /// Fluent step for `Strict` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ContainBuilder Strict => Chain("strict");
    /// <summary>
    /// Fluent step for `Content` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public ContainBuilder Content => Chain("content");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public ContainBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public ContainBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public ContainBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public ContainBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public ContainBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ContainBuilder Chain(string value)
    {
        _rules.Add(new ContainRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ContainBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
                "none" => "contain-none",
                "size" => "contain-size",
                "layout" => "contain-layout",
                "style" => "contain-style",
                "paint" => "contain-paint",
                "strict" => "contain-strict",
                "content" => "contain-content",
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
            if (!IsValid(rule.Value)) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("contain: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static bool IsValid(string v) =>
        v is "none" or "size" or "layout" or "style" or "paint" or "strict" or "content";
}
