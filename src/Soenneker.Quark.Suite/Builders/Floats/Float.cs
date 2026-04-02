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
}
