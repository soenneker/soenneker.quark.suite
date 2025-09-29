
namespace Soenneker.Quark;

/// <summary>
/// Simplified aspect ratio utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class AspectRatio
{
    /// <summary>
    /// 1:1 aspect ratio (square).
    /// </summary>
    public static AspectRatioBuilder R1x1 => new("1x1");

    /// <summary>
    /// 4:3 aspect ratio.
    /// </summary>
    public static AspectRatioBuilder R4x3 => new("4x3");

    /// <summary>
    /// 16:9 aspect ratio.
    /// </summary>
    public static AspectRatioBuilder R16x9 => new("16x9");

    /// <summary>
    /// 21:9 aspect ratio.
    /// </summary>
    public static AspectRatioBuilder R21x9 => new("21x9");
}
