namespace Soenneker.Quark.Builders.Abstract;

/// <summary>
/// Interface for CSS builders that can generate CSS classes and styles.
/// </summary>
public interface ICssBuilder
{
    /// <summary>
    /// Gets the CSS class string for the current configuration.
    /// </summary>
    string ToClass();

    /// <summary>
    /// Gets the CSS style string for the current configuration.
    /// </summary>
    string ToStyle();
}
