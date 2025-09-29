using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Simplified border utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Border
{
    /// <summary>
    /// No border (0).
    /// </summary>
    public static BorderBuilder S0 => new(ScaleType.S0.Value);

    /// <summary>
    /// Size 1 border (1px).
    /// </summary>
    public static BorderBuilder S1 => new(ScaleType.S1.Value);

    /// <summary>
    /// Size 2 border (2px).
    /// </summary>
    public static BorderBuilder S2 => new(ScaleType.S2.Value);

    /// <summary>
    /// Size 3 border (3px).
    /// </summary>
    public static BorderBuilder S3 => new(ScaleType.S3.Value);

    /// <summary>
    /// Size 4 border (4px).
    /// </summary>
    public static BorderBuilder S4 => new(ScaleType.S4.Value);

    /// <summary>
    /// Size 5 border (5px).
    /// </summary>
    public static BorderBuilder S5 => new(ScaleType.S5.Value);
}
