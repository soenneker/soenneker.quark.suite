using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

public sealed class LetterSpacingBuilder : ICssBuilder
{
    private readonly List<LetterSpacingRule> _rules = new(4);

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

    public LetterSpacingBuilder OnBase => ChainBreakpoint(BreakpointType.Base);
    public LetterSpacingBuilder OnSm => ChainBreakpoint(BreakpointType.Sm);
    public LetterSpacingBuilder OnMd => ChainBreakpoint(BreakpointType.Md);
    public LetterSpacingBuilder OnLg => ChainBreakpoint(BreakpointType.Lg);
    public LetterSpacingBuilder OnXl => ChainBreakpoint(BreakpointType.Xl);
    public LetterSpacingBuilder On2xl => ChainBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LetterSpacingBuilder Chain(string utility, string value)
    {
        _rules.Add(new LetterSpacingRule(utility, value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LetterSpacingBuilder ChainBreakpoint(BreakpointType breakpoint)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new LetterSpacingRule("tracking-normal", "", breakpoint));
            return this;
        }

        int lastIdx = _rules.Count - 1;
        LetterSpacingRule last = _rules[lastIdx];
        _rules[lastIdx] = new LetterSpacingRule(last.Utility, last.Value, breakpoint);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        bool first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            LetterSpacingRule rule = _rules[i];
            string cls = rule.Value.Length == 0 ? rule.Utility : $"{rule.Utility}-{rule.Value}";
            string bp = BreakpointUtil.GetBreakpointClass(rule.Breakpoint);
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
