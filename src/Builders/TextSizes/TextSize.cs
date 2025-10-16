using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Simplified text size utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class TextSize
{
    /// <summary>
    /// Font size 1 (largest). Generates 'fs-1' class.
    /// </summary>
    public static TextSizeBuilder Is1 => new("1");

    /// <summary>
    /// Font size 2. Generates 'fs-2' class.
    /// </summary>
    public static TextSizeBuilder Is2 => new("2");

    /// <summary>
    /// Font size 3. Generates 'fs-3' class.
    /// </summary>
    public static TextSizeBuilder Is3 => new("3");

    /// <summary>
    /// Font size 4. Generates 'fs-4' class.
    /// </summary>
    public static TextSizeBuilder Is4 => new("4");

    /// <summary>
    /// Font size 5. Generates 'fs-5' class.
    /// </summary>
    public static TextSizeBuilder Is5 => new("5");

    /// <summary>
    /// Font size 6 (smallest). Generates 'fs-6' class.
    /// </summary>
    public static TextSizeBuilder Is6 => new("6");
}
