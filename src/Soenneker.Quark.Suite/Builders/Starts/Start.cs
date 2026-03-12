using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Static utility for start (inset-inline-start). Tailwind: start-*.
/// </summary>
public static class Start
{
    public static StartBuilder Is0 => new(ScaleType.Is0Value);
    public static StartBuilder Is1 => new(ScaleType.Is1Value);
    public static StartBuilder Is2 => new(ScaleType.Is2Value);
    public static StartBuilder Is3 => new(ScaleType.Is3Value);
    public static StartBuilder Is4 => new(ScaleType.Is4Value);
    public static StartBuilder Is5 => new(ScaleType.Is5Value);
    public static StartBuilder Auto => new("auto");
    public static StartBuilder Px => new("px");
}
