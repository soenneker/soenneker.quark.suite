namespace Soenneker.Quark;

/// <summary>
/// Static utility for stroke width. Tailwind: stroke-0, stroke-1, stroke-2.
/// </summary>
public static class StrokeWidth
{
    /// <summary>
    /// Spacing/sizing scale step `0` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 0` for integer spacing utilities unless overridden).
    /// </summary>
    public static StrokeWidthBuilder Is0 => new("0");
    /// <summary>
    /// Spacing/sizing scale step `1` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 1` for integer spacing utilities unless overridden).
    /// </summary>
    public static StrokeWidthBuilder Is1 => new("1");
    /// <summary>
    /// Spacing/sizing scale step `2` — uses Tailwind’s default spacing scale (each step is typically `0.25rem × 2` for integer spacing utilities unless overridden).
    /// </summary>
    public static StrokeWidthBuilder Is2 => new("2");
}
