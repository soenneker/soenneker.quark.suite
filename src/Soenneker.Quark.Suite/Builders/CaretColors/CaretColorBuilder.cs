using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Caret color builder for text inputs. Tailwind: caret-primary, caret-transparent, caret-*.
/// </summary>
[TailwindPrefix("caret-", Responsive = true)]
public sealed class CaretColorBuilder : ICssBuilder
{
    private static readonly HashSet<string> SemanticTokens = new(System.StringComparer.Ordinal)
    {
        "primary",
        "current",
        "transparent"
    };

    private const string Prefix = "caret-";

    private readonly List<ColorRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal CaretColorBuilder(string value, BreakpointType? breakpoint = null, bool isUtility = false)
    {
        _rules.Add(new ColorRule(value, breakpoint, isUtility));
    }

    internal CaretColorBuilder(List<ColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `Primary` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public CaretColorBuilder Primary => Chain("primary");
    /// <summary>
    /// Fully transparent color (`transparent`).
    /// </summary>
    public CaretColorBuilder Transparent => Chain("transparent");
    /// <summary>
    /// `currentColor` — uses the element’s computed `color` (common for icons and rings).
    /// </summary>
    public CaretColorBuilder Current => Chain("current");
    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint.
    /// </summary>
    public CaretColorBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public CaretColorBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public CaretColorBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public CaretColorBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public CaretColorBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public CaretColorBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    public CaretColorBuilder Token(string token) => Chain(token);

    public CaretColorBuilder Utility(string utility) => Chain(utility, isUtility: true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CaretColorBuilder Chain(string value, bool isUtility = false)
    {
        _rules.Add(new ColorRule(value, ConsumePendingBreakpoint(), isUtility));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private CaretColorBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var cls = ColorUtility.GetClass(Prefix, rule, SemanticTokens);
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
