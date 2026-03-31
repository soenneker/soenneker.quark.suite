using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// CSS isolation builder. Tailwind: isolation-auto, isolation-isolate.
/// </summary>
[TailwindPrefix("isolation-", Responsive = true)]
public sealed class IsolationBuilder : ICssBuilder
{
    private readonly List<IsolationRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal IsolationBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new IsolationRule(value, breakpoint));
    }

    internal IsolationBuilder(List<IsolationRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public IsolationBuilder Auto => Chain("auto");
    public IsolationBuilder Isolate => Chain("isolate");

    public IsolationBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public IsolationBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public IsolationBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public IsolationBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public IsolationBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IsolationBuilder Chain(string value)
    {
        _rules.Add(new IsolationRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private IsolationBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
            var cls = rule.Value switch { "auto" => "isolation-auto", "isolate" => "isolation-isolate", _ => string.Empty };
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
            if (rule.Value is not ("auto" or "isolate")) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("isolation: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
