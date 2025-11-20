using Soenneker.Quark.Enums;

namespace Soenneker.Quark;

/// <summary>
/// Represents Bootstrap nav utilities.
/// </summary>
public readonly struct NavStyle
{
    /// <summary>
    /// Gets the nav style type.
    /// </summary>
    public readonly NavStyleType Type;

    /// <summary>
    /// Initializes a new instance of the NavStyle struct.
    /// </summary>
    /// <param name="type">The nav style type.</param>
    public NavStyle(NavStyleType type)
    {
        Type = type;
    }

    /// <summary>
    /// Gets a nav style with default styling.
    /// </summary>
    public static NavStyle Default => new(NavStyleType.Default);
    /// <summary>
    /// Gets a nav style with tabs styling.
    /// </summary>
    public static NavStyle Tabs => new(NavStyleType.Tabs);
    /// <summary>
    /// Gets a nav style with pills styling.
    /// </summary>
    public static NavStyle Pills => new(NavStyleType.Pills);
    /// <summary>
    /// Gets a nav style with justified styling.
    /// </summary>
    public static NavStyle Justified => new(NavStyleType.Justified);
    /// <summary>
    /// Gets a nav style with fill styling.
    /// </summary>
    public static NavStyle Fill => new(NavStyleType.Fill);
}