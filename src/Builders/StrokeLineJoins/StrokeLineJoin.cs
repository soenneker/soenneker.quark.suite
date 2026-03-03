namespace Soenneker.Quark;

/// <summary>
/// Static utility for stroke line join. Tailwind: stroke-join-*.
/// </summary>
public static class StrokeLineJoin
{
    public static StrokeLineJoinBuilder Auto => new("auto");
    public static StrokeLineJoinBuilder Round => new("round");
    public static StrokeLineJoinBuilder Bevel => new("bevel");
    public static StrokeLineJoinBuilder Miter => new("miter");
}
