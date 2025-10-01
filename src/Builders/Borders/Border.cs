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
    public static BorderBuilder Is0 => new(ScaleType.Is0.Value);

    /// <summary>
    /// Size 1 border (1px).
    /// </summary>
    public static BorderBuilder Is1 => new(ScaleType.Is1.Value);

    /// <summary>
    /// Size 2 border (2px).
    /// </summary>
    public static BorderBuilder Is2 => new(ScaleType.Is2.Value);

    /// <summary>
    /// Size 3 border (3px).
    /// </summary>
    public static BorderBuilder Is3 => new(ScaleType.Is3.Value);

    /// <summary>
    /// Size 4 border (4px).
    /// </summary>
    public static BorderBuilder Is4 => new(ScaleType.Is4.Value);

    /// <summary>
    /// Size 5 border (5px).
    /// </summary>
    public static BorderBuilder Is5 => new(ScaleType.Is5.Value);
}
