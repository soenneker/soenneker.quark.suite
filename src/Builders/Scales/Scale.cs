using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Scale utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Scale
{
    /// <summary>
    /// Scale 0 (no scaling).
    /// </summary>
    public static ScaleBuilder Is0 => new(ScaleType.S0);

    /// <summary>
    /// Scale 1 (normal size).
    /// </summary>
    public static ScaleBuilder Is1 => new(ScaleType.S1);

    /// <summary>
    /// Scale 2 (2x scaling).
    /// </summary>
    public static ScaleBuilder Is2 => new(ScaleType.S2);

    /// <summary>
    /// Scale 3 (3x scaling).
    /// </summary>
    public static ScaleBuilder Is3 => new(ScaleType.S3);

    /// <summary>
    /// Scale 4 (4x scaling).
    /// </summary>
    public static ScaleBuilder Is4 => new(ScaleType.S4);

    /// <summary>
    /// Scale 5 (5x scaling).
    /// </summary>
    public static ScaleBuilder Is5 => new(ScaleType.S5);

    /// <summary>
    /// Scale 6 (6x scaling).
    /// </summary>
    public static ScaleBuilder Is6 => new(ScaleType.S6);

    /// <summary>
    /// Create from a ScaleType enum value.
    /// </summary>
    public static ScaleBuilder From(ScaleType scaleType) => new(scaleType);
}
