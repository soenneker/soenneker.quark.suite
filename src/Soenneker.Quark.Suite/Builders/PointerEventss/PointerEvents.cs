using Soenneker.Quark.Enums;


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

    /// <summary>
    /// Gets a pointer events builder with inherit keyword.
    /// </summary>
    public static PointerEventsBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a pointer events builder with initial keyword.
    /// </summary>
    public static PointerEventsBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a pointer events builder with revert keyword.
    /// </summary>
    public static PointerEventsBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a pointer events builder with revert-layer keyword.
    /// </summary>
    public static PointerEventsBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a pointer events builder with unset keyword.
    /// </summary>
    public static PointerEventsBuilder Unset => new(GlobalKeyword.UnsetValue);
}
