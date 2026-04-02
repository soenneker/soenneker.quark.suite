namespace Soenneker.Quark;

/// <summary>
/// Static utility for end (inset-inline-end). Tailwind: end-*.
/// </summary>
public static class End
{
    public static EndBuilder Is0 => new(ScaleType.Is0Value);
    public static EndBuilder Is1 => new(ScaleType.Is1Value);
    public static EndBuilder Is2 => new(ScaleType.Is2Value);
    public static EndBuilder Is3 => new(ScaleType.Is3Value);
    public static EndBuilder Is4 => new(ScaleType.Is4Value);
    public static EndBuilder Is5 => new(ScaleType.Is5Value);
    public static EndBuilder Auto => new("auto");
    public static EndBuilder Px => new("px");
}
