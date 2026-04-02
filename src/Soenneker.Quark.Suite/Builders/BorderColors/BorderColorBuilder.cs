using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("border-", Responsive = true)]
public sealed class BorderColorBuilder : ICssBuilder
{
    private static readonly HashSet<string> SemanticTokens = new(System.StringComparer.Ordinal)
    {
        "primary",
        "secondary",
        "destructive",
        "muted",
        "accent",
        "popover",
        "card",
        "background",
        "border",
        "input",
        "ring",
        "white",
        "black",
        "transparent"
    };

    private const string Prefix = "border-";

    private readonly List<ColorRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal BorderColorBuilder(string value, BreakpointType? breakpoint = null, bool isUtility = false)
    {
        _rules.Add(new ColorRule(value, breakpoint, isUtility));
    }

    internal BorderColorBuilder(List<ColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BorderColorBuilder Primary => ChainValue("primary");
    public BorderColorBuilder Secondary => ChainValue("secondary");
    public BorderColorBuilder Destructive => ChainValue("destructive");
    public BorderColorBuilder Muted => ChainValue("muted");
    public BorderColorBuilder Accent => ChainValue("accent");
    public BorderColorBuilder Popover => ChainValue("popover");
    public BorderColorBuilder Card => ChainValue("card");
    public BorderColorBuilder Background => ChainValue("background");
    public BorderColorBuilder Border => ChainValue("border");
    public BorderColorBuilder Input => ChainValue("input");
    public BorderColorBuilder Ring => ChainValue("ring");

    public BorderColorBuilder White => ChainValue("white");
    public BorderColorBuilder Black => ChainValue("black");
    public BorderColorBuilder Transparent => ChainValue("transparent");

    public BorderColorBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public BorderColorBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public BorderColorBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public BorderColorBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public BorderColorBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public BorderColorBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    public BorderColorBuilder Token(string token) => ChainValue(token);

    public BorderColorBuilder Utility(string utility) => ChainValue(utility, isUtility: true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderColorBuilder ChainValue(string value, bool isUtility = false)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new ColorRule(value, bp, isUtility));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BorderColorBuilder SetPendingBreakpoint(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
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
            var cls = ColorUtility.GetClass(Prefix, rule, SemanticTokens);
            if (cls.Length == 0)
                continue;

            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);
            if (bp.Length != 0)
                cls = BreakpointUtil.ApplyTailwindBreakpoint(cls, bp);

            if (!first) sb.Append(' ');
            else first = false;

            sb.Append(cls);
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        return string.Empty;
    }

    public override string ToString() => ToClass();
}