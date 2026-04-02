using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

/// <summary>
/// Static utility class for creating float builders with predefined values.
/// </summary>
public static class Float
{
    /// <summary>
    /// Gets a float builder with none value (no floating).
    /// </summary>
    public static FloatBuilder None => new(FloatKeyword.NoneValue);

    /// <summary>
    /// Gets a float builder with left value (float left).
    /// </summary>
    public static FloatBuilder Left => new(FloatKeyword.LeftValue);

    /// <summary>
    /// Gets a float builder with start value (float inline-start).
    /// </summary>
    public static FloatBuilder Start => new(FloatKeyword.InlineStartValue);

    /// <summary>
    /// Gets a float builder with right value (float right).
    /// </summary>
    public static FloatBuilder Right => new(FloatKeyword.RightValue);

    /// <summary>
    /// Gets a float builder with end value (float inline-end).
    /// </summary>
    public static FloatBuilder End => new(FloatKeyword.InlineEndValue);

    /// <summary>
    /// Gets a float builder with inherit keyword.
    /// </summary>
    public static FloatBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Gets a float builder with initial keyword.
    /// </summary>
    public static FloatBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Gets a float builder with revert keyword.
    /// </summary>
    public static FloatBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Gets a float builder with revert-layer keyword.
    /// </summary>
    public static FloatBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Gets a float builder with unset keyword.
    /// </summary>
    public static FloatBuilder Unset => new(GlobalKeyword.UnsetValue);
}
