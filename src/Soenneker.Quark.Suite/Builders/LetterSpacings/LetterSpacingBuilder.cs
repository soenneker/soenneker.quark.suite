using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("tracking-", Responsive = true)]
public sealed class LetterSpacingBuilder : ICssBuilder
{
    private readonly List<LetterSpacingRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal LetterSpacingBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new LetterSpacingRule(utility, value, breakpoint));
    }

    public LetterSpacingBuilder Tighter => Chain("tracking-tighter", "");
    public LetterSpacingBuilder Tight => Chain("tracking-tight", "");
    public LetterSpacingBuilder Normal => Chain("tracking-normal", "");
    public LetterSpacingBuilder Wide => Chain("tracking-wide", "");
    public LetterSpacingBuilder Wider => Chain("tracking-wider", "");
    public LetterSpacingBuilder Widest => Chain("tracking-widest", "");
    public LetterSpacingBuilder Token(string value) => Chain("tracking", value);

    public LetterSpacingBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public LetterSpacingBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public LetterSpacingBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public LetterSpacingBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public LetterSpacingBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public LetterSpacingBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LetterSpacingBuilder Chain(string utility, string value)
    {
        _rules.Add(new LetterSpacingRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LetterSpacingBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
