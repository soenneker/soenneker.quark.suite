using Soenneker.Quark.Enums;


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

    /// <summary>
    /// Gets a visibility builder with inherit keyword.
    /// </summary>
    public static VisibilityBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a visibility builder with initial keyword.
    /// </summary>
    public static VisibilityBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a visibility builder with revert keyword.
    /// </summary>
    public static VisibilityBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a visibility builder with revert-layer keyword.
    /// </summary>
    public static VisibilityBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a visibility builder with unset keyword.
    /// </summary>
    public static VisibilityBuilder Unset => new(GlobalKeyword.UnsetValue);
}
