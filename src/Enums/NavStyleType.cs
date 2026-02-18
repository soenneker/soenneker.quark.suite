using Intellenum;

namespace Soenneker.Quark.Enums;

/// <summary>
/// Represents nav style types.
/// </summary>
[Intellenum<string>]
public sealed partial class NavStyleType
{
    /// <summary>
    /// Default nav style.
    /// </summary>
    public static readonly NavStyleType Default = new("");

    /// <summary>
    /// Tab-style navigation.
    /// </summary>
    public static readonly NavStyleType Tabs = new("tabs");

    /// <summary>
    /// Pill-style navigation.
    /// </summary>
    public static readonly NavStyleType Pills = new("pills");

    /// <summary>
    /// Justified navigation.
    /// </summary>
    public static readonly NavStyleType Justified = new("justified");

    /// <summary>
    /// Fill navigation.
    /// </summary>
    public static readonly NavStyleType Fill = new("fill");
}
