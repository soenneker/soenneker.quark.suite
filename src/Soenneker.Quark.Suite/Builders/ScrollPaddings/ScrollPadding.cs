namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll padding. Tailwind: scroll-p-*.
/// </summary>
public static class ScrollPadding
{
    public static ScrollPaddingBuilder Is0 => new(ScaleType.Is0Value);
    public static ScrollPaddingBuilder Is1 => new(ScaleType.Is1Value);
    public static ScrollPaddingBuilder Is2 => new(ScaleType.Is2Value);
    public static ScrollPaddingBuilder Is3 => new(ScaleType.Is3Value);
    public static ScrollPaddingBuilder Is4 => new(ScaleType.Is4Value);
    public static ScrollPaddingBuilder Is5 => new(ScaleType.Is5Value);
    public static ScrollPaddingBuilder Px => new("px");
}
