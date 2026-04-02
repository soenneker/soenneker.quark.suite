namespace Soenneker.Quark;

/// <summary>
/// Static utility for fill-rule (SVG). Tailwind: fill-rule-evenodd, fill-rule-nonzero.
/// </summary>
public static class SvgFillRule
{
    /// <summary>
    /// Fluent step for `Evenodd` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FillRuleBuilder Evenodd => new("evenodd");
    /// <summary>
    /// Fluent step for `Nonzero` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FillRuleBuilder Nonzero => new("nonzero");
}
