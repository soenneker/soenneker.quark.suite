using Soenneker.Utils.PooledStringBuilders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Soenneker.Quark;

/// <summary>
/// Builds Tailwind/shadcn radius classes.
/// </summary>
public sealed class RadiusBuilder : ICssBuilder
{
    private readonly List<RadiusRule> _rules = new(4);

    private string? _pendingPosition;
    private BreakpointType? _pendingBreakpoint;

    private const string _base = "rounded";

    internal RadiusBuilder()
    {
    }

    internal RadiusBuilder(List<RadiusRule> rules, string? position = null, BreakpointType? bp = null)
    {
        if (rules.Count > 0)
            _rules.AddRange(rules);

        _pendingPosition = position;
        _pendingBreakpoint = bp;
    }

    // ----- Positions -----

    /// <summary>
    /// Targets all corners (`rounded-*` with no corner suffix).
    /// </summary>
    public RadiusBuilder All => SetPosition(null);
    /// <summary>
    /// Top corners only (`rounded-t-*`).
    /// </summary>
    public RadiusBuilder Top => SetPosition("t");
    /// <summary>
    /// Bottom corners only (`rounded-b-*`).
    /// </summary>
    public RadiusBuilder Bottom => SetPosition("b");
    /// <summary>
    /// Left corners only (`rounded-l-*`).
    /// </summary>
    public RadiusBuilder Left => SetPosition("l");
    /// <summary>
    /// Right corners only (`rounded-r-*`).
    /// </summary>
    public RadiusBuilder Right => SetPosition("r");

    /// <summary>
    /// Top-left corner only (`rounded-tl-*`).
    /// </summary>
    public RadiusBuilder TopLeft => SetPosition("tl");
    /// <summary>
    /// Top-right corner only (`rounded-tr-*`).
    /// </summary>
    public RadiusBuilder TopRight => SetPosition("tr");
    /// <summary>
    /// Bottom-left corner only (`rounded-bl-*`).
    /// </summary>
    public RadiusBuilder BottomLeft => SetPosition("bl");
    /// <summary>
    /// Bottom-right corner only (`rounded-br-*`).
    /// </summary>
    public RadiusBuilder BottomRight => SetPosition("br");

    // ----- Sizes -----

    /// <summary>
    /// Default theme radius: `rounded` with no suffix — in Tailwind’s default config typically `0.25rem` (maps to shadcn `--radius` usage when you align tokens).
    /// </summary>
    public RadiusBuilder Default => Add(null);
    /// <summary>
    /// <c>rounded-none</c> — <c>border-radius: 0</c> on the selected corners (fully square corners).
    /// </summary>
    public RadiusBuilder None => Add("none");
    /// <summary>
    /// `rounded-sm` — small radius (default theme `0.125rem`).
    /// </summary>
    public RadiusBuilder Sm => Add("sm");
    /// <summary>
    /// `rounded-md` — medium radius (default theme `0.375rem`); common for cards and inputs in shadcn.
    /// </summary>
    public RadiusBuilder Md => Add("md");
    /// <summary>
    /// `rounded-lg` — large radius (default theme `0.5rem`).
    /// </summary>
    public RadiusBuilder Lg => Add("lg");
    /// <summary>
    /// `rounded-xl` — extra-large radius (default theme `0.75rem`).
    /// </summary>
    public RadiusBuilder Xl => Add("xl");
    /// <summary>
    /// `rounded-2xl` — 2× XL radius (default theme `1rem`).
    /// </summary>
    public RadiusBuilder TwoXl => Add("2xl");
    /// <summary>
    /// `rounded-3xl` — very large radius (default theme `1.5rem`).
    /// </summary>
    public RadiusBuilder ThreeXl => Add("3xl");
    /// <summary>
    /// “Full” extremum for this utility. For border radius this is `rounded-full` (`border-radius: 9999px`), producing pills/circles; for width/height often `100%` (`w-full` / `h-full`).
    /// </summary>
    public RadiusBuilder Full => Add("full");
    /// <summary>
    /// Custom <c>rounded-*</c> suffix: theme scale key, arbitrary length (for example <c>[2vw]</c>), or CSS variable reference aligned with shadcn’s <c>--radius</c> pattern.
    /// </summary>
    /// <param name="value">The segment after <c>rounded-</c> (and any corner prefix such as <c>tl-</c>).</param>
    public RadiusBuilder Token(string value) => Add(value);

    // ----- Breakpoints -----

    /// <summary>
    /// Scopes the next utility to the default (unprefixed) breakpoint. In Tailwind’s mobile‑first model, unprefixed utilities apply from 0px unless a larger breakpoint overrides them.
    /// </summary>
    public RadiusBuilder OnBase => SetBreakpoint(BreakpointType.Base);
    /// <summary>
    /// Applies the preceding utility from the `sm` breakpoint and up (`sm:` prefix). Tailwind default: `min-width: 40rem` (640px).
    /// </summary>
    public RadiusBuilder OnSm => SetBreakpoint(BreakpointType.Sm);
    /// <summary>
    /// Applies from the `md` breakpoint and up (`md:`). Tailwind default: `min-width: 48rem` (768px).
    /// </summary>
    public RadiusBuilder OnMd => SetBreakpoint(BreakpointType.Md);
    /// <summary>
    /// Applies from the `lg` breakpoint and up (`lg:`). Tailwind default: `min-width: 64rem` (1024px).
    /// </summary>
    public RadiusBuilder OnLg => SetBreakpoint(BreakpointType.Lg);
    /// <summary>
    /// Applies from the `xl` breakpoint and up (`xl:`). Tailwind default: `min-width: 80rem` (1280px).
    /// </summary>
    public RadiusBuilder OnXl => SetBreakpoint(BreakpointType.Xl);
    /// <summary>
    /// Applies from the `2xl` breakpoint and up (`2xl:`). Tailwind default: `min-width: 96rem` (1536px).
    /// </summary>
    public RadiusBuilder On2xl => SetBreakpoint(BreakpointType.Xxl);

    // ----- Core -----

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RadiusBuilder SetPosition(string? pos)
    {
        _pendingPosition = pos;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RadiusBuilder SetBreakpoint(BreakpointType bp)
    {
        _pendingBreakpoint = bp;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private RadiusBuilder Add(string? size)
    {
        _rules.Add(new RadiusRule(size, _pendingPosition, _pendingBreakpoint));

        _pendingPosition = null;
        _pendingBreakpoint = null;

        return this;
    }

    // ----- Output -----

    public string ToClass()
    {
        if (_rules.Count == 0)
            return string.Empty;

        using var sb = new PooledStringBuilder();
        bool first = true;

        foreach (var rule in _rules)
        {
            var bp = BreakpointUtil.GetBreakpointToken(rule.Breakpoint);

            if (!first)
                sb.Append(' ');
            else
                first = false;

            if (bp.Length > 0)
            {
                sb.Append(bp);
                sb.Append(':');
            }

            sb.Append(_base);

            if (rule.PositionToken is { Length: > 0 })
            {
                sb.Append('-');
                sb.Append(rule.PositionToken);
            }

            if (rule.SizeToken is { Length: > 0 })
            {
                sb.Append('-');
                sb.Append(rule.SizeToken);
            }
        }

        return sb.ToString();
    }

    public string ToStyle()
    {
        if (_rules.Count == 0)
            return string.Empty;

        var sb = new PooledStringBuilder();
        bool first = true;
        try
        {
            foreach (var rule in _rules)
            {
                var size = GetCssRadiusValue(rule.SizeToken);

                if (size.Length == 0)
                    continue;

                switch (rule.PositionToken)
                {
                    case "tl":
                        AppendStyle(ref sb, ref first, "border-top-left-radius", size);
                        break;
                    case "tr":
                        AppendStyle(ref sb, ref first, "border-top-right-radius", size);
                        break;
                    case "bl":
                        AppendStyle(ref sb, ref first, "border-bottom-left-radius", size);
                        break;
                    case "br":
                        AppendStyle(ref sb, ref first, "border-bottom-right-radius", size);
                        break;
                    case "t":
                        AppendStyle(ref sb, ref first, "border-top-left-radius", size);
                        AppendStyle(ref sb, ref first, "border-top-right-radius", size);
                        break;
                    case "b":
                        AppendStyle(ref sb, ref first, "border-bottom-left-radius", size);
                        AppendStyle(ref sb, ref first, "border-bottom-right-radius", size);
                        break;
                    case "l":
                        AppendStyle(ref sb, ref first, "border-top-left-radius", size);
                        AppendStyle(ref sb, ref first, "border-bottom-left-radius", size);
                        break;
                    case "r":
                        AppendStyle(ref sb, ref first, "border-top-right-radius", size);
                        AppendStyle(ref sb, ref first, "border-bottom-right-radius", size);
                        break;
                    default:
                        AppendStyle(ref sb, ref first, "border-radius", size);
                        break;
                }
            }

            return sb.ToString();
        }
        finally
        {
            sb.Dispose();
        }
    }

    public override string ToString() => ToClass();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void AppendStyle(ref PooledStringBuilder sb, ref bool first, string property, string value)
    {
        if (!first)
            sb.Append("; ");
        else
            first = false;

        sb.Append(property);
        sb.Append(": ");
        sb.Append(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string GetCssRadiusValue(string? sizeToken)
    {
        return sizeToken switch
        {
            null => "0.25rem",
            "none" => "0",
            "sm" => "0.125rem",
            "md" => "0.375rem",
            "lg" => "0.5rem",
            "xl" => "0.75rem",
            "2xl" => "1rem",
            "3xl" => "1.5rem",
            "full" => "9999px",
            _ => string.Empty
        };
    }
}