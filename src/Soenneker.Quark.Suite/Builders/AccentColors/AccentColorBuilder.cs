using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Accent color builder for form controls. Tailwind: accent-auto, accent-primary, accent-*.
/// </summary>
[TailwindPrefix("accent-", Responsive = true)]
public sealed class AccentColorBuilder : ICssBuilder
{
    private static readonly HashSet<string> SemanticTokens = new(System.StringComparer.Ordinal)
    {
        "primary",
        "current",
        "transparent"
    };

    private const string Prefix = "accent-";

    private readonly List<ColorRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal AccentColorBuilder(string value, BreakpointType? breakpoint = null, bool isUtility = false)
    {
        _rules.Add(new ColorRule(value, breakpoint, isUtility));
    }

    internal AccentColorBuilder(List<ColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// `auto` — browser-default sizing/behavior for the underlying utility.
    /// </summary>
    public AccentColorBuilder Auto => Chain("auto");
    /// <summary>
    /// `accent-primary` — uses your theme primary (shadcn maps this to CSS variables).
    /// </summary>
    public AccentColorBuilder Primary => Chain("primary");
    /// <summary>
    /// Fully transparent color (`transparent`).
    /// </summary>
    public AccentColorBuilder Transparent => Chain("transparent");
    /// <summary>
    /// `currentColor` — uses the element’s computed `color` (common for icons and rings).
    /// </summary>
    public AccentColorBuilder Current => Chain("current");
    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint.
    /// </summary>
    public AccentColorBuilder OnBase => ChainBp(BreakpointType.Base);

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public AccentColorBuilder OnSm => ChainBp(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public AccentColorBuilder OnMd => ChainBp(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public AccentColorBuilder OnLg => ChainBp(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public AccentColorBuilder OnXl => ChainBp(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public AccentColorBuilder On2xl => ChainBp(BreakpointType.Xxl);

    public AccentColorBuilder Token(string token) => Chain(token);

    public AccentColorBuilder Utility(string utility) => Chain(utility, isUtility: true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AccentColorBuilder Chain(string value, bool isUtility = false)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new ColorRule(value, breakpoint, isUtility));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AccentColorBuilder ChainBp(BreakpointType bp)
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
            var cls = GetClass(rule);
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
        => string.Empty;

    public override string ToString() => ToClass();

    private static string GetClass(ColorRule rule)
    {
        return rule.Value == "auto"
            ? "accent-auto"
            : ColorUtility.GetClass(Prefix, rule, SemanticTokens);
    }
}
