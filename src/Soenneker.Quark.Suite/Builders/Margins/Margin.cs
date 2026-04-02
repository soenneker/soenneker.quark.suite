namespace Soenneker.Quark;

/// <summary>
/// Tailwind/shadcn-aligned margin utility.
/// </summary>
public static class Margin
{
    public static MarginBuilder Is0 => new(ScaleType.Is0Value);
    public static MarginBuilder Is1 => new(ScaleType.Is1Value);
    public static MarginBuilder Is2 => new(ScaleType.Is2Value);
    public static MarginBuilder Is3 => new(ScaleType.Is3Value);
    public static MarginBuilder Is4 => new(ScaleType.Is4Value);
    public static MarginBuilder Is5 => new(ScaleType.Is5Value);
    public static MarginBuilder Is8 => new("8");
    public static MarginBuilder Auto => new("auto");

    /// <summary>
    /// Creates a margin utility from an arbitrary Tailwind spacing token.
    /// </summary>
    public static MarginBuilder Token(string value) => new(value);
}
