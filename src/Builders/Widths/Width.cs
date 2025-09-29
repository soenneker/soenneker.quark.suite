
namespace Soenneker.Quark;

/// <summary>
/// Simplified width utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Width
{
    /// <summary>
    /// 25% width.
    /// </summary>
    public static WidthBuilder P25 => new("25");

    /// <summary>
    /// 50% width.
    /// </summary>
    public static WidthBuilder P50 => new("50");

    /// <summary>
    /// 75% width.
    /// </summary>
    public static WidthBuilder P75 => new("75");

    /// <summary>
    /// 100% width.
    /// </summary>
    public static WidthBuilder P100 => new("100");

    /// <summary>
    /// Auto width.
    /// </summary>
    public static WidthBuilder Auto => new("auto");
}


