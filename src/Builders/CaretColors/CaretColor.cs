namespace Soenneker.Quark;

/// <summary>
/// Static utility for caret color. Tailwind: caret-*.
/// </summary>
public static class CaretColor
{
    public static CaretColorBuilder Primary => new("primary");
    public static CaretColorBuilder Transparent => new("transparent");
    public static CaretColorBuilder Current => new("current");
}
