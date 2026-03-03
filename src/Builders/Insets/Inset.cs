using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Static utility for inset (position offset) builders. Tailwind: inset-*, top-*, right-*, bottom-*, left-*, start-*, end-*.
/// </summary>
public static class Inset
{
    public static InsetBuilder Is0 => new(ScaleType.Is0Value);
    public static InsetBuilder Is1 => new(ScaleType.Is1Value);
    public static InsetBuilder Is2 => new(ScaleType.Is2Value);
    public static InsetBuilder Is3 => new(ScaleType.Is3Value);
    public static InsetBuilder Is4 => new(ScaleType.Is4Value);
    public static InsetBuilder Is5 => new(ScaleType.Is5Value);
    public static InsetBuilder Px => new("px");
    public static InsetBuilder Auto => new("auto");
}
