using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("bg-gradient-to-", Responsive = true)]
public sealed class GradientBuilder : ICssBuilder
{
    private readonly List<GradientRule> _rules = new(6);
    private BreakpointType? _pendingBreakpoint;

    internal GradientBuilder(string utility, string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new GradientRule(utility, value, breakpoint));
    }

    public GradientBuilder To(string value) => Chain("bg-gradient-to", value);
    public GradientBuilder From(string value) => Chain("from", value);
    public GradientBuilder Via(string value) => Chain("via", value);
    public GradientBuilder End(string value) => Chain("to", value);

    public GradientBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public GradientBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public GradientBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public GradientBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public GradientBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public GradientBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GradientBuilder Chain(string utility, string value)
    {
        _rules.Add(new GradientRule(utility, value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GradientBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
