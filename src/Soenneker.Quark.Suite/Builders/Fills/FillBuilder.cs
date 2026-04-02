using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("fill-", Responsive = true)]
public sealed class FillBuilder : ICssBuilder
{
    private readonly List<FillRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal FillBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new FillRule(utility, value, breakpoint));
    }

    public FillBuilder Token(string value) => Chain("fill", value);
    public FillBuilder None => Chain("fill", "none");
    public FillBuilder Current => Chain("fill", "current");

    public FillBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public FillBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public FillBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public FillBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public FillBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public FillBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillBuilder Chain(string utility, string value)
    {
        _rules.Add(new FillRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private FillBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
}
