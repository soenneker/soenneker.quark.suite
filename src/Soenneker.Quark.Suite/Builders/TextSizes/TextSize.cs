namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned text size utility.
/// </summary>
public static class TextSize
{
    public static TextSizeBuilder Xs => new("xs");
    public static TextSizeBuilder Sm => new("sm");
    public static TextSizeBuilder Base => new("base");
    public static TextSizeBuilder Lg => new("lg");
    public static TextSizeBuilder Xl => new("xl");
    public static TextSizeBuilder TwoXl => new("2xl");
    public static TextSizeBuilder ThreeXl => new("3xl");
    public static TextSizeBuilder FourXl => new("4xl");

    /// <summary>
    /// Creates an arbitrary Tailwind text size token (e.g. "5xl", "[18px]").
    /// </summary>
    public static TextSizeBuilder Token(string value) => new(value);
}
