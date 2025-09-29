namespace Soenneker.Quark.Builders.ClipPaths;

/// <summary>
/// Simplified clip path utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class ClipPath
{
    /// <summary>
    /// No clip path (none).
    /// </summary>
    public static ClipPathBuilder None => new("none");

    /// <summary>
    /// Circle clip path (circle).
    /// </summary>
    public static ClipPathBuilder Circle => new("circle");

    /// <summary>
    /// Ellipse clip path (ellipse).
    /// </summary>
    public static ClipPathBuilder Ellipse => new("ellipse");

    /// <summary>
    /// Inset clip path (inset).
    /// </summary>
    public static ClipPathBuilder Inset => new("inset");

    /// <summary>
    /// Polygon clip path (polygon).
    /// </summary>
    public static ClipPathBuilder Polygon => new("polygon");
}
