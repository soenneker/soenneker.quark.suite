using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Static utility for scroll margin. Tailwind: scroll-m-*.
/// </summary>
public static class ScrollMargin
{
    public static ScrollMarginBuilder Is0 => new(ScaleType.Is0Value);
    public static ScrollMarginBuilder Is1 => new(ScaleType.Is1Value);
    public static ScrollMarginBuilder Is2 => new(ScaleType.Is2Value);
    public static ScrollMarginBuilder Is3 => new(ScaleType.Is3Value);
    public static ScrollMarginBuilder Is4 => new(ScaleType.Is4Value);
    public static ScrollMarginBuilder Is5 => new(ScaleType.Is5Value);
    public static ScrollMarginBuilder Px => new("px");
}
