namespace Soenneker.Quark;

/// <summary>
/// Static utility for contain. Tailwind: contain-*.
/// </summary>
public static class Contain
{
    public static ContainBuilder None => new("none");
    public static ContainBuilder Size => new("size");
    public static ContainBuilder Layout => new("layout");
    public static ContainBuilder Style => new("style");
    public static ContainBuilder Paint => new("paint");
    public static ContainBuilder Strict => new("strict");
    public static ContainBuilder Content => new("content");
}
