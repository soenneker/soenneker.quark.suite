using Soenneker.Quark.Attributes;
using Soenneker.Utils.PooledStringBuilders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Accent color builder for form controls. Tailwind: accent-auto, accent-primary, accent-*.
/// </summary>
[TailwindPrefix("accent-", Responsive = true)]
public sealed class AccentColorBuilder : ICssBuilder
{
    private readonly List<AccentColorRule> _rules = new(4);

    internal AccentColorBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new AccentColorRule(value, breakpoint));
    }

    internal AccentColorBuilder(List<AccentColorRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public AccentColorBuilder Auto => Chain("auto");
    public AccentColorBuilder Primary => Chain("primary");
    public AccentColorBuilder Transparent => Chain("transparent");
    public AccentColorBuilder Current => Chain("current");
    public AccentColorBuilder Inherit => Chain(GlobalKeyword.InheritValue);
    public AccentColorBuilder Initial => Chain(GlobalKeyword.InitialValue);
    public AccentColorBuilder Unset => Chain(GlobalKeyword.UnsetValue);

    public AccentColorBuilder OnSm => ChainBp(BreakpointType.Sm);
    public AccentColorBuilder OnMd => ChainBp(BreakpointType.Md);
    public AccentColorBuilder OnLg => ChainBp(BreakpointType.Lg);
    public AccentColorBuilder OnXl => ChainBp(BreakpointType.Xl);
    public AccentColorBuilder On2xl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AccentColorBuilder Chain(string value)
    {
        _rules.Add(new AccentColorRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private AccentColorBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new AccentColorRule("auto", bp));
        else _rules[^1] = new AccentColorRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = GetClass(rule.Value);
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
            var styleVal = rule.Value switch
            {
                "auto" => "auto",
                "primary" => "var(--primary)",
                "transparent" => "transparent",
                "current" => "currentColor",
                "inherit" => "inherit",
                "initial" => "initial",
                "unset" => "unset",
                _ => rule.Value.StartsWith("--") || rule.Value.StartsWith("#") || rule.Value.StartsWith("rgb") ? rule.Value : null
            };
            if (styleVal is null) continue;
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("accent-color: ");
            sb.Append(styleVal);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static string GetClass(string v) => v switch
    {
        "auto" => "accent-auto",
        "primary" => "accent-primary",
        "transparent" => "accent-transparent",
        "current" => "accent-current",
        "inherit" => "accent-inherit",
        "initial" => "accent-initial",
        "unset" => "accent-unset",
        _ => v.StartsWith("accent-") ? v : string.Empty
    };
}
