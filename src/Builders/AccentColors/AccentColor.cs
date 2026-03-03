namespace Soenneker.Quark;

/// <summary>
/// Static utility for accent color. Tailwind: accent-*.
/// </summary>
public static class AccentColor
{
    public static AccentColorBuilder Auto => new("auto");
    public static AccentColorBuilder Primary => new("primary");
    public static AccentColorBuilder Transparent => new("transparent");
    public static AccentColorBuilder Current => new("current");
}
