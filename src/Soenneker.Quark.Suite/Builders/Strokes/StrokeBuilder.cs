using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("stroke-", Responsive = true)]
public sealed class StrokeBuilder : ICssBuilder
{
    private readonly List<StrokeRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal StrokeBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new StrokeRule(utility, value, breakpoint));
    }

    /// <summary>
    /// Tailwind token segment (spacing scale step, arbitrary value like `[17rem]`, or theme key). Builds the matching utility class for this builder.
    /// </summary>
    /// <param name="value">Suffix/token after the utility prefix (see Tailwind docs for this family).</param>
    public StrokeBuilder Token(string value) => Chain("stroke", value);
    public StrokeBuilder Width(int value) => Chain("stroke", value.ToString());
    /// <summary>
    /// Disables the effect (`none` token) or sets size to zero, depending on the utility.
    /// </summary>
    public StrokeBuilder None => Chain("stroke", "none");

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public StrokeBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public StrokeBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public StrokeBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public StrokeBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public StrokeBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public StrokeBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeBuilder Chain(string utility, string value)
    {
        var breakpoint = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new StrokeRule(utility, value, breakpoint));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private StrokeBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        _pendingBreakpoint = breakpoint;
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
            var cls = $"{rule.Utility}-{rule.Value}";
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

    public override string ToString() => ToClass();
}
