namespace Soenneker.Quark.Builders.Heights;

/// <summary>
/// Simplified height utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Height
{
    /// <summary>
    /// 25% height.
    /// </summary>
    public static HeightBuilder P25 => new("25");

    /// <summary>
    /// 50% height.
    /// </summary>
    public static HeightBuilder P50 => new("50");

    /// <summary>
    /// 75% height.
    /// </summary>
    public static HeightBuilder P75 => new("75");

    /// <summary>
    /// 100% height.
    /// </summary>
    public static HeightBuilder P100 => new("100");

    /// <summary>
    /// Auto height.
    /// </summary>
    public static HeightBuilder Auto => new("auto");
}
