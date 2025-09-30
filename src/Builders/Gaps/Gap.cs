
namespace Soenneker.Quark;

/// <summary>
/// Simplified gap utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Gap
{
    /// <summary>
    /// No gap (0).
    /// </summary>
    public static GapBuilder Is0 => new(Enums.ScaleType.S0Value);

    /// <summary>
    /// Size 1 gap (0.25rem).
    /// </summary>
    public static GapBuilder Is1 => new(Enums.ScaleType.S1Value);

    /// <summary>
    /// Size 2 gap (0.5rem).
    /// </summary>
    public static GapBuilder Is2 => new(Enums.ScaleType.S2Value);

    /// <summary>
    /// Size 3 gap (1rem).
    /// </summary>
    public static GapBuilder Is3 => new(Enums.ScaleType.S3Value);

    /// <summary>
    /// Size 4 gap (1.5rem).
    /// </summary>
    public static GapBuilder Is4 => new(Enums.ScaleType.S4Value);

    /// <summary>
    /// Size 5 gap (3rem).
    /// </summary>
    public static GapBuilder Is5 => new(Enums.ScaleType.S5Value);
}
