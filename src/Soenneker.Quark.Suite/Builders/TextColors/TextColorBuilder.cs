using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

[TailwindPrefix("text-", Responsive = true)]
public sealed class TextColorBuilder : ICssBuilder
{
    private static readonly HashSet<string> SemanticTokens = new(System.StringComparer.Ordinal)
    {
        "primary",
        "primary-foreground",
        "secondary",
        "secondary-foreground",
        "destructive",
        "destructive-foreground",
        "muted-foreground",
        "accent",
        "accent-foreground",
        "popover-foreground",
        "card-foreground",
        "foreground",
        "white",
        "black"
    };

    private const string Prefix = "text-";

    private readonly List<ColorRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal TextColorBuilder(string value, BreakpointType? breakpoint = null, bool isUtility = false)
    {
        _rules.Add(new ColorRule(value, breakpoint, isUtility));
    }

    internal TextColorBuilder(List<ColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public TextColorBuilder Primary => ChainValue("primary");
    public TextColorBuilder PrimaryForeground => ChainValue("primary-foreground");
    public TextColorBuilder Secondary => ChainValue("secondary");
    public TextColorBuilder SecondaryForeground => ChainValue("secondary-foreground");
    public TextColorBuilder Destructive => ChainValue("destructive");
    public TextColorBuilder DestructiveForeground => ChainValue("destructive-foreground");
    public TextColorBuilder MutedForeground => ChainValue("muted-foreground");
    public TextColorBuilder Accent => ChainValue("accent");
    public TextColorBuilder AccentForeground => ChainValue("accent-foreground");
    public TextColorBuilder PopoverForeground => ChainValue("popover-foreground");
    public TextColorBuilder CardForeground => ChainValue("card-foreground");
    public TextColorBuilder Foreground => ChainValue("foreground");
    public TextColorBuilder White => ChainValue("white");
    public TextColorBuilder Black => ChainValue("black");

    public TextColorBuilder OnBase => SetPendingBreakpoint(BreakpointType.Base);
    public TextColorBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    public TextColorBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    public TextColorBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    public TextColorBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    public TextColorBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    public TextColorBuilder Token(string token) => ChainValue(token);

    public TextColorBuilder Utility(string utility) => ChainValue(utility, isUtility: true);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextColorBuilder ChainValue(string value, bool isUtility = false)
    {
        var bp = _pendingBreakpoint;
        _pendingBreakpoint = null;
        _rules.Add(new ColorRule(value, bp, isUtility));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TextColorBuilder SetPendingBreakpoint(BreakpointType bp)
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

    public string ToStyle() => string.Empty;

    public override string ToString() => ToClass();
}
