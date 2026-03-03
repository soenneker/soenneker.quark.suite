using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Mix-blend-mode builder. Tailwind: mix-blend-normal, mix-blend-multiply, etc.
/// </summary>
public sealed class MixBlendModeBuilder : ICssBuilder
{
    private readonly List<MixBlendModeRule> _rules = new(4);

    internal MixBlendModeBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new MixBlendModeRule(value, breakpoint));
    }

    internal MixBlendModeBuilder(List<MixBlendModeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public MixBlendModeBuilder Normal => Chain("normal");
    public MixBlendModeBuilder Multiply => Chain("multiply");
    public MixBlendModeBuilder Screen => Chain("screen");
    public MixBlendModeBuilder Overlay => Chain("overlay");
    public MixBlendModeBuilder Darken => Chain("darken");
    public MixBlendModeBuilder Lighten => Chain("lighten");
    public MixBlendModeBuilder ColorDodge => Chain("color-dodge");
    public MixBlendModeBuilder ColorBurn => Chain("color-burn");
    public MixBlendModeBuilder HardLight => Chain("hard-light");
    public MixBlendModeBuilder SoftLight => Chain("soft-light");
    public MixBlendModeBuilder Difference => Chain("difference");
    public MixBlendModeBuilder Exclusion => Chain("exclusion");
    public MixBlendModeBuilder Hue => Chain("hue");
    public MixBlendModeBuilder Saturation => Chain("saturation");
    public MixBlendModeBuilder Color => Chain("color");
    public MixBlendModeBuilder Luminosity => Chain("luminosity");
    public MixBlendModeBuilder PlusDarker => Chain("plus-darker");
    public MixBlendModeBuilder PlusLighter => Chain("plus-lighter");

    public MixBlendModeBuilder OnSm => ChainBp(BreakpointType.Sm);
    public MixBlendModeBuilder OnMd => ChainBp(BreakpointType.Md);
    public MixBlendModeBuilder OnLg => ChainBp(BreakpointType.Lg);
    public MixBlendModeBuilder OnXl => ChainBp(BreakpointType.Xl);
    public MixBlendModeBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MixBlendModeBuilder Chain(string value)
    {
        _rules.Add(new MixBlendModeRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MixBlendModeBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new MixBlendModeRule("normal", bp));
        else _rules[^1] = new MixBlendModeRule(_rules[^1].Value, bp);
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
            if (!first) sb.Append("; ");
            else first = false;
            sb.Append("mix-blend-mode: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();

    private static string GetClass(string v)
    {
        if (string.IsNullOrEmpty(v)) return string.Empty;
        var tail = v.Replace(" ", "-", System.StringComparison.Ordinal);
        return "mix-blend-" + tail;
    }
}
