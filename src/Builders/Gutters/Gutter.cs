namespace Soenneker.Quark;

/// <summary>
/// Static helper for Bootstrap gutter utilities.
/// </summary>
public static class Gutter
{
    /// <summary>
    /// Creates a gutter builder with the specified horizontal (X) gutter value.
    /// </summary>
    /// <param name="value">The horizontal gutter value.</param>
    /// <returns>A gutter builder with the specified horizontal value.</returns>
    public static GutterBuilder X(int value) => new GutterBuilder().X(value);
    /// <summary>
    /// Creates a gutter builder with the specified vertical (Y) gutter value.
    /// </summary>
    /// <param name="value">The vertical gutter value.</param>
    /// <returns>A gutter builder with the specified vertical value.</returns>
    public static GutterBuilder Y(int value) => new GutterBuilder().Y(value);
    /// <summary>
    /// Creates a gutter builder with the specified value for both horizontal and vertical gutters.
    /// </summary>
    /// <param name="value">The gutter value to apply to both axes.</param>
    /// <returns>A gutter builder with the specified value for both axes.</returns>
    public static GutterBuilder All(int value) => new GutterBuilder().All(value);
    /// <summary>
    /// Gets a gutter builder with no gutters (0).
    /// </summary>
    /// <returns>A gutter builder with no gutters.</returns>
    public static GutterBuilder None => new GutterBuilder().None();
}
