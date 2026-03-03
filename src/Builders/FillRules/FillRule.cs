namespace Soenneker.Quark;

/// <summary>
/// Static utility for fill-rule (SVG). Tailwind: fill-rule-evenodd, fill-rule-nonzero.
/// </summary>
public static class SvgFillRule
{
    public static FillRuleBuilder Evenodd => new("evenodd");
    public static FillRuleBuilder Nonzero => new("nonzero");
}
