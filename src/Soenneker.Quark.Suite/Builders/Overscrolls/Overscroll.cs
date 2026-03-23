
namespace Soenneker.Quark;

/// <summary>
/// Tailwind overscroll-behavior utility entry points.
/// </summary>
public static class Overscroll
{
    public static OverscrollBuilder Auto => new("auto");
    public static OverscrollBuilder Contain => new("contain");
    public static OverscrollBuilder None => new("none");

    public static OverscrollBuilder XAuto => new("x-auto");
    public static OverscrollBuilder XContain => new("x-contain");
    public static OverscrollBuilder XNone => new("x-none");

    public static OverscrollBuilder YAuto => new("y-auto");
    public static OverscrollBuilder YContain => new("y-contain");
    public static OverscrollBuilder YNone => new("y-none");
}
