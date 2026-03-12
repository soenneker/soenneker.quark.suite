using Soenneker.Quark.Attributes;
using Soenneker.Quark.Enums;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Background-blend-mode builder. Tailwind: bg-blend-normal, bg-blend-multiply, etc.
/// </summary>
[TailwindPrefix("bg-blend-", Responsive = true)]
public sealed class BackgroundBlendModeBuilder : ICssBuilder
{
    private readonly List<BackgroundBlendModeRule> _rules = new(4);

    internal BackgroundBlendModeBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackgroundBlendModeRule(value, breakpoint));
    }

    internal BackgroundBlendModeBuilder(List<BackgroundBlendModeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    public BackgroundBlendModeBuilder Normal => Chain("normal");
    public BackgroundBlendModeBuilder Multiply => Chain("multiply");
    public BackgroundBlendModeBuilder Screen => Chain("screen");
    public BackgroundBlendModeBuilder Overlay => Chain("overlay");
    public BackgroundBlendModeBuilder Darken => Chain("darken");
    public BackgroundBlendModeBuilder Lighten => Chain("lighten");
    public BackgroundBlendModeBuilder ColorDodge => Chain("color-dodge");
    public BackgroundBlendModeBuilder ColorBurn => Chain("color-burn");
    public BackgroundBlendModeBuilder HardLight => Chain("hard-light");
    public BackgroundBlendModeBuilder SoftLight => Chain("soft-light");
    public BackgroundBlendModeBuilder Difference => Chain("difference");
    public BackgroundBlendModeBuilder Exclusion => Chain("exclusion");
    public BackgroundBlendModeBuilder Hue => Chain("hue");
    public BackgroundBlendModeBuilder Saturation => Chain("saturation");
    public BackgroundBlendModeBuilder Color => Chain("color");
    public BackgroundBlendModeBuilder Luminosity => Chain("luminosity");

    public BackgroundBlendModeBuilder OnSm => ChainBp(BreakpointType.Sm);
    public BackgroundBlendModeBuilder OnMd => ChainBp(BreakpointType.Md);
    public BackgroundBlendModeBuilder OnLg => ChainBp(BreakpointType.Lg);
    public BackgroundBlendModeBuilder OnXl => ChainBp(BreakpointType.Xl);
    public BackgroundBlendModeBuilder OnXxl => ChainBp(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundBlendModeBuilder Chain(string value)
    {
        _rules.Add(new BackgroundBlendModeRule(value, null));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundBlendModeBuilder ChainBp(BreakpointType bp)
    {
        if (_rules.Count == 0) _rules.Add(new BackgroundBlendModeRule("normal", bp));
        else _rules[^1] = new BackgroundBlendModeRule(_rules[^1].Value, bp);
        return this;
    }

    public string ToClass()
    {
        if (_rules.Count == 0) return string.Empty;
        using var sb = new PooledStringBuilder();
        var first = true;
        foreach (var rule in _rules)
        {
            var cls = "bg-blend-" + rule.Value.Replace(" ", "-", System.StringComparison.Ordinal);
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
            sb.Append("background-blend-mode: ");
            sb.Append(rule.Value);
        }
        return sb.ToString();
    }

    public override string ToString() => ToClass();
}
