using Soenneker.Quark.Enums;

namespace Soenneker.Quark.Builders.Positions;

/// <summary>
/// Simplified position utility with fluent API and Bootstrap-first approach.
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

    public static PositionBuilder Inherit => new(GlobalKeyword.InheritValue);
    public static PositionBuilder Initial => new(GlobalKeyword.InitialValue);
    public static PositionBuilder Revert => new(GlobalKeyword.RevertValue);
    public static PositionBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    public static PositionBuilder Unset => new(GlobalKeyword.UnsetValue);
}
