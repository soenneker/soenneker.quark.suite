namespace Soenneker.Quark.Builders.Transitions;

/// <summary>
/// Simplified transition utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Transition
{
    /// <summary>
    /// No transition (none).
    /// </summary>
    public static TransitionBuilder None => new("none");

    /// <summary>
    /// All properties transition (all).
    /// </summary>
    public static TransitionBuilder All => new("all");

    /// <summary>
    /// Colors transition (colors).
    /// </summary>
    public static TransitionBuilder Colors => new("colors");

    /// <summary>
    /// Opacity transition (opacity).
    /// </summary>
    public static TransitionBuilder Opacity => new("opacity");

    /// <summary>
    /// Shadow transition (shadow).
    /// </summary>
    public static TransitionBuilder Shadow => new("shadow");

    /// <summary>
    /// Transform transition (transform).
    /// </summary>
    public static TransitionBuilder Transform => new("transform");
}
