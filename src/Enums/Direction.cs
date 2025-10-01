using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Defines the direction for dropdown placement.
/// </summary>
[Intellenum<string>]
public sealed partial class Direction
{
    /// <summary>
    /// Default direction (down).
    /// </summary>
    public static readonly Direction Default = new("default");

    /// <summary>
    /// Dropdown appears above the trigger.
    /// </summary>
    public static readonly Direction Up = new("up");

    /// <summary>
    /// Dropdown appears to the right of the trigger.
    /// </summary>
    public static readonly Direction End = new("end");

    /// <summary>
    /// Dropdown appears to the left of the trigger.
    /// </summary>
    public static readonly Direction Start = new("start");
}
