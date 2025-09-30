using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Simplified padding utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Padding
{
    /// <summary>
    /// No padding (0).
    /// </summary>
    public static PaddingBuilder Is0 => new(ScaleType.Is0Value);

    /// <summary>
    /// Size 1 padding (0.25rem).
    /// </summary>
    public static PaddingBuilder Is1 => new(ScaleType.Is1Value);

    /// <summary>
    /// Size 2 padding (0.5rem).
    /// </summary>
    public static PaddingBuilder Is2 => new(ScaleType.Is2Value);

    /// <summary>
    /// Size 3 padding (1rem).
    /// </summary>
    public static PaddingBuilder Is3 => new(ScaleType.Is3Value);

    /// <summary>
    /// Size 4 padding (1.5rem).
    /// </summary>
    public static PaddingBuilder Is4 => new(ScaleType.Is4Value);

    /// <summary>
    /// Size 5 padding (3rem).
    /// </summary>
    public static PaddingBuilder Is5 => new(ScaleType.Is5Value);
}
