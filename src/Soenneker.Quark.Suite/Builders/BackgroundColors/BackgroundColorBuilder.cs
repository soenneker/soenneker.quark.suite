using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("bg-", Responsive = true)]
public sealed class BackgroundColorBuilder : ICssBuilder
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
        "white",
        "black",
        "transparent"
    };

    private const string Prefix = "bg-";

    private readonly List<ColorRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal BackgroundColorBuilder(string value, BreakpointType? breakpoint = null, bool isUtility = false)
    {
        _rules.Add(new ColorRule(value, breakpoint, isUtility));
    }

    internal BackgroundColorBuilder(List<ColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BackgroundColorBuilder Primary => ChainValue("primary");
    public BackgroundColorBuilder Secondary => ChainValue("secondary");
    public BackgroundColorBuilder Destructive => ChainValue("destructive");
    public BackgroundColorBuilder Muted => ChainValue("muted");
    public BackgroundColorBuilder Accent => ChainValue("accent");
    public BackgroundColorBuilder Popover => ChainValue("popover");
    public BackgroundColorBuilder Card => ChainValue("card");
    public BackgroundColorBuilder Background => ChainValue("background");

    public BackgroundColorBuilder White => ChainValue("white");
    public BackgroundColorBuilder Black => ChainValue("black");
    public BackgroundColorBuilder Transparent => ChainValue("transparent");

    public BackgroundColorBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public BackgroundColorBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public BackgroundColorBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public BackgroundColorBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public BackgroundColorBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public BackgroundColorBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    public BackgroundColorBuilder Token(string token) => ChainValue(token);

    public BackgroundColorBuilder Utility(string utility) => ChainValue(utility, isUtility: true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder ChainValue(string value, bool isUtility = false)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new ColorRule(value, bp, isUtility));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundColorBuilder SetPendingBreakpoint(BreakpointType bp)
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