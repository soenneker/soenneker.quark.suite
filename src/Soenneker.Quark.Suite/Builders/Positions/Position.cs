namespace Soenneker.Quark;

/// <summary>
/// Simplified position utility with fluent API and Tailwind/shadcn-aligned fluent API.
/// </summary>
public static class Position
{
    /// <summary>
    /// Static positioning.
    /// </summary>
    public static PositionBuilder Static => new(PositionKeyword.StaticValue);

    /// <summary>
    /// Relative positioning.
    /// </summary>
    public static PositionBuilder Relative => new(PositionKeyword.RelativeValue);

    /// <summary>
    /// Absolute positioning.
    /// </summary>
    public static PositionBuilder Absolute => new(PositionKeyword.AbsoluteValue);

    /// <summary>
    /// Fixed positioning.
    /// </summary>
    public static PositionBuilder Fixed => new(PositionKeyword.FixedValue);

    /// <summary>
    /// Sticky positioning.
    /// </summary>
    public static PositionBuilder Sticky => new(PositionKeyword.StickyValue);
}
