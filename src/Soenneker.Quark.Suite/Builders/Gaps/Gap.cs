namespace Soenneker.Quark;

/// <summary>
/// Simplified gap utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class Gap
{
    /// <summary>
    /// No gap (0).
    /// </summary>
    public static GapBuilder Is0 => new(ScaleType.Is0Value);

    /// <summary>
    /// Size 1 gap (0.25rem).
    /// </summary>
    public static GapBuilder Is1 => new(ScaleType.Is1Value);

    /// <summary>
    /// Size 2 gap (0.5rem).
    /// </summary>
    public static GapBuilder Is2 => new(ScaleType.Is2Value);

    /// <summary>
    /// Size 3 gap (1rem).
    /// </summary>
    public static GapBuilder Is3 => new(ScaleType.Is3Value);

    /// <summary>
    /// Size 4 gap (1.5rem).
    /// </summary>
    public static GapBuilder Is4 => new(ScaleType.Is4Value);

    /// <summary>
    /// Size 5 gap (3rem).
    /// </summary>
    public static GapBuilder Is5 => new(ScaleType.Is5Value);

    /// <summary>
    /// Create from an arbitrary Tailwind gap token (e.g. "1.5", "6", "8", "16", "20").
    /// </summary>
    public static GapBuilder Token(string value) => new(value);
}
