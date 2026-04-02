namespace Soenneker.Quark;

/// <summary>
/// Tailwind font variant numeric utility entry points.
/// </summary>
public static class FontVariantNumeric
{
    /// <summary>
    /// Fluent step for `Normal Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder NormalNums => new("normal-nums");
    /// <summary>
    /// Fluent step for `Ordinal` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder Ordinal => new("ordinal");
    /// <summary>
    /// Fluent step for `Slashed Zero` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder SlashedZero => new("slashed-zero");
    /// <summary>
    /// Fluent step for `Lining Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder LiningNums => new("lining-nums");
    /// <summary>
    /// Fluent step for `Oldstyle Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder OldstyleNums => new("oldstyle-nums");
    /// <summary>
    /// Fluent step for `Proportional Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder ProportionalNums => new("proportional-nums");
    /// <summary>
    /// Fluent step for `Tabular Nums` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder TabularNums => new("tabular-nums");
    /// <summary>
    /// Fluent step for `Diagonal Fractions` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder DiagonalFractions => new("diagonal-fractions");
    /// <summary>
    /// Fluent step for `Stacked Fractions` in this Tailwind/shadcn-aligned builder. See the corresponding `-*` utility in the Tailwind docs for exact CSS.
    /// </summary>
    public static FontVariantNumericBuilder StackedFractions => new("stacked-fractions");
}
