namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating pointer events builders with predefined values.
/// </summary>
public static class PointerEvents
{
    /// <summary>
    /// Gets a pointer events builder with none value (element does not respond to pointer events).
    /// </summary>
    public static PointerEventsBuilder None => new("none");
    /// <summary>
    /// Gets a pointer events builder with auto value (element responds to pointer events normally).
    /// </summary>
    public static PointerEventsBuilder Auto => new("auto");
}
