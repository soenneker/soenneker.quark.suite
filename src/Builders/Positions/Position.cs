using Soenneker.Quark.Enums;


namespace Soenneker.Quark;

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

    /// <summary>
    /// Inherit positioning from parent.
    /// </summary>
    public static PositionBuilder Inherit => new(GlobalKeyword.InheritValue);
    /// <summary>
    /// Initial positioning keyword.
    /// </summary>
    public static PositionBuilder Initial => new(GlobalKeyword.InitialValue);
    /// <summary>
    /// Revert positioning keyword.
    /// </summary>
    public static PositionBuilder Revert => new(GlobalKeyword.RevertValue);
    /// <summary>
    /// Revert-layer positioning keyword.
    /// </summary>
    public static PositionBuilder RevertLayer => new(GlobalKeyword.RevertLayerValue);
    /// <summary>
    /// Unset positioning keyword.
    /// </summary>
    public static PositionBuilder Unset => new(GlobalKeyword.UnsetValue);
}
