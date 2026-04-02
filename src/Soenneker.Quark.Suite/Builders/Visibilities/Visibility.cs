namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating visibility builders with predefined values.
/// </summary>
public static class Visibility
{
    /// <summary>
    /// Gets a visibility builder with visible value (element is visible).
    /// </summary>
    public static VisibilityBuilder Visible => new(VisibilityKeyword.VisibleValue);

    /// <summary>
    /// Gets a visibility builder with invisible value (element is hidden but takes up space).
    /// </summary>
    public static VisibilityBuilder Invisible => new("invisible");
}
