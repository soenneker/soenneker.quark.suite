using Soenneker.Quark.Attributes;
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
    private BreakpointType? _pendingBreakpoint;

    internal BackgroundBlendModeBuilder(string value, BreakpointType? breakpoint = null)
    {
        _rules.Add(new BackgroundBlendModeRule(value, breakpoint));
    }

    internal BackgroundBlendModeBuilder(List<BackgroundBlendModeRule> rules)
    {
        if (rules is { Count: > 0 })
            _rules.AddRange(rules);
    }

    /// <summary>
    /// Fluent step for `Normal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Normal => Chain("normal");
    /// <summary>
    /// Fluent step for `Multiply` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Multiply => Chain("multiply");
    /// <summary>
    /// Viewport-relative size (e.g. `w-screen`, `min-h-screen`) tied to the viewport width/height.
    /// </summary>
    public BackgroundBlendModeBuilder Screen => Chain("screen");
    /// <summary>
    /// Fluent step for `Overlay` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Overlay => Chain("overlay");
    /// <summary>
    /// Fluent step for `Darken` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Darken => Chain("darken");
    /// <summary>
    /// Fluent step for `Lighten` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Lighten => Chain("lighten");
    /// <summary>
    /// Fluent step for `Color Dodge` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder ColorDodge => Chain("color-dodge");
    /// <summary>
    /// Fluent step for `Color Burn` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder ColorBurn => Chain("color-burn");
    /// <summary>
    /// Fluent step for `Hard Light` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder HardLight => Chain("hard-light");
    /// <summary>
    /// Fluent step for `Soft Light` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder SoftLight => Chain("soft-light");
    /// <summary>
    /// Fluent step for `Difference` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Difference => Chain("difference");
    /// <summary>
    /// Fluent step for `Exclusion` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Exclusion => Chain("exclusion");
    /// <summary>
    /// Fluent step for `Hue` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Hue => Chain("hue");
    /// <summary>
    /// Fluent step for `Saturation` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Saturation => Chain("saturation");
    /// <summary>
    /// Fluent step for `Color` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Color => Chain("color");
    /// <summary>
    /// Fluent step for `Luminosity` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public BackgroundBlendModeBuilder Luminosity => Chain("luminosity");

    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public BackgroundBlendModeBuilder OnSm => SetPendingBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public BackgroundBlendModeBuilder OnMd => SetPendingBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public BackgroundBlendModeBuilder OnLg => SetPendingBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public BackgroundBlendModeBuilder OnXl => SetPendingBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public BackgroundBlendModeBuilder On2xl => SetPendingBreakpoint(BreakpointType.Xxl);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundBlendModeBuilder Chain(string value)
    {
        _rules.Add(new BackgroundBlendModeRule(value, ConsumePendingBreakpoint()));
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private BackgroundBlendModeBuilder SetPendingBreakpoint(BreakpointType breakpoint)
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
