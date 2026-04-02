using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// End (inset-inline-end) builder. Tailwind: end-*.
/// </summary>
[TailwindPrefix("end-", Responsive = true)]
public sealed class EndBuilder : ICssBuilder
{
    private readonly List<EndRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal EndBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new EndRule(value, breakpoint));
    }

    internal EndBuilder(List<EndRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public EndBuilder Is0 => Chain(ScaleType.Is0Value);
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public EndBuilder Is1 => Chain(ScaleType.Is1Value);
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public EndBuilder Is2 => Chain(ScaleType.Is2Value);
    /// <summary>
    /// Spacing/sizing scale step `3` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 3` for integer spacing utilities unless overridden).
    /// </summary>
    public EndBuilder Is3 => Chain(ScaleType.Is3Value);
    /// <summary>
    /// Spacing/sizing scale step `4` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 4` for integer spacing utilities unless overridden).
    /// </summary>
    public EndBuilder Is4 => Chain(ScaleType.Is4Value);
    /// <summary>
    /// Spacing/sizing scale step `5` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 5` for integer spacing utilities unless overridden).
    /// </summary>
    public EndBuilder Is5 => Chain(ScaleType.Is5Value);
    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public EndBuilder Auto => Chain("auto");
    /// <summary>
    /// One pixel (`px` unit) — hairline borders, fixed 1px tracks, etc.
    /// </summary>
    public EndBuilder Px => Chain("px");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public EndBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public EndBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public EndBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public EndBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public EndBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private EndBuilder Chain(string value)
    {
        _rules.Add(new EndRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private EndBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var cls = GetClass(rule.Value);
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
            var val = GetStyleValue(rule.Value);
            if (val is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("inset-inline-end: ");
            sb.Append(val);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static string GetClass(string v) => v switch
    {
        ScaleType.Is0Value => "end-0",
        ScaleType.Is1Value => "end-1",
        ScaleType.Is2Value => "end-2",
        ScaleType.Is3Value => "end-3",
        ScaleType.Is4Value => "end-4",
        ScaleType.Is5Value => "end-5",
        "auto" => "end-auto",
        "px" => "end-px",
        _ => string.Empty
    };

    private static string? GetStyleValue(string v) => v switch
    {
        ScaleType.Is0Value => "0",
        ScaleType.Is1Value => "0.25rem",
        ScaleType.Is2Value => "0.5rem",
        ScaleType.Is3Value => "1rem",
        ScaleType.Is4Value => "1.5rem",
        ScaleType.Is5Value => "3rem",
        "auto" => "auto",
        "px" => "1px",
        _ => null
    };
}
