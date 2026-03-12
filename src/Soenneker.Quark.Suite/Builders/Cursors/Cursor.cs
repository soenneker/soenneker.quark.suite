
namespace Soenneker.Quark;

/// <summary>
/// Simplified cursor utility with fluent API and Bootstrap-first approach.
/// </summary>
public static class Cursor
{
    /// <summary>
    /// Default cursor (auto).
    /// </summary>
    public static CursorBuilder Default => new("auto");

    /// <summary>
    /// Pointer cursor (clickable).
    /// </summary>
    public static CursorBuilder Pointer => new("pointer");

    /// <summary>
    /// Grab cursor (draggable).
    /// </summary>
    public static CursorBuilder Grab => new("grab");

    /// <summary>
    /// Grabbing cursor (dragging).
    /// </summary>
    public static CursorBuilder Grabbing => new("grabbing");

    /// <summary>
    /// Text cursor (selectable text).
    /// </summary>
    public static CursorBuilder Text => new("text");

    /// <summary>
    /// Move cursor (movable).
    /// </summary>
    public static CursorBuilder Move => new("move");

    /// <summary>
    /// Resize cursor (resizable).
    /// </summary>
    public static CursorBuilder Resize => new("resize");

    /// <summary>
    /// Not allowed cursor (disabled).
    /// </summary>
    public static CursorBuilder NotAllowed => new("not-allowed");

    /// <summary>
    /// Help cursor (help available).
    /// </summary>
    public static CursorBuilder Help => new("help");

    /// <summary>
    /// Wait cursor (loading).
    /// </summary>
    public static CursorBuilder Wait => new("wait");

    /// <summary>
    /// Crosshair cursor (precise selection).
    /// </summary>
    public static CursorBuilder Crosshair => new("crosshair");

    /// <summary>
    /// Zoom in cursor.
    /// </summary>
    public static CursorBuilder ZoomIn => new("zoom-in");

    /// <summary>
    /// Zoom out cursor.
    /// </summary>
    public static CursorBuilder ZoomOut => new("zoom-out");
}
