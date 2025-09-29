namespace Soenneker.Quark.Builders.Transforms;

/// <summary>
/// Simplified transform utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Transform
{
    /// <summary>
    /// No transform (none).
    /// </summary>
    public static TransformBuilder None => new("none");

    /// <summary>
    /// Scale transform (scale).
    /// </summary>
    public static TransformBuilder Scale => new("scale");

    /// <summary>
    /// Rotate transform (rotate).
    /// </summary>
    public static TransformBuilder Rotate => new("rotate");

    /// <summary>
    /// Translate transform (translate).
    /// </summary>
    public static TransformBuilder Translate => new("translate");

    /// <summary>
    /// Skew transform (skew).
    /// </summary>
    public static TransformBuilder Skew => new("skew");
}
