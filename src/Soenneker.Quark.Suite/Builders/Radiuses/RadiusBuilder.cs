using Soenneker.Utils.PooledStringBuilders;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Soenneker.Quark.Enums;

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

    public RadiusBuilder All => SetPosition(null);
    public RadiusBuilder Top => SetPosition("t");
    public RadiusBuilder Bottom => SetPosition("b");
    public RadiusBuilder Left => SetPosition("l");
    public RadiusBuilder Right => SetPosition("r");

    public RadiusBuilder TopLeft => SetPosition("tl");
    public RadiusBuilder TopRight => SetPosition("tr");
    public RadiusBuilder BottomLeft => SetPosition("bl");
    public RadiusBuilder BottomRight => SetPosition("br");

    // ----- Sizes -----

    public RadiusBuilder Default => Add(null);
    public RadiusBuilder None => Add("none");
    public RadiusBuilder Sm => Add("sm");
    public RadiusBuilder Md => Add("md");
    public RadiusBuilder Lg => Add("lg");
    public RadiusBuilder Xl => Add("xl");
    public RadiusBuilder TwoXl => Add("2xl");
    public RadiusBuilder ThreeXl => Add("3xl");
    public RadiusBuilder Full => Add("full");
    public RadiusBuilder Token(string value) => Add(value);

    // ----- Breakpoints -----

    public RadiusBuilder OnBase => SetBreakpoint(BreakpointType.Base);
    public RadiusBuilder OnSm => SetBreakpoint(BreakpointType.Sm);
    public RadiusBuilder OnMd => SetBreakpoint(BreakpointType.Md);
    public RadiusBuilder OnLg => SetBreakpoint(BreakpointType.Lg);
    public RadiusBuilder OnXl => SetBreakpoint(BreakpointType.Xl);
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