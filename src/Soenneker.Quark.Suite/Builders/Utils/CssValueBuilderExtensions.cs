namespace Soenneker.Quark;

/// <summary>
/// Extension methods for CSS value builders.
/// </summary>
public static class CssValueBuilderExtensions
{
    /// <summary>
    /// Creates a new CssValue with the specified CSS selector from a builder.
    /// </summary>
    /// <typeparam name="TBuilder">The type of CSS builder.</typeparam>
    /// <param name="builder">The CSS builder to convert.</param>
    /// <param name="selector">The CSS selector to apply.</param>
    /// <param name="absolute">Whether the selector is absolute (not relative to base selector).</param>
    /// <returns>A new CssValue with the specified selector.</returns>
    public static CssValue<TBuilder> WithSelector<TBuilder>(this TBuilder builder, string selector, bool absolute = false) where TBuilder : class, ICssBuilder
    {
        CssValue<TBuilder> value = builder;
        return value.WithSelector(selector, absolute);
    }
}


