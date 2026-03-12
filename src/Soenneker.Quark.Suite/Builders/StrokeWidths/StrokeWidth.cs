namespace Soenneker.Quark;

/// <summary>
/// Static utility for stroke width. Tailwind: stroke-0, stroke-1, stroke-2.
/// </summary>
public static class StrokeWidth
{
    public static StrokeWidthBuilder Is0 => new("0");
    public static StrokeWidthBuilder Is1 => new("1");
    public static StrokeWidthBuilder Is2 => new("2");
}
