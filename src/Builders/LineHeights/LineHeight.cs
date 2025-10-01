using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

public static class LineHeight
{
    public static LineHeightBuilder Is1 => new(ScaleType.Is1.Value);
    public static LineHeightBuilder Small => new(SizeType.Small.Value);
    public static LineHeightBuilder Base => new("base");
    public static LineHeightBuilder Large => new(SizeType.Large.Value);
}
