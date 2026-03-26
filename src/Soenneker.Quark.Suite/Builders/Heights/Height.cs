namespace Soenneker.Quark;

/// <summary>
/// Height utility with fluent API and Tailwind-first mappings.
/// </summary>
public static class Height
{
    public static HeightBuilder Is25 => new("25");
    public static HeightBuilder Is50 => new("50");
    public static HeightBuilder Is75 => new("75");
    public static HeightBuilder Is100 => new("100");

    public static HeightBuilder Is0 => new("0");
    public static HeightBuilder Is8 => new("8");
    public static HeightBuilder IsPx => new("px");
    public static HeightBuilder IsFull => new("full");
    public static HeightBuilder IsScreen => new("screen");
    public static HeightBuilder IsFit => new("fit");
    public static HeightBuilder IsMin => new("min");
    public static HeightBuilder IsMax => new("max");

    public static HeightBuilder Is1of2 => new("1/2");
    public static HeightBuilder Is1of3 => new("1/3");
    public static HeightBuilder Is2of3 => new("2/3");
    public static HeightBuilder Is1of4 => new("1/4");
    public static HeightBuilder Is3of4 => new("3/4");

    public static HeightBuilder Auto => new("auto");

    /// <summary>
    /// Create from an arbitrary Tailwind height token (e.g. "72", "[18rem]", "full").
    /// </summary>
    public static HeightBuilder Token(string token) => new(token);
}
