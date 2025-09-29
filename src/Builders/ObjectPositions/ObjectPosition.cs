namespace Soenneker.Quark.Builders.ObjectPositions;

/// <summary>
/// Simplified object position utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class ObjectPosition
{
    /// <summary>
    /// Center object position (center).
    /// </summary>
    public static ObjectPositionBuilder Center => new("center");

    /// <summary>
    /// Top object position (top).
    /// </summary>
    public static ObjectPositionBuilder Top => new("top");

    /// <summary>
    /// Right object position (right).
    /// </summary>
    public static ObjectPositionBuilder Right => new("right");

    /// <summary>
    /// Bottom object position (bottom).
    /// </summary>
    public static ObjectPositionBuilder Bottom => new("bottom");

    /// <summary>
    /// Left object position (left).
    /// </summary>
    public static ObjectPositionBuilder Left => new("left");

    /// <summary>
    /// Top left object position (top-left).
    /// </summary>
    public static ObjectPositionBuilder TopLeft => new("top-left");

    /// <summary>
    /// Top right object position (top-right).
    /// </summary>
    public static ObjectPositionBuilder TopRight => new("top-right");

    /// <summary>
    /// Bottom left object position (bottom-left).
    /// </summary>
    public static ObjectPositionBuilder BottomLeft => new("bottom-left");

    /// <summary>
    /// Bottom right object position (bottom-right).
    /// </summary>
    public static ObjectPositionBuilder BottomRight => new("bottom-right");
}
