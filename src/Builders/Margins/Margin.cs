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
    public static MarginBuilder S0 => new(ScaleType.S0Value);

    /// <summary>
    /// Size 1 margin (0.25rem).
    /// </summary>
    public static MarginBuilder S1 => new(ScaleType.S1Value);

    /// <summary>
    /// Size 2 margin (0.5rem).
    /// </summary>
    public static MarginBuilder S2 => new(ScaleType.S2Value);

    /// <summary>
    /// Size 3 margin (1rem).
    /// </summary>
    public static MarginBuilder S3 => new(ScaleType.S3Value);

    /// <summary>
    /// Size 4 margin (1.5rem).
    /// </summary>
    public static MarginBuilder S4 => new(ScaleType.S4Value);

    /// <summary>
    /// Size 5 margin (3rem).
    /// </summary>
    public static MarginBuilder S5 => new(ScaleType.S5Value);

    /// <summary>
    /// Auto margin (auto).
    /// </summary>
    public static MarginBuilder Auto => new("auto");
}
