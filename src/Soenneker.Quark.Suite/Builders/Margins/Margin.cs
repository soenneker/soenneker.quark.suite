using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Simplified margin utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Margin
{
    /// <summary>
    /// No margin (0).
    /// </summary>
    public static MarginBuilder Is0 => new(ScaleType.Is0Value);

    /// <summary>
    /// Size 1 margin (0.25rem).
    /// </summary>
    public static MarginBuilder Is1 => new(ScaleType.Is1Value);

    /// <summary>
    /// Size 2 margin (0.5rem).
    /// </summary>
    public static MarginBuilder Is2 => new(ScaleType.Is2Value);

    /// <summary>
    /// Size 3 margin (1rem).
    /// </summary>
    public static MarginBuilder Is3 => new(ScaleType.Is3Value);

    /// <summary>
    /// Size 4 margin (1.5rem).
    /// </summary>
    public static MarginBuilder Is4 => new(ScaleType.Is4Value);

    /// <summary>
    /// Size 5 margin (3rem).
    /// </summary>
    public static MarginBuilder Is5 => new(ScaleType.Is5Value);

    /// <summary>
    /// Auto margin (auto).
    /// </summary>
    public static MarginBuilder Auto => new("auto");
}
