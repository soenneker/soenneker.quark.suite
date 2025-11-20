using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;


namespace Soenneker.Quark;

public sealed class VisibilityBuilder : ICssBuilder
{
    private readonly List<VisibilityRule> _rules = new(4);

    private const string _classInvisible = "invisible";
    private const string _classVisible = "visible";

    internal VisibilityBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new VisibilityRule(value, breakpoint));
    }

    internal VisibilityBuilder(List<VisibilityRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public VisibilityBuilder Visible => Chain(VisibilityKeyword.VisibleValue);
    public VisibilityBuilder Invisible => Chain("invisible");
    public VisibilityBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public VisibilityBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public VisibilityBuilder Revert => Chain(GlobalKeyword.RevertValue);
    public VisibilityBuilder RevertLayer => Chain(GlobalKeyword.RevertLayerValue);
    public VisibilityBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public VisibilityBuilder OnPhone => ChainBp(BreakpointType.Phone);
    public VisibilityBuilder OnTablet => ChainBp(BreakpointType.Tablet);
    public VisibilityBuilder OnLaptop => ChainBp(BreakpointType.Laptop);
    public VisibilityBuilder OnDesktop => ChainBp(BreakpointType.Desktop);
    public VisibilityBuilder OnWidescreen => ChainBp(BreakpointType.Widescreen);
    public VisibilityBuilder OnUltrawide => ChainBp(BreakpointType.Ultrawide);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VisibilityBuilder Chain(string value)
    {
        _rules.Add(new VisibilityRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private VisibilityBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0)
        {
            _rules.Add(new VisibilityRule(VisibilityKeyword.VisibleValue, bp));
            return this;
        }

        var lastIdx = _rules.Count - 1;
        var last = _rules[lastIdx];
        _rules[lastIdx] = new VisibilityRule(last.Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;

        using var sb = new PooledStringBuilder();
        var first = true;
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var cls = rule.Value switch
            {
                "invisible" => _classInvisible,
                VisibilityKeyword.VisibleValue => _classVisible,
                _ => string.Empty
            };
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.InsertBreakpointType(cls, bp);

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
        for (var i = 0; i < _rules.Count; i++)
        {
            var rule = _rules[i];
            var css = rule.Value switch
            {
                "invisible" => "visibility: hidden",
                VisibilityKeyword.VisibleValue => "visibility: visible",
                GlobalKeyword.InheritValue => "visibility: inherit",
                GlobalKeyword.InitialValue => "visibility: initial",
                GlobalKeyword.UnsetValue => "visibility: unset",
                GlobalKeyword.RevertValue => "visibility: revert",
                GlobalKeyword.RevertLayerValue => "visibility: revert-layer",
                _ => null
            };
            if (css is null) continue;

            if (!first) sb.Append("; ");
            else first = false;

            sb.Append(css);
        }
        return sb.ToString();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetBp(BreakpointType? breakpoint) => breakpoint?.Value ?? string.Empty;

}

 
