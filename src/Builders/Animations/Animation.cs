namespace Soenneker.Quark.Builders.Animations;

/// <summary>
/// Simplified animation utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Animation
{
    /// <summary>
    /// No animation (none).
    /// </summary>
    public static AnimationBuilder None => new("none");

    /// <summary>
    /// Spin animation (spin).
    /// </summary>
    public static AnimationBuilder Spin => new("spin");

    /// <summary>
    /// Ping animation (ping).
    /// </summary>
    public static AnimationBuilder Ping => new("ping");

    /// <summary>
    /// Pulse animation (pulse).
    /// </summary>
    public static AnimationBuilder Pulse => new("pulse");

    /// <summary>
    /// Bounce animation (bounce).
    /// </summary>
    public static AnimationBuilder Bounce => new("bounce");
}
