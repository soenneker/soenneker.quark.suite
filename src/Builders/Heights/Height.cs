
namespace Soenneker.Quark;

/// <summary>
/// Simplified height utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Height
{
    /// <summary>
    /// 25% height.
    /// </summary>
    public static HeightBuilder Is25 => new("25");

    /// <summary>
    /// 50% height.
    /// </summary>
    public static HeightBuilder Is50 => new("50");

    /// <summary>
    /// 75% height.
    /// </summary>
    public static HeightBuilder Is75 => new("75");

    /// <summary>
    /// 100% height.
    /// </summary>
    public static HeightBuilder Is100 => new("100");

    /// <summary>
    /// Auto height.
    /// </summary>
    public static HeightBuilder Auto => new("auto");
}
