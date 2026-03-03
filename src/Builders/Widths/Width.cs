namespace Soenneker.Quark;

/// <summary>
/// Width utility with fluent API and Tailwind-first mappings.
/// </summary>
public static class Width
{
    public static WidthBuilder Is25 => new("25");
    public static WidthBuilder Is50 => new("50");
    public static WidthBuilder Is75 => new("75");
    public static WidthBuilder Is100 => new("100");

    public static WidthBuilder Is0 => new("0");
    public static WidthBuilder IsPx => new("px");
    public static WidthBuilder IsFull => new("full");
    public static WidthBuilder IsScreen => new("screen");
    public static WidthBuilder IsFit => new("fit");
    public static WidthBuilder IsMin => new("min");
    public static WidthBuilder IsMax => new("max");

    public static WidthBuilder Is1of2 => new("1/2");
    public static WidthBuilder Is1of3 => new("1/3");
    public static WidthBuilder Is2of3 => new("2/3");
    public static WidthBuilder Is1of4 => new("1/4");
    public static WidthBuilder Is3of4 => new("3/4");
    public static WidthBuilder Is1of5 => new("1/5");
    public static WidthBuilder Is2of5 => new("2/5");
    public static WidthBuilder Is3of5 => new("3/5");
    public static WidthBuilder Is4of5 => new("4/5");
    public static WidthBuilder Is1of6 => new("1/6");
    public static WidthBuilder Is5of6 => new("5/6");

    public static WidthBuilder Auto => new("auto");

    /// <summary>
    /// Create from an arbitrary Tailwind width token (e.g. "72", "[18rem]", "full").
    /// </summary>
    public static WidthBuilder Token(string token) => new(token);
}
