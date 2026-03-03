namespace Soenneker.Quark;

/// <summary>
/// Static utility for stroke line cap. Tailwind: stroke-cap-*.
/// </summary>
public static class StrokeLineCap
{
    public static StrokeLineCapBuilder Auto => new("auto");
    public static StrokeLineCapBuilder Round => new("round");
    public static StrokeLineCapBuilder Square => new("square");
    public static StrokeLineCapBuilder Butt => new("butt");
}
