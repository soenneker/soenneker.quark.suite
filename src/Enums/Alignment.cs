using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Defines the alignment of an element.
/// </summary>
[Intellenum<string>]
public sealed partial class Alignment
{
    /// <summary>
    /// No particular alignment rule will be applied, meaning a default alignment will be used.
    /// </summary>
    public static readonly Alignment Default = new("default");

    /// <summary>
    /// Aligns an element to the left.
    /// </summary>
    public static readonly Alignment Start = new("start");

    /// <summary>
    /// Aligns an element on the center.
    /// </summary>
    public static readonly Alignment Center = new("center");

    /// <summary>
    /// Aligns an element to the right.
    /// </summary>
    public static readonly Alignment End = new("end");
}

