using Soenneker.Quark.Attributes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Utils.PooledStringBuilders;

namespace Soenneker.Quark;

/// <summary>
/// Mix-blend-mode builder. Tailwind: mix-blend-normal, mix-blend-multiply, etc.
/// </summary>
[TailwindPrefix("mix-blend-", Responsive = true)]
public sealed class MixBlendModeBuilder : ICssBuilder
{
    private readonly List<MixBlendModeRule> _rules = new(4);
    private BreakpointType? _pendingBreakpoint;

    internal MixBlendModeBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new MixBlendModeRule(value, breakpoint));
    }

    internal MixBlendModeBuilder(List<MixBlendModeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `Normal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Normal => Chain("normal");
    /// <summary>
    /// Fluent step for `Multiply` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Multiply => Chain("multiply");
    /// <summary>
    /// Viewport-relative size (e.g. `w-screen`, `min-h-screen`) tied to the viewport width/height.
    /// </summary>
    public MixBlendModeBuilder Screen => Chain("screen");
    /// <summary>
    /// Fluent step for `Overlay` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Overlay => Chain("overlay");
    /// <summary>
    /// Fluent step for `Darken` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Darken => Chain("darken");
    /// <summary>
    /// Fluent step for `Lighten` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Lighten => Chain("lighten");
    /// <summary>
    /// Fluent step for `Color Dodge` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder ColorDodge => Chain("color-dodge");
    /// <summary>
    /// Fluent step for `Color Burn` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder ColorBurn => Chain("color-burn");
    /// <summary>
    /// Fluent step for `Hard Light` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder HardLight => Chain("hard-light");
    /// <summary>
    /// Fluent step for `Soft Light` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder SoftLight => Chain("soft-light");
    /// <summary>
    /// Fluent step for `Difference` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Difference => Chain("difference");
    /// <summary>
    /// Fluent step for `Exclusion` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Exclusion => Chain("exclusion");
    /// <summary>
    /// Fluent step for `Hue` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Hue => Chain("hue");
    /// <summary>
    /// Fluent step for `Saturation` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Saturation => Chain("saturation");
    /// <summary>
    /// Fluent step for `Color` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Color => Chain("color");
    /// <summary>
    /// Fluent step for `Luminosity` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder Luminosity => Chain("luminosity");
    /// <summary>
    /// Fluent step for `Plus Darker` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder PlusDarker => Chain("plus-darker");
    /// <summary>
    /// Fluent step for `Plus Lighter` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public MixBlendModeBuilder PlusLighter => Chain("plus-lighter");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public MixBlendModeBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public MixBlendModeBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public MixBlendModeBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public MixBlendModeBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public MixBlendModeBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MixBlendModeBuilder Chain(string value)
    {
        _rules.Add(new MixBlendModeRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private MixBlendModeBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
