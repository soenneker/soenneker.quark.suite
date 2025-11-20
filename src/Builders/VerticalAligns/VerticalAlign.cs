using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating vertical align builders with predefined values.
/// </summary>
public static class VerticalAlign
{
    /// <summary>
    /// Gets a vertical align builder with baseline alignment.
    /// </summary>
    public static VerticalAlignBuilder Baseline => new("baseline");
    /// <summary>
    /// Gets a vertical align builder with top alignment.
    /// </summary>
    public static VerticalAlignBuilder Top => new("top");
    /// <summary>
    /// Gets a vertical align builder with middle alignment.
    /// </summary>
    public static VerticalAlignBuilder Middle => new("middle");
    /// <summary>
    /// Gets a vertical align builder with bottom alignment.
    /// </summary>
    public static VerticalAlignBuilder Bottom => new("bottom");
    /// <summary>
    /// Gets a vertical align builder with text-top alignment.
    /// </summary>
    public static VerticalAlignBuilder TextTop => new("text-top");
    /// <summary>
    /// Gets a vertical align builder with text-bottom alignment.
    /// </summary>
    public static VerticalAlignBuilder TextBottom => new("text-bottom");

    /// <summary>
    /// Gets a vertical align builder with inherit keyword.
    /// </summary>
    public static VerticalAlignBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a vertical align builder with initial keyword.
    /// </summary>
    public static VerticalAlignBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a vertical align builder with revert keyword.
    /// </summary>
    public static VerticalAlignBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a vertical align builder with revert-layer keyword.
    /// </summary>
    public static VerticalAlignBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a vertical align builder with unset keyword.
    /// </summary>
    public static VerticalAlignBuilder Unset => new(GlobalKeyword.UnsetValue);
}
