namespace Soenneker.Quark;

/// <summary>
/// Tailwind font family utility entry points.
/// </summary>
public static class FontFamily
{
    public static FontFamilyBuilder Sans => new("sans");
    public static FontFamilyBuilder Serif => new("serif");
    public static FontFamilyBuilder Mono => new("mono");
}
