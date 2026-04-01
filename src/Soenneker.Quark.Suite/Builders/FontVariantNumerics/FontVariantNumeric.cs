namespace Soenneker.Quark;

/// <summary>
/// Tailwind font variant numeric utility entry points.
/// </summary>
public static class FontVariantNumeric
{
    public static FontVariantNumericBuilder NormalNums => new("normal-nums");
    public static FontVariantNumericBuilder Ordinal => new("ordinal");
    public static FontVariantNumericBuilder SlashedZero => new("slashed-zero");
    public static FontVariantNumericBuilder LiningNums => new("lining-nums");
    public static FontVariantNumericBuilder OldstyleNums => new("oldstyle-nums");
    public static FontVariantNumericBuilder ProportionalNums => new("proportional-nums");
    public static FontVariantNumericBuilder TabularNums => new("tabular-nums");
    public static FontVariantNumericBuilder DiagonalFractions => new("diagonal-fractions");
    public static FontVariantNumericBuilder StackedFractions => new("stacked-fractions");
}
