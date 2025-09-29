namespace Soenneker.Quark.Builders.Resizes;

/// <summary>
/// Simplified resize utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Resize
{
    /// <summary>
    /// No resize (none).
    /// </summary>
    public static ResizeBuilder None => new("none");

    /// <summary>
    /// Resize both (both).
    /// </summary>
    public static ResizeBuilder Both => new("both");

    /// <summary>
    /// Resize horizontally (horizontal).
    /// </summary>
    public static ResizeBuilder Horizontal => new("horizontal");

    /// <summary>
    /// Resize vertically (vertical).
    /// </summary>
    public static ResizeBuilder Vertical => new("vertical");
}
